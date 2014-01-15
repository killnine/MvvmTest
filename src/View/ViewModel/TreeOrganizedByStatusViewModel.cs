using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model;
using View.Annotations;

namespace View.ViewModel
{
    public class TreeOrganizedByStatusViewModel : BaseTreeDataViewModel, INotifyPropertyChanged
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

        private ObservableCollection<StatusNode> _treeData;
        public ObservableCollection<StatusNode> TreeData
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

        private ObservableCollection<StatusNode> FilteredTreeData
        {
            get
            {
                ObservableCollection<StatusNode> result = new ObservableCollection<StatusNode>();

                foreach (var node in _treeData)
                {
                    if (node.Status.ToString().Contains(SearchTerm))
                    {
                        result.Add(node);
                    }
                    else
                    {
                        var newStatusNode = new StatusNode {Status = node.Status};
                        var compositeMachinesList = new List<Machine>();
                        foreach (var machine in node.Machines)
                        {
                            if (machine.MachineName.Contains(SearchTerm))
                            {
                                compositeMachinesList.Add(machine);
                            }
                            else
                            {
                                var compositeDeviceList = machine.Devices.Where(device => device.DeviceName.Contains(SearchTerm));

                                if (compositeDeviceList.Any())
                                {
                                    var newMachine = machine;
                                    newMachine.Devices = compositeDeviceList.ToList();
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

        public TreeOrganizedByStatusViewModel(IList<Machine> machines)
        {
            _treeData = new ObservableCollection<StatusNode>();
            foreach (Status status in Enum.GetValues(typeof (Status)))
            {
                _treeData.Add(new StatusNode
                    {
                        Status = status,
                        Machines = machines.Where(m => m.Status == status).ToList()
                    });
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