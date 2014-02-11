using System.Collections.ObjectModel;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;
using Model;
using Raven.Abstractions.Data;
using Raven.Client;

namespace View.ViewModels
{
    public class SystemsViewModel : ViewModelBase
    {
        private readonly IDocumentStore _documentStore;

        public ICommand AddSystemCommand { get { return new RelayCommand(arg => AddSystem());}}
        public ICommand RemoveSystemCommand { get { return new RelayCommand(RemoveSystem, CanRemoveSystem);} }
        public ICommand SaveSystemCommand { get { return new RelayCommand(SaveSystem, CanSaveSystem);} }

        private ObservableCollection<AutomationSystem> _systems;
        public ObservableCollection<AutomationSystem> Systems
        {
            get { return _systems; }
            set 
            {
                _systems = value;
                RaisePropertyChanged(() => Systems);
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

        public SystemsViewModel(IDocumentStore documentStore)
        {
            _documentStore = documentStore;

            if (!IsInDesignMode)
            {
                RefreshSystems();
            }
            else
            {
                Systems = new ObservableCollection<AutomationSystem>
                    {
                        new AutomationSystem
                            {
                                SystemName = "Test System Name",
                                Description = "Sample Description for a Small System"
                            }
                    };
            }
        }

        private void AddSystem()
        {
            var newSystem = new AutomationSystem() {SystemName = "<New System>"};
            using (var session = _documentStore.OpenSession())
            {
                session.Store(newSystem);
                session.SaveChanges();

                Systems.Add(newSystem);
            }

            RefreshSystems();
        }

        private bool CanRemoveSystem(object arg)
        {
            return SelectedSystem != null;
        }

        private void RemoveSystem(object obj)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Delete(SelectedSystem);
                session.SaveChanges();
            }

            Systems.Remove(SelectedSystem);
            SelectedSystem = null;
        }

        private bool CanSaveSystem(object arg)
        {
            return SelectedSystem != null;
        }

        private void SaveSystem(object obj)
        {
            _documentStore.DatabaseCommands.Patch(SelectedSystem.Id, new[]
                {
                    new PatchRequest
                        {
                            Type = PatchCommandType.Set,
                            Name = "SystemName",
                            Value = SelectedSystem.SystemName
                        },
                    new PatchRequest
                        {
                            Type = PatchCommandType.Set,
                            Name = "Description",
                            Value = SelectedSystem.Description
                        }
                });
        }

        private void RefreshSystems(int selectedSystemId = -1)
        {
            using (var session = _documentStore.OpenSession())
            {
                Systems = new ObservableCollection<AutomationSystem>(session.Query<AutomationSystem>());
            }
        }
    }
}
