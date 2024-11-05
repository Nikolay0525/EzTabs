using CommunityToolkit.Mvvm.Input;
using EzTabs.Services.ModelServices;
using EzTabs.ViewModel.BaseViewModels;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace EzTabs.ViewModel.MainControlsViewModels
{
    public class TabCreationControlViewModel : BaseViewModel
    {
        private UserService _userService;
        private TabService _tabService;

        private string? _title;
        private string? _band;
        private string? _genre;
        private string? _key;
        private int _bpm;
        private string? _description;
        private int _stringOrder;
        private string? _stringNote;
        private Dictionary<int, string> _tunings = new();

        private ObservableCollection<string> _listOfTunings = new();

        private string? _selectedItem;
        public string? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                OnSelecting();
            }
        }

        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string? Band
        {
            get => _band;
            set
            {
                _band = value;
                OnPropertyChanged(nameof(Band));
            }
        }
        public string? Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }
        public string? Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }
        public int BitsPerMinute
        {
            get => _bpm;
            set
            {
                _bpm = value;
                OnPropertyChanged(nameof(BitsPerMinute));
                OnPropertyChanged(nameof(BitsPerMinuteText));
            }
        }
        public string BitsPerMinuteText => $"{BitsPerMinute} BPM";

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        
        public int StringOrder
        {
            get => _stringOrder;
            set
            {
                _stringOrder = value;
                OnPropertyChanged(nameof(StringOrder));
            }
        }
        
        public string? StringNote
        {
            get => _stringNote;
            set
            {
                _stringNote = value;
                OnPropertyChanged(nameof(StringNote));
            }
        }

        public ObservableCollection<string> ListOfTunings
        {
            get => _listOfTunings;
            set => _listOfTunings = value;
        }

        public ICommand AddTuningCommand { get; }
        public ICommand EditTuningCommand { get; }
        public ICommand RemoveTuningCommand { get; }

        public TabCreationControlViewModel() 
        {
            AddTuningCommand = new RelayCommand(AddTuning);
            EditTuningCommand = new RelayCommand(EditTuning);
            RemoveTuningCommand = new RelayCommand(RemoveTuning);
            _userService = new UserService();
            _tabService = new TabService();
        }

        private void OnSelecting()
        {
            if(SelectedItem is null) return;
            var selectedItem = SelectedItem.Split(":");
            if(int.TryParse(selectedItem[0], out int result) != default)
            {
                StringOrder = result;
                StringNote = selectedItem[1];
            }
            else { throw new ArgumentException(nameof(result) + "is default"); }
        }

        private void AddTuning()
        {
            if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
            if (ListOfTunings is null) throw new ArgumentNullException(nameof(_tunings));
            if (StringNote is null) throw new ArgumentNullException(nameof(StringNote));

            if (_tunings.ContainsKey(StringOrder))
            {
                // add message
                return;
            }
            _tunings.Add(StringOrder, StringNote);
            ListOfTunings.Clear();
            foreach (var tuning in _tunings)
            {
                ListOfTunings.Add($"{tuning.Key}: {tuning.Value}");
                OnPropertyChanged(nameof(ListOfTunings));
            }
        }

        private void EditTuning()
        {
            if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
            if (ListOfTunings is null) throw new ArgumentNullException(nameof(_tunings));
            if (StringNote is null) throw new ArgumentNullException(nameof(StringNote));

            if (_tunings.ContainsKey(StringOrder))
            {
                _tunings[StringOrder] = StringNote;
                ListOfTunings.Clear();
                foreach (var tuning in _tunings)
                {
                    ListOfTunings.Add($"{tuning.Key}: {tuning.Value}");
                    OnPropertyChanged(nameof(ListOfTunings));
                }
            } 
        }

        private void RemoveTuning()
        {
            if (_tunings is null) throw new ArgumentNullException(nameof(_tunings));
            if (ListOfTunings is null) throw new ArgumentNullException(nameof(_tunings));

            _tunings.Remove(StringOrder);
            ListOfTunings.Clear();
            foreach (var tuning in _tunings)
            {
                ListOfTunings.Add($"{tuning.Key}: {tuning.Value}");
                OnPropertyChanged(nameof(ListOfTunings));
            }
        }
    }
}
