using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using View.Annotations;

namespace View.ViewModel
{
    public class TreeOrganizedByLocationViewModel : BaseTreeDataViewModel, INotifyPropertyChanged
    {
        private string _searchTerm;
        public override string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged("TreeData");
            }
        }

        private ObservableCollection<LocationNode> _treeData;
        public ObservableCollection<LocationNode> TreeData
        {
            get
            {
                return string.IsNullOrEmpty(SearchTerm) ? _treeData : FilteredTreeData;
            }
            set
            {
                _treeData = value;
                OnPropertyChanged("TreeData");
            }
        }

        private ObservableCollection<LocationNode> FilteredTreeData
        {
            get
            {
                ObservableCollection<LocationNode> result = new ObservableCollection<LocationNode>();

                foreach (var node in _treeData)
                {
                    if (node.Location.Contains(SearchTerm))
                    {
                        result.Add(node);
                    }
                    else
                    {
                        var newStatusNode = new LocationNode { Location = node.Location };
                        var compositeMachinesList = new List<Machine>();
                        foreach (var machine in node.Machines)
                        {
                            if (machine.MachineName.Contains(SearchTerm))
                            {
                                compositeMachinesList.Add(machine);
                            }
                            else
                            {
                                var compositeDeviceList = machine.Devices.Where(device => device.DeviceName.Contains(SearchTerm)).ToList();

                                if (compositeDeviceList.Any())
                                {
                                    var newMachine = machine;
                                    newMachine.Devices = compositeDeviceList;
                                    compositeMachinesList.Add(newMachine);
                                }
                            }
                        }

                        if (compositeMachinesList.Any())
                        {
                            newStatusNode.Machines = compositeMachinesList;
                            result.Add(newStatusNode);
                        }
                    }
                }

                return result;
            }
        }

        public TreeOrganizedByLocationViewModel(IList<Machine> machines)
        {
            _treeData = new ObservableCollection<LocationNode>();
            
            foreach (Machine machine in machines)
            {
                if (_treeData.Any(l => l.Location == machine.Location))
                {
                    var locationNode = _treeData.First(l => l.Location == machine.Location);
                    locationNode.Machines.Add(machine);
                }
                else
                {
                    _treeData.Add(new LocationNode { Location = machine.Location, Machines = new List<Machine> { machine }});
                }
            }

            OnPropertyChanged("TreeData");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public override void FilterData(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}