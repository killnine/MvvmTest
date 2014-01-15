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

        private IList<Machine> _machines; 
        public IList<Machine> Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                RaisePropertyChanged(() => this.Machines);
            }
        }

        private BaseTreeDataViewModel _treeViewModel;
        public BaseTreeDataViewModel TreeViewModel
        {
            get { return _treeViewModel; }
            set
            {
                _treeViewModel = value;
                RaisePropertyChanged(() => this.TreeViewModel);
            }
        }

        public MachineStatusViewModel()
        {
            var _green01Devices = new DeviceStatus[]
                {
                    new DeviceStatus { DeviceName = "Data Collection System", Status = Status.Connected, DeviceType = "Data Collection"},
                    new DeviceStatus { DeviceName = "Stacker A", Status = Status.Connected, DeviceType  = "Allen-Bradley Stacker"},
                    new DeviceStatus { DeviceName = "Stacker B", Status = Status.Connected, DeviceType  = "Allen-Bradley Stacker" },
                    new DeviceStatus { DeviceName = "Stacker C", Status = Status.Connected, DeviceType  = "Allen-Bradley Stacker" },
                    new DeviceStatus { DeviceName = "Stacker D", Status = Status.Connected, DeviceType  = "Allen-Bradley Stacker" },
                    new DeviceStatus { DeviceName = "Palletizer A", Status = Status.Connected, DeviceType  = "Allen-Bradley Palletizer" },
                    new DeviceStatus { DeviceName = "Palletizer B", Status = Status.Connected, DeviceType  = "Allen-Bradley Palletizer" },
                    new DeviceStatus { DeviceName = "Palletizer C", Status = Status.Connected, DeviceType  = "Allen-Bradley Palletizer" },
                    new DeviceStatus { DeviceName = "Palletizer D", Status = Status.Connected, DeviceType  = "Allen-Bradley Palletizer" },               
                };

            var _yellow01Devices = new DeviceStatus[]
                {
                    new DeviceStatus {DeviceName = "Entry-Level Data Collection System", Status = Status.Connected, DeviceType = "Data Collection"}
                };

            var _red01Devices = new DeviceStatus[]
                {
                    new DeviceStatus { DeviceName = "OCS Console", Status = Status.Connected, DeviceType = "Horner Stacker"} 
                };

            _machines = new Machine[]
                {
                    new Machine { Devices = _green01Devices, MachineName = "Green 01", Location = "Baltimore" }, 
                    new Machine { MachineName = "Green 02", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 03", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 04", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 05", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 06", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 07", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 08", Location = "Baltimore"}, 
                    new Machine { MachineName = "Green 09", Location = "Baltimore"},
                    new Machine { MachineName = "Yellow 01", Location = "Paris", Devices = _yellow01Devices},
                    new Machine { MachineName = "Blue 01", Location = "Louisville"},
                    new Machine { MachineName = "Red 01", Location = "Chicago", Devices = _red01Devices}, 
                    new Machine { MachineName = "Orange 01", Location = "New York" } 
                };

            Machines = _machines;

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
