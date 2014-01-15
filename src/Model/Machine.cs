using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Machine
    {
        private IList<DeviceStatus> _devices = new List<DeviceStatus>();

        public string Location { get; set; }
        public string MachineName { get; set; }
        public IList<DeviceStatus> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }
        public Status Status
        {
            get
            {
                if (Devices == null || Devices.Count == 0)
                {
                    return Status.Unknown;
                }

                return Devices.Any(d => d.Status == Status.Disconnected) ? Status.Disconnected : Status.Connected;
            }
        }
    }
}
