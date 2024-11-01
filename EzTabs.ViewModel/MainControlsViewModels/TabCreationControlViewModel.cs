using EzTabs.Services.ModelServices;
using EzTabs.ViewModel.BaseViewModels;

namespace EzTabs.ViewModel.MainControlsViewModels
{
    public class TabCreationControlViewModel : BaseViewModel
    {
        private UserService _userService;
        private TabService _tabService;

        private string _title;
        private string _band;
        private string _genre;
        private string _key;
        private int _bpm;
        private string _description;
        private int _stringNumber;
        private string _stringNote;
        private Dictionary<int, string> _tunings = new()
        {
            {1,"e"},
            {2,"B"},
            {3,"G"},
            {4,"D"},
            {5,"A"},
            {6,"E"}
        };

        private List<string> _listOfTunings = new()
        {
            {"1: e"},
            {"2: B"},
            {"3: G"},
            {"4: D"},
            {"5: A"},
            {"6: E"}
        };

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Band
        {
            get => _band;
            set
            {
                _band = value;
                OnPropertyChanged(nameof(Band));
            }
        }
        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }
        public string Key
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

        public string Description
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
            get => _stringNumber;
            set
            {
                _stringNumber = value;
                OnPropertyChanged(nameof(StringOrder));
            }
        }
        
        public string StringNote
        {
            get => _stringNote;
            set
            {
                _stringNote = value;
                OnPropertyChanged(nameof(StringNote));
            }
        }
        
        public List<string> ListOfTunings
        {
            get => _listOfTunings;
            private set
            {
                _listOfTunings = value;
                OnPropertyChanged(nameof(ListOfTunings));
            }
        }

        public TabCreationControlViewModel() 
        { 
            _userService = new UserService();
            _tabService = new TabService();
        }

        public void AddTuning(int stringOrder, string stringNote)
        {
            _tunings.Add(stringOrder, stringNote);
            ListOfTunings.Clear();
            foreach (var tuning in _tunings)
            {
                ListOfTunings.Add($"{tuning.Key}: {tuning.Value}");
            }
        }
        public void RemoveTuning()
        {
            string stringKey = SelectedItem.Split(":")[0];
            int key;
            if (int.TryParse(stringKey, out int result))
            {
                key = result;
            }
            else throw new ArgumentException("Failed to convert string to int while converting selectedItem");
            _tunings.Remove(key);
            ListOfTunings.Clear();
            foreach (var tuning in _tunings)
            {
                ListOfTunings.Add($"{tuning.Key}: {tuning.Value}");
            }
        }
    }
}
