using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel, ITrackElementFocus
{
    private readonly TuningService _tuningService;
    private readonly TabService _tabService;

    private int _cursorPosition = 3;
    private int _cursorLine = 2;
    private int _cursorRow = 0;
    private int _cursorBar = 1;

    private bool _insert = false;
    private bool _isFocused;
    private readonly Task _initializedTask;
    private List<Tuning> _tunings = new();
    private List<List<List<string>>> _tabRows = new();
    private string _tabText;
    private int _zoom = 100;
    private double _fontSize = 33.3;
    private int _lineLength = 20;
    private int _barAmount = 2;

    public BaseViewModel ControlBarViewModel { get; private set; }

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

    public bool Insert 
    { 
        get => _insert;
        set
        {
            _insert = value;
            OnPropertyChanged();
        } 
    }

    public ICommand GoToMainPageCommand { get; }
    public ICommand HandleKeyCommand { get; }
    public ICommand CreateBarsButtonCommand { get; }
    public ICommand RemoveBarButtonCommand { get; }
    public ICommand CreateLineButtonCommand { get; }
    public ICommand RemoveLineButtonCommand { get; }
    public ICommand MoveCursorCommand { get; }
    public ICommand WriteNumberCommand { get; }
    public ICommand RemoveSymbolCommand { get; }

    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService, TuningService tuningService, TabService tabService) : base(viewModelService, navigationService)
    {
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        _tuningService = tuningService;
        _tabService = tabService;
        Task.Run(CreateTabText);
        CreateLineButtonCommand = new RelayCommand(CreateRow);
        RemoveLineButtonCommand = new RelayCommand(RemoveRow);
        CreateBarsButtonCommand = new RelayCommand(CreateBar);
        RemoveBarButtonCommand = new RelayCommand(RemoveBar);
        GoToMainPageCommand = new AsyncRelayCommand(GoToMainPage);
        MoveCursorCommand = new RelayCommand<string>(MoveCursor);
        HandleKeyCommand = new RelayCommand<string>(HandleKeyPress);
        RemoveSymbolCommand = new RelayCommand(RemoveSymbol);
    }

    private async Task GoToMainPage()
    {
        if(ShowOkCancelMessage("Editor", "Do yo want to save your tab before exit?") == MessageBoxResult.OK) await _tabService.SaveTabText(TabText, _tabRows);
        NavigationService.NavigateTo<SearchControlViewModel>();
    }

    private void HandleKeyPress(string? symbol)
    {
        _tabRows[_cursorRow][_cursorLine][_cursorBar] = _tabRows[_cursorRow][_cursorLine][_cursorBar]
            .Remove(_cursorPosition, 1)
            .Insert(_cursorPosition, symbol!);

        if (!Insert) MoveCursor("Right");

        UpdateTabText();
    }

    private void ResetCursor()
    {
        _cursorLine = 0;
        _cursorBar = 0;
    }

    private void MoveCursor(string? direction)
    {
        switch (direction) 
        {
            case "Up":
                if (_tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 1 == _cursorPosition) return;
                if (_cursorLine <= 0)
                {
                    SwitchCursorRow(direction);
                    break;
                }
                CursorLine--; break;

            case "Down":
                if (_tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 1 == _cursorPosition) return;
                if (_cursorLine >= _tunings.Count-2)
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

    private void SwitchCursorRow(string direction)
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
                if (_tabRows[_cursorRow][_cursorLine][_cursorBar].Length < _cursorPosition) _cursorPosition = _tabRows[_cursorRow][_cursorLine][_cursorBar].Length - 2;
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
    
    private void SwitchCursorBar(string direction)
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


    private void RemoveSymbol()
    {
        if (!Insert) MoveCursor("Left");

        string symbol = _tabRows[_cursorRow][_cursorLine][0] == "   " ? " " : "-";

        _tabRows[_cursorRow][_cursorLine][_cursorBar] = _tabRows[_cursorRow][_cursorLine][_cursorBar]
            .Remove(_cursorPosition, 1)
            .Insert(_cursorPosition, symbol!);

        UpdateTabText();
    }

    private void UpdateTabText()
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
                foreach(var stringsBars in row)
                {
                    lines.Add(string.Join("", stringsBars));
                }
            }

            TabText = string.Join("\n", lines);
            return;
        }

        foreach(var row in _tabRows)
        {
            foreach(var stringsBars in row)
            {
                lines.Add(string.Join("", stringsBars));
            }
        }

        TabText = string.Join("\n", lines);
    }
    
    private void CreateRow()
    {
        List<List<string>> stringsBars = CreateStrings(_tunings);
        stringsBars = CreateStringsBars(stringsBars);
        _tabRows.Add(stringsBars);
        UpdateTabText();
    }
    
    private void RemoveRow()
    {
        int cursorRow = _cursorRow;
        if (_cursorRow == 0) return;
        if (_cursorRow == _tabRows.Count - 1) SwitchCursorRow("Up"); 
        _tabRows.RemoveAt(cursorRow);
        UpdateTabText();
    }

    private void CreateBar()
    {
        List<List<string>> stringsBars = CreateStringsBars(_tabRows[_cursorRow]);
        _tabRows[_cursorRow] = stringsBars;
        UpdateTabText();
    }

    private void RemoveBar()
    {
        int tabRowsCountBeforeRemoving = _tabRows[_cursorRow][_cursorLine].Count - 1;
        if (_tabRows[_cursorRow][_cursorLine].Count <= 1) return;
        _tabRows[_cursorRow] = RemoveStringsBar(_tabRows[_cursorRow]);
        if (_cursorBar == tabRowsCountBeforeRemoving) SwitchCursorBar("Left");
        UpdateTabText();
    }

    private List<List<string>> CreateStrings(List<Tuning> tuningsOfStrings)
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

    private List<List<string>> CreateStringsBars(List<List<string>> stringsBarsToAddBars)
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
    
    private List<List<string>> RemoveStringsBar(List<List<string>> stringsBarsToRemoveBar)
    {
        foreach(var stringBars in stringsBarsToRemoveBar)
        {
            stringBars.RemoveAt(_cursorBar);
        }
        return stringsBarsToRemoveBar;
    }


    private async Task CreateTabText()
    {
        if (TabService.SavedTab is null) throw new ArgumentNullException(nameof(TabService.SavedTab));
        if(_tunings.Count == 0)
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
}
