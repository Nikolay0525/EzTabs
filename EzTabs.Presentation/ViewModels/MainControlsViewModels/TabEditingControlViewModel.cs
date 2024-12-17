using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.Services.ViewServices;
using EzTabs.Presentation.ViewModels.AuthControlsViewModels;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.Views.MainControls.SimpleControls.ControlBarParts.DropControls;
using System.IO;
using System.Text.Json;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel, ITrackElementFocus
{
    private readonly TuningService _tuningService;
    private readonly TabService _tabService;

    private System.Timers.Timer _saveTimer;

    private int _cursorPosition = 0;
    private int _cursorLine = 2;
    private int _cursorRow = 0;
    private int _cursorBar = 1;

    private bool _insert = false;
    private bool _makingBackup = false;
    private bool _isFocused;
    private List<Tuning> _tunings = new();
    private List<List<List<string>>> _tabRows = new();
    private string _tabText;
    private int _zoom = 100;
    private double _fontSize = 33.3;
    private int _lineLength = 20;
    private int _barAmount = 2;

    public bool IsFocused
    {
        get => _isFocused;
        set
        {
            _isFocused = value;
            OnPropertyChanged();
            UpdateTabText();
        }
    }

    public int CursorPosition
    {
        get => _cursorPosition;
        private set
        {
            _cursorPosition = value;
            OnPropertyChanged();
        }
    }
    
    public int CursorRow
    {
        get => _cursorRow;
        private set
        {
            _cursorRow = value;
            OnPropertyChanged();
        }
    }
    
    public int CursorBar
    {
        get => _cursorBar;
        private set
        {
            _cursorBar = value;
            OnPropertyChanged();
        }
    }
    
    public int CursorLine
    {
        get => _cursorLine;
        private set
        {
            _cursorLine = value;
            OnPropertyChanged();
        }
    }

    public string TabText
    {
        get => _tabText;
        private set
        {
            _tabText = value;
            OnPropertyChanged();
        }
    }

    public int Zoom
    {
        get => _zoom;
        set
        {
            _zoom = value;
            FontSize = value / 3;
            OnPropertyChanged();
            UpdateTabText();
        }
    }
    
    public double FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            OnPropertyChanged();
            UpdateTabText();
        }
    }
    
    public int LineLength
    {
        get => _lineLength;
        set
        {
            _lineLength = value;
            OnPropertyChanged();
        }
    }
    
    public int BarAmount
    {
        get => _barAmount;
        set
        {
            _barAmount = value;
            OnPropertyChanged();
        }
    }

    public bool MakingBackup 
    { 
        get => _makingBackup;
        set
        {
            _makingBackup = value;
            OnPropertyChanged();
        } 
    }
    
    public bool Insert 
    { 
        get => _insert;
        set
        {
            _insert = value;
            OnPropertyChanged();
        } 
    }

    public ICommand LoadBackupSaveCommand { get; }
    public ICommand SignOutNewCommand { get; }
    public ICommand ExportInFileCommand { get; }
    public ICommand SaveTabCommand { get; }
    public ICommand DeleteTabCommand { get; }
    public ICommand GoToMainPageCommand { get; }
    public ICommand HandleKeyCommand { get; }
    public ICommand CreateBarsButtonCommand { get; }
    public ICommand RemoveBarButtonCommand { get; }
    public ICommand CreateLineButtonCommand { get; }
    public ICommand RemoveLineButtonCommand { get; }
    public ICommand ExtendBarButtonCommand { get; }
    public ICommand ShortenBarButtonCommand { get; }
    public ICommand MoveCursorCommand { get; }
    public ICommand WriteNumberCommand { get; }
    public ICommand RemoveSymbolCommand { get; }

    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService, IWindowService windowService,
        TuningService tuningService, TabService tabService) : base(viewModelService, navigationService, windowService)
    {
        _tuningService = tuningService;
        _tabService = tabService;
        CreateTabText();
        LoadBackupSaveCommand = new RelayCommand(LoadBackupSave);
        SignOutNewCommand = new AsyncRelayCommand(SignOutNew);
        ExportInFileCommand = new RelayCommand(ExportTab);
        SaveTabCommand = new AsyncRelayCommand(SaveTab);
        DeleteTabCommand = new AsyncRelayCommand(DeleteTab);
        CreateLineButtonCommand = new RelayCommand(CreateRow);
        RemoveLineButtonCommand = new RelayCommand(RemoveRow);
        CreateBarsButtonCommand = new RelayCommand(CreateBar);
        RemoveBarButtonCommand = new RelayCommand(RemoveBar);
        ExtendBarButtonCommand = new RelayCommand(ExtendBar);
        ShortenBarButtonCommand = new RelayCommand(ShortenBar);
        GoToMainPageCommand = new AsyncRelayCommand(GoToMainPage);
        MoveCursorCommand = new RelayCommand<string>(MoveCursor);
        HandleKeyCommand = new RelayCommand<string>(HandleKeyPress);
        RemoveSymbolCommand = new RelayCommand(RemoveSymbol);

        _saveTimer = new System.Timers.Timer(300000);
        _saveTimer.Elapsed += OnDebounceTimerElapsed;
        _saveTimer.AutoReset = false;
        
        ButtonInDropControl deleteTab = new()
        {
            IsButtonVisible = true,
            Text = "Delete Tab",
            ButtonCommand = DeleteTabCommand
        };
        ButtonsInMenu.Insert(0, deleteTab);

        ButtonInDropControl exportTab = new()
        {
            IsButtonVisible = true,
            Text = "Export Tab",
            ButtonCommand = ExportInFileCommand
        };
        ButtonsInMenu.Insert(0, exportTab);

        ButtonInDropControl saveTab = new()
        {
            IsButtonVisible = true,
            Text = "Save Tab",
            ButtonCommand = SaveTabCommand
        };
        ButtonsInMenu.Insert(0, saveTab);

        ButtonInDropControl loadBackup = new()
        {
            IsButtonVisible = true,
            Text = "Load Backup",
            ButtonCommand = LoadBackupSaveCommand
        };
        ButtonsInMenu.Insert(0, loadBackup);
        
        ButtonInDropControl signOut = new()
        {
            IsButtonVisible = true,
            Text = "Sign Out",
            ButtonCommand = SignOutNewCommand
        };
        ButtonsInMenu.RemoveAt(4);
        ButtonsInMenu.Add(signOut);

        _saveTimer.Start();
    }

    private static void LoadBackupSave()
    {
        try
        {
            if(ShowOkCancelMessage("Editor",$"Are you sure want to load backup maded on {TabService.SavedTab!.DateOfBackup} ?") == true) TabService.LoadBackup();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task SignOutNew()
    {
        if (ShowOkCancelMessage("Editor", "Do you want to save your tab before exit?") == true) await SaveTab();
        NavigationService.NavigateTo<LoginControlViewModel>();
    }

    private async void OnDebounceTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        try
        {
            MakingBackup = true;
            await MakeBackup();
            MakingBackup = false;
            _saveTimer.Stop();
            _saveTimer.Start();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void SwitchInsert()
    {
        Insert = !Insert;
    }

    private void ExportTab()
    {
        try
        {
            string rootFolder = AppDomain.CurrentDomain.BaseDirectory;

            string targetFolder = Path.Combine(rootFolder, "ExportedTabs");

            Directory.CreateDirectory(targetFolder);

            var title = TabService.SavedTab!.Title.Replace(" ", "_");
            var band = TabService.SavedTab!.Band.Replace(" ", "_");
            var author = UserService.SavedUser!.Name;

            string filePath = Path.Combine(targetFolder, $"{title}_{band}_By_{author}.txt");

            File.WriteAllText(filePath, TabText);

            ShowInfoMessage("Editor", $"Successfully exported in {filePath}");
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task SaveTab()
    {
        try
        {
            await _tabService.SaveTabText(TabText, _tabRows);
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    } 
    
    private async Task MakeBackup()
    {
        try
        {
            await _tabService.MakeBackup(TabText, _tabRows);
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    } 

    private async Task DeleteTab()
    {
        try
        {
            if (ShowOkNoMessage("Editor", "Are you sure want to delete your tab?") == true)
            {
                if (ShowConfirmMessage("Editor", "Confirm this by writing your name.") == UserService.SavedUser.Name)
                {
                    await _tabService.DeleteTab();
                    NavigationService.NavigateTo<SearchControlViewModel>();
                }
                else { ShowMessage("Editor", "Wrong name of user"); }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private async Task GoToMainPage()
    {
        if (ShowOkNoMessage("Editor", "Do you want to save your tab before exit?") == true) await SaveTab();
        NavigationService.NavigateTo<SearchControlViewModel>();
    }

    private void HandleKeyPress(string? symbol)
    {
        try
        {
            if (symbol == null) return;
            if (symbol == "insert") { SwitchInsert(); return; }
            _tabRows[_cursorRow][_cursorLine][_cursorBar] = _tabRows[_cursorRow][_cursorLine][_cursorBar]
                .Remove(_cursorPosition, 1)
                .Insert(_cursorPosition, symbol!);

            if (!Insert) MoveCursor("Right");

            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void ResetCursor()
    {
        _cursorLine = 0;
        _cursorBar = 0;
    }

    private void MoveCursor(string? direction)
    {
        try
        {
            switch (direction)
            {
                case "Up":
                    if (_cursorLine <= 0)
                    {
                        SwitchCursorRow(direction);
                        break;
                    }
                    CursorLine--; break;

                case "Down":
                    if (_tabRows[_cursorRow][_cursorLine][0] == "   " && _tabRows[_cursorRow][_cursorLine + 1][0] != "   " && _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 1 == _cursorPosition) return;
                    if (_cursorLine >= _tunings.Count - 2)
                    {
                        SwitchCursorRow(direction);
                        break;
                    }
                    CursorLine++;
                    break;

                case "Left":
                    if (_cursorPosition <= 0)
                    {
                        SwitchCursorBar(direction);
                        break;
                    }
                    CursorPosition--; break;

                case "Right":
                    if (_tabRows[_cursorRow][_cursorLine][0] == "   " && _cursorPosition >= _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 1 ||
                        _tabRows[_cursorRow][_cursorLine][0] != "   " && _cursorPosition >= _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2)
                    {
                        SwitchCursorBar(direction);
                        break;
                    }
                    CursorPosition++; break;

            }
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void SwitchCursorRow(string direction)
    {
        try
        {
            switch (direction)
            {
                case "Up":
                    if (_cursorRow == 0) return;
                    _cursorRow--;
                    _cursorLine = _tunings.Count - 2;
                    if (_tabRows[_cursorRow][_cursorLine].Count - 1 < _cursorBar)
                    {
                        _cursorBar = _tabRows[_cursorRow][_cursorLine].Count - 1;
                        _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
                    }
                    if ((_tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2) < _cursorPosition) { _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2; };
                    break;
                case "Down":
                    if (_cursorRow == _tabRows.Count - 1) return;
                    _cursorRow++;
                    _cursorLine = 0;
                    if (_tabRows[_cursorRow][_cursorLine].Count - 1 < _cursorBar)
                    {
                        _cursorBar = _tabRows[_cursorRow][_cursorLine].Count - 1;
                        _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
                    }
                    if (_tabRows[_cursorRow][_cursorLine][_cursorBar].Length < _cursorPosition) _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void SwitchCursorBar(string direction)
    {
        try
        {
            switch (direction)
            {
                case "Left":
                    if (_cursorBar <= 1) return;
                    _cursorBar--;
                    if (_tabRows[_cursorRow][_cursorLine][0] == "   ")
                    {
                        _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 1;
                        break;
                    }
                    _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
                    break;

                case "Right":
                    if (_cursorBar == _tabRows[_cursorRow][_cursorLine].Count - 1) return;
                    _cursorBar++;
                    _cursorPosition = 0;
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }


    private void RemoveSymbol()
    {
        try
        {
            if (!Insert) MoveCursor("Left");

            string symbol = _tabRows[_cursorRow][_cursorLine][0] == "   " ? " " : "-";

            _tabRows[_cursorRow][_cursorLine][_cursorBar] = _tabRows[_cursorRow][_cursorLine][_cursorBar]
                .Remove(_cursorPosition, 1)
                .Insert(_cursorPosition, symbol!);

            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void UpdateTabText()
    {
        try
        {
            List<string> lines = new();

            if (IsFocused)
            {
                List<List<List<string>>> tabRowsCopy = _tabRows
                    .Select(row => row
                        .Select(stringsBars => stringsBars.ToList())
                        .ToList())
                    .ToList();

                List<List<string>> tabStringsBarsCopy = tabRowsCopy[CursorRow];
                List<string> tabStringBarsCopy = tabStringsBarsCopy[CursorLine + 1];
                char[] tabStringBarToPutCursor = tabStringBarsCopy![CursorBar].ToCharArray();
                tabStringBarToPutCursor[CursorPosition] = '^';
                tabRowsCopy[CursorRow][CursorLine + 1][CursorBar] = new(tabStringBarToPutCursor);

                foreach (var row in tabRowsCopy)
                {
                    foreach (var stringsBars in row)
                    {
                        lines.Add(string.Join("", stringsBars));
                    }
                }

                TabText = string.Join("\n", lines);
                return;
            }

            foreach (var row in _tabRows)
            {
                foreach (var stringsBars in row)
                {
                    lines.Add(string.Join("", stringsBars));
                }
            }

            TabText = string.Join("\n", lines);
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void CreateRow()
    {
        try
        {
            List<List<string>> stringsBars = CreateStrings(_tunings);
            stringsBars = CreateStringsBars(stringsBars);
            _tabRows.Add(stringsBars);
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void RemoveRow()
    {
        try
        {
            int cursorRow = _cursorRow;
            if (_tabRows.Count - 1 <= 0) return;
            if (_cursorRow > 0) SwitchCursorRow("Up");
            _tabRows.RemoveAt(cursorRow);
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void CreateBar()
    {
        try
        {
            List<List<string>> stringsBars = CreateStringsBars(_tabRows[_cursorRow]);
            _tabRows[_cursorRow] = stringsBars;
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void RemoveBar()
    {
        try
        {
            if (_tabRows[_cursorRow][_cursorLine].Count - 1 <= 1) return;
            _tabRows[_cursorRow] = RemoveStringsBar(_tabRows[_cursorRow]);
            if (_cursorBar > 1) SwitchCursorBar("Left");
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
    
    private void ExtendBar()
    {
        try
        {
            _tabRows[_cursorRow] = EditLastColumnOfBar(true, _tabRows[_cursorRow]);
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private void ShortenBar()
    {
        try
        {
            int lengthBeforeRemoving = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
            if (_tabRows[_cursorRow][_cursorLine][_cursorBar].Length <= 2) return;
            _tabRows[_cursorRow] = EditLastColumnOfBar(false, _tabRows[_cursorRow]);
            if (_cursorPosition >= lengthBeforeRemoving) MoveCursor("Left");
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }

    private List<List<string>> EditLastColumnOfBar(bool addOrRemove, List<List<string>> stringsBarsToEdit)
    {
        try
        {
            if (addOrRemove)
            {
                for (int j = 0; j < stringsBarsToEdit.Count; j++)
                {
                    if (string.IsNullOrWhiteSpace(stringsBarsToEdit[j][CursorBar]))
                    {
                        stringsBarsToEdit[j][_cursorBar] = stringsBarsToEdit[j][_cursorBar].Insert(stringsBarsToEdit[j][CursorBar].Length - 1, " ");
                        continue;
                    }
                    stringsBarsToEdit[j][_cursorBar] = stringsBarsToEdit[j][_cursorBar].Insert(stringsBarsToEdit[j][_cursorBar].Length - 1, "-");
                    continue;
                }
                return stringsBarsToEdit;
            }
            else
            {
                for (int j = 0; j < stringsBarsToEdit.Count; j++)
                {
                    stringsBarsToEdit[j][_cursorBar] = stringsBarsToEdit[j][_cursorBar].Remove(stringsBarsToEdit[j][CursorBar].Length - 2, 1);
                    continue;
                }
                return stringsBarsToEdit;
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
            return new();
        }
    }
    
    private List<List<string>> CreateStrings(List<Tuning> tuningsOfStrings)
    {
        try
        {
            List<List<string>> strings = [];

            foreach (var tuning in tuningsOfStrings)
            {
                if (tuning.StringOrder > 0 && tuning.StringOrder != tuningsOfStrings.Count)
                {
                    List<string> stringBars = [tuning.StringNote + "|"];
                    strings.Add(stringBars);
                }
                else
                {
                    List<string> stringBars = ["   "];
                    strings.Add(stringBars);
                }
            }
            return strings;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
            return new();
        }
    }

    private List<List<string>> CreateStringsBars(List<List<string>> stringsBarsToAddBars)
    {
        try
        {
            for (int j = 0; j < stringsBarsToAddBars.Count; j++)
            {
                for (int i = 0; i < BarAmount; i++)
                {
                    if (string.IsNullOrWhiteSpace(stringsBarsToAddBars[j][i]))
                    {
                        stringsBarsToAddBars[j].Add(new string(' ', LineLength + 1));
                        continue;
                    }
                    stringsBarsToAddBars[j].Add(new string('-', LineLength) + "|");
                }
            }
            return stringsBarsToAddBars;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);

            return new();
        }
    }
    
    private List<List<string>> RemoveStringsBar(List<List<string>> stringsBarsToRemoveBar)
    {
        try
        {
            foreach (var stringBars in stringsBarsToRemoveBar)
            {
                stringBars.RemoveAt(_cursorBar);
            }
            return stringsBarsToRemoveBar;
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);

            return new();
        }
    }


    private async void CreateTabText()
    {
        try
        {
            if (TabService.SavedTab is null) throw new ArgumentNullException(nameof(TabService.SavedTab));
            if (_tunings.Count == 0)
            {
                _tunings = await _tuningService.GetAllTunings(TabService.SavedTab);
                _tunings = _tunings.OrderBy(s => s.StringOrder).ToList();
            }


            if (TabService.SavedTab.TabText != string.Empty)
            {
                TabText = TabService.SavedTab.TabText;
                _tabRows = JsonSerializer.Deserialize<List<List<List<string>>>>(TabService.SavedTab.JsonTabText)!;
                if (_tabRows is null) throw new ArgumentNullException(nameof(_tabRows));
                UpdateTabText();
                return;
            }
            CreateRow();
            UpdateTabText();
        }
        catch (Exception ex)
        {
            ShowMessage("Exception", ex.Message);
        }
    }
}
