using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using View.Helpers;

namespace View.ViewModel
{
    public class TreeOrganizedByNameViewModel : BaseTreeDataViewModel
    {
        private string _searchTerm;
        public override string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                RaisePropertyChanged(() => TreeData);
            }
        }

        private ObservableCollection<Machine> _treeData;
        public ObservableCollection<Machine> TreeData
        {
            get
            {
                return string.IsNullOrEmpty(SearchTerm) ? _treeData.OrderBy(m => m.MachineName).ToObservableCollection() : FilteredTreeData;
            }
            set
            {
                _treeData = value;
                RaisePropertyChanged(() => TreeData);
            }
        }

        private ObservableCollection<Machine> FilteredTreeData
        {
            get
            {
                ObservableCollection<Machine> result = new ObservableCollection<Machine>();

                foreach (var machine in _treeData)
                {
                    if (machine.MachineName.Contains(SearchTerm))
                    {
                        result.Add(machine);
                    }
                    else
                    {
                        var compositeDeviceList = machine.Devices.Where(device => device.DeviceName.Contains(SearchTerm)).ToList();
                        if (compositeDeviceList.Any())
                        {
                            var newMachine = machine;
                            newMachine.Devices = compositeDeviceList;
                            result.Add(newMachine);
                        }
                    }
                }

                return result.OrderBy(m => m.MachineName).ToObservableCollection();
            }
        }

        public TreeOrganizedByNameViewModel(IList<Machine> machines)
        {
            TreeData = new ObservableCollection<Machine>(machines);
        }

        public override void FilterData(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}