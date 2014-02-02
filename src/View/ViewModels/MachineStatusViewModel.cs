using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;
using Model;

namespace View.ViewModel
{
    public class MachineStatusViewModel : ViewModelBase
    {
        private TreeOrganizedByNameViewModel _organizedByNameViewModel;
        private TreeOrganizedByLocationViewModel _organizedByLocationViewModel;
        private TreeOrganizedByStatusViewModel _organizedByStatusViewModel;
        private TreeOrganizedByDeviceTypeViewModel _organizedByDeviceTypeViewModel;

        public ICommand OrganizeMachinesByName { get { return new RelayCommand(OrganizeByName); }}
        public ICommand OrganizeMachinesByStatus { get { return new RelayCommand(OrganizeByStatus);} }
        public ICommand OrganizeMachinesByLocation { get { return new RelayCommand(OrganizeByLocation);} }
        public ICommand OrganizeMachinesByType { get { return new RelayCommand(OrganizeByType);} }

        private void OrganizeByName(object e)
        {
            TreeViewModel.SearchTerm = string.Empty;
            TreeViewModel = _organizedByNameViewModel;
        }
        private void OrganizeByStatus(object e)
        {
            TreeViewModel.SearchTerm = string.Empty;
            TreeViewModel = _organizedByStatusViewModel;
        }
        private void OrganizeByLocation(object e)
        {
            TreeViewModel.SearchTerm = string.Empty;
            TreeViewModel = _organizedByLocationViewModel;
        }
        private void OrganizeByType(object e)
        {
            TreeViewModel.SearchTerm = string.Empty;
            TreeViewModel = _organizedByDeviceTypeViewModel;
        }

        private IList<Machine> _machines = new List<Machine>(); 
        public IList<Machine> Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                RaisePropertyChanged(() => Machines);
            }
        }

        private BaseTreeDataViewModel _treeViewModel;
        public BaseTreeDataViewModel TreeViewModel
        {
            get { return _treeViewModel; }
            set
            {
                _treeViewModel = value;
                RaisePropertyChanged(() => TreeViewModel);
            }
        }

        public MachineStatusViewModel()
        {
            //Set up different views for the TreeView
            _organizedByDeviceTypeViewModel = new TreeOrganizedByDeviceTypeViewModel(Machines);
            _organizedByLocationViewModel = new TreeOrganizedByLocationViewModel(Machines);
            _organizedByNameViewModel = new TreeOrganizedByNameViewModel(Machines);
            _organizedByStatusViewModel = new TreeOrganizedByStatusViewModel(Machines);
            TreeViewModel = _organizedByNameViewModel;

            //Setup throttled search for items in TreeView
            var searchTermThrottledObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged");
            searchTermThrottledObservable.Where((arg) => arg.EventArgs.PropertyName.Equals("SearchTerm"))
                                         .Throttle(new TimeSpan(0, 0, 0, 0, 500))
                                         .Subscribe(pattern => TreeViewModel.FilterData(TreeViewModel.SearchTerm));
        }
    }
}
