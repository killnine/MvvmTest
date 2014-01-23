using GalaSoft.MvvmLight;

namespace View.ViewModel
{
    public abstract class BaseTreeDataViewModel : ViewModelBase
    {
        public abstract string SearchTerm { get; set; }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public abstract void FilterData(string searchTerm);
    }
}