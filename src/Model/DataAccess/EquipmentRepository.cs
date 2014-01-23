using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Model.DataAccess
{
    public interface IEquipmentRepository
    {
        IList<Machine> SelectMachines();
        IList<AutomationSystem> SelectSystems();
        IList<Device> SelectDevices();
        AutomationSystem CreateSystem();
        void DeleteSystem(int systemId);
        void UpdateSystem(AutomationSystem selectedSystem);
    }

    public class EquipmentRepository : SqLiteBaseRepository, IEquipmentRepository
    {
        private static IList<Device> _green01Devices = new[]
            {
                new Device
                    {
                        DeviceName = "Data Collection System",
                        Status = Status.Connected,
                        DeviceType = "Data Collection"
                    },
                new Device {DeviceName = "Stacker A", Status = Status.Connected, DeviceType = "Allen-Bradley Stacker"},
                new Device {DeviceName = "Stacker B", Status = Status.Connected, DeviceType = "Allen-Bradley Stacker"},
                new Device {DeviceName = "Stacker C", Status = Status.Connected, DeviceType = "Allen-Bradley Stacker"},
                new Device {DeviceName = "Stacker D", Status = Status.Connected, DeviceType = "Allen-Bradley Stacker"},
                new Device
                    {
                        DeviceName = "Palletizer A",
                        Status = Status.Connected,
                        DeviceType = "Allen-Bradley Palletizer"
                    },
                new Device
                    {
                        DeviceName = "Palletizer B",
                        Status = Status.Connected,
                        DeviceType = "Allen-Bradley Palletizer"
                    },
                new Device
                    {
                        DeviceName = "Palletizer C",
                        Status = Status.Connected,
                        DeviceType = "Allen-Bradley Palletizer"
                    },
                new Device
                    {
                        DeviceName = "Palletizer D",
                        Status = Status.Connected,
                        DeviceType = "Allen-Bradley Palletizer"
                    },
            };

        private static IList<Device> _yellow01Devices = new[]
            {
                new Device
                    {
                        DeviceName = "Entry-Level Data Collection System",
                        Status = Status.Connected,
                        DeviceType = "Data Collection"
                    }
            };

        private static IList<Device> _red01Devices = new[]
            {
                new Device {DeviceName = "OCS Console", Status = Status.Connected, DeviceType = "Horner Stacker"}
            };

        private IList<Machine> _machines = new[]
            {
                new Machine {Devices = _green01Devices, MachineName = "Green 01", Location = "Baltimore"},
                new Machine {MachineName = "Green 02", Location = "Baltimore"},
                new Machine {MachineName = "Green 03", Location = "Baltimore"},
                new Machine {MachineName = "Green 04", Location = "Baltimore"},
                new Machine {MachineName = "Green 05", Location = "Baltimore"},
                new Machine {MachineName = "Green 06", Location = "Baltimore"},
                new Machine {MachineName = "Green 07", Location = "Baltimore"},
                new Machine {MachineName = "Green 08", Location = "Baltimore"},
                new Machine {MachineName = "Green 09", Location = "Baltimore"},
                new Machine {MachineName = "Yellow 01", Location = "Paris", Devices = _yellow01Devices},
                new Machine {MachineName = "Blue 01", Location = "Louisville"},
                new Machine {MachineName = "Red 01", Location = "Chicago", Devices = _red01Devices},
                new Machine {MachineName = "Orange 01", Location = "New York"}
            };

        private IList<AutomationSystem> _systems = new[]
            {
                new AutomationSystem { SystemId = 1, SystemName = "Test System 1", Description = "Test System for GUI."},
                new AutomationSystem { SystemId = 2, SystemName = "Test System 2", Description = "Test System for GUI."}
            };

        public IList<Machine> SelectMachines()
        {
            return _machines;
        }

        public IList<AutomationSystem> SelectSystems()
        {
            return _systems;
        }

        public IList<Device> SelectDevices()
        {
            return _green01Devices.Concat(_red01Devices).Concat(_yellow01Devices).ToList();
        }

        public AutomationSystem CreateSystem()
        {
            return new AutomationSystem();
        }

        public void DeleteSystem(int systemId)
        {
            return;
        }

        public void UpdateSystem(AutomationSystem selectedSystem)
        {
            throw new NotImplementedException();
        }
    }

    public class SqLiteBaseRepository
    {
        //TODO: SqlLite functionality to come in later release
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\EquipmentDb.sqlite"; }
        }

        public static SQLiteConnection Connection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }

    
}
