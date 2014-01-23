using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;

namespace View.ViewModel
{
    public class DeviceTypeNode
    {
        public IDictionary<string, Device> Devices { get; set; }
        public string DeviceType { get; set; }
    }

    public class TreeOrganizedByDeviceTypeViewModel : BaseTreeDataViewModel
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

        private ObservableCollection<DeviceTypeNode> _treeData;
        public ObservableCollection<DeviceTypeNode> TreeData
        {
            get
            {
                return string.IsNullOrEmpty(SearchTerm) ? _treeData : FilteredTreeData;
            }
            set
            {
                _treeData = value;
                RaisePropertyChanged(() => TreeData);
            }
        }

        private ObservableCollection<DeviceTypeNode> FilteredTreeData
        {
            get
            {
                ObservableCollection<DeviceTypeNode> result = new ObservableCollection<DeviceTypeNode>();

                foreach (var node in _treeData)
                {
                    if (node.DeviceType.Contains(SearchTerm))
                    {
                        result.Add(node);
                    }
                    else
                    {
                        var newStatusNode = new DeviceTypeNode {DeviceType = node.DeviceType};
                        IDictionary<string, Device> compositeDeviceList = new Dictionary<string, Device>();
                        foreach (var kvp in node.Devices)
                        {
                            if (kvp.Value.DeviceName.Contains(SearchTerm))
                            {
                                compositeDeviceList.Add(kvp.Key, kvp.Value);
                            }
                        }

                        if (compositeDeviceList.Any())
                        {
                            newStatusNode.Devices = compositeDeviceList;
                            result.Add(newStatusNode);
                        }
                    }
                }

                return result;
            }
        }

        public TreeOrganizedByDeviceTypeViewModel(IList<Machine> machines)
        {
            _treeData = new ObservableCollection<DeviceTypeNode>();
            foreach (Machine machine in machines)
            {
                if (machine.Devices != null)
                    foreach (Device device in machine.Devices)
                    {
                        if (_treeData.Any(t => t.DeviceType == device.DeviceType))
                        {
                            var deviceTypeNode = _treeData.First(t => t.DeviceType == device.DeviceType);
                            deviceTypeNode.Devices.Add(string.Format("{0} - {1}", machine.MachineName, device.DeviceName), device); //NOTE: In real application, this is actually just LocationId, need to look up meaningful location name
                        }
                        else
                        {
                            _treeData.Add(new DeviceTypeNode
                                {
                                    DeviceType = device.DeviceType, 
                                    Devices = new Dictionary<string, Device>()
                                        {
                                            { string.Format("{0} - {1}",machine.MachineName, device.DeviceName), device }
                                        }
                                });
                        }
                    }
            }
        }

        public override void FilterData(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
