using CommunityToolkit.Mvvm.Input;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Services.ViewModelServices;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.SimpleControlsViewModels.ControlBarPartsVMs;
using System.Windows.Input;
using System.Windows.Shapes;

namespace EzTabs.Presentation.ViewModels.MainControlsViewModels;

public class TabEditingControlViewModel : BaseViewModel
{
    public BaseViewModel ControlBarViewModel { get; private set; }

    private char? _symbolBehindCursor = null;
    private int _symbolBehindCursorPosition;
    private int _symbolBehindCursorLine;
    private int _cursorPosition = 3;
    private int _cursorLine = 3;

    private readonly TuningService _tuningService;
    private readonly Task _initializedTask;
    private List<Tuning> _tunings = new();
    private List<string> _tabLines = new();
    private string _tabText;
    private int _lineLength = 20;
    private int _barAmount = 2;

    public int CursorPosition
    {
        get => _cursorPosition;
        private set
        {
            _cursorPosition = value;
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

    public ICommand HandleKeyCommand { get; }
    public ICommand MoveCursorCommand { get; }
    public ICommand WriteNumberCommand { get; }
    public ICommand RemoveSymbolCommand { get; }

    public TabEditingControlViewModel(IViewModelService viewModelService, INavigationService navigationService, TuningService tuningService) : base(viewModelService, navigationService)
    {
        ControlBarViewModel = ViewModelService.CreateViewModel<ControlBarViewModel>();
        _tuningService = tuningService;
        UpdateTabText();
        MoveCursorCommand = new AsyncRelayCommand<string>(MoveCursor);
        HandleKeyCommand = new AsyncRelayCommand<string>(HandleKeyPress);
        RemoveSymbolCommand = new AsyncRelayCommand(RemoveSymbol);
    }

    private async Task HandleKeyPress(string? symbol)
    {
        if (_cursorPosition < _tabLines![_cursorLine].Length)
        {
            _tabLines[_cursorLine] = _tabLines[_cursorLine]
                .Remove(_cursorPosition, 1)
                .Insert(_cursorPosition, symbol!);
        }
        OnPropertyChanged(nameof(TabText));
        await UpdateTabText();
    }

    private async Task MoveCursor(string? direction)
    {
        switch (direction) 
        {
            case "Up":
                if (_cursorLine <= 0) return;
                CursorLine--; break;

            case "Down":
                if (_cursorLine >= _tabLines.Count-2) return;
                CursorLine++; 
                break;

            case "Right":
                if (_cursorPosition >= _tabLines[_cursorLine].Length-2) return;
                CursorPosition++; break;

            case "Left":
                if (_cursorPosition <= 3) return;
                CursorPosition--; break;

        }
        await UpdateTabText();
    }


    private async Task RemoveSymbol()
    {
        char[] tabLine = _tabLines![CursorLine].ToCharArray();
        tabLine[CursorPosition] = '-';
        _tabLines[CursorLine] = new(tabLine);
        await UpdateTabText();
    }

    private async Task ReplaceCursorSymbol()
    {
        if (_symbolBehindCursor == null) return;

        List<string> tabLinesCopy = _tabLines;
        char[] tabLineToPutCursor = tabLinesCopy![CursorLine + 1].ToCharArray();
        tabLineToPutCursor[CursorPosition] = '^';
        tabLinesCopy[CursorLine + 1] = new(tabLineToPutCursor);
    }

    private async Task UpdateTabText()
    {
        if(_tabLines.Count == 0) await CreateEmptyTabText();

        List<string> tabLinesCopy = _tabLines.ToList();
        char[] tabLineToPutCursor = tabLinesCopy![CursorLine+1].ToCharArray();
        tabLineToPutCursor[CursorPosition] = '^';
        tabLinesCopy[CursorLine+1] = new(tabLineToPutCursor);

        TabText = string.Join("\n", tabLinesCopy);
    }

    private async Task CreateEmptyTabText()
    {
        if (TabService.SavedTab is null) throw new ArgumentNullException(nameof(TabService.SavedTab));
        if(_tunings.Count == 0)
        {
            _tunings = await _tuningService.GetAllTunings(TabService.SavedTab);
            _tunings = _tunings.OrderBy(s => s.StringOrder).ToList();
        }

        foreach(var tuning in _tunings)
        {
            if(tuning.StringOrder > 0 && tuning.StringOrder != _tunings.Count) 
            { 
                _tabLines.Add(tuning.StringNote + "|");
            }
            else
            {
                _tabLines.Add("   ");
            }
        }

        for (int i = 0; i < BarAmount; i++)
        {
            for (int j = 0; j < _tabLines.Count; j++)
            {
                if (string.IsNullOrWhiteSpace(_tabLines[j]))
                {
                    _tabLines[j] += new string(' ', LineLength + 1);
                    continue;
                }
                _tabLines[j] += new string('-', LineLength) + "|";
            }
        }
    }
}
