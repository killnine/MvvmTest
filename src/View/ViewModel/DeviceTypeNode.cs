using System.Collections.Generic;
using Model;

namespace View.ViewModel
{
    public class DeviceTypeNode
    {
        public IDictionary<string, DeviceStatus> Devices { get; set; }
        public string DeviceType { get; set; }
    }
}