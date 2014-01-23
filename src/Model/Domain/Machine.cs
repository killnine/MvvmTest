using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Machine
    {
        private IList<Device> _devices = new List<Device>();

        public int MachineId { get; set; }
        public string Location { get; set; }
        public string MachineName { get; set; }
        public IList<Device> Devices
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
