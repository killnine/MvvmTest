using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;
using Model;

namespace View.ViewModels
{
    public class SystemsViewModel : ViewModelBase
    {
        public ICommand AddSystemCommand { get { return new RelayCommand(AddSystem);}}
        public ICommand RemoveSystemCommand { get { return new RelayCommand(RemoveSystem, CanRemoveSystem);} }
        public ICommand SaveSystemCommand { get { return new RelayCommand(SaveSystem, CanSaveSystem);} }

        private IList<AutomationSystem> _systems;
        public IList<AutomationSystem> Systems
        {
            get { return _systems; }
            set 
            {
                _systems = value;
                RaisePropertyChanged(() => this.Systems);
            }
        }

        private AutomationSystem _selectedSystem;
        public AutomationSystem SelectedSystem
        {
            get { return _selectedSystem; }
            set
            {
                _selectedSystem = value;
                RaisePropertyChanged(() => SelectedSystem);
            }
        }

        public SystemsViewModel()
        {
            if (!IsInDesignMode)
            {
                RefreshSystems();
            }
            else
            {
                Systems = new List<AutomationSystem> { new AutomationSystem { SystemName = "Test System Name", Description = "Sample Description for a Small System"}};
            }
        }

        private void AddSystem(object obj)
        {
            return;
        }

        private bool CanRemoveSystem(object arg)
        {
            return SelectedSystem != null;
        }

        private void RemoveSystem(object obj)
        {
            return;
        }

        private bool CanSaveSystem(object arg)
        {
            return SelectedSystem != null;
        }

        public void SaveSystem(object obj)
        {
            return;
        }

        private void RefreshSystems(int selectedSystemId = -1)
        {
            return;
        }
    }
}
