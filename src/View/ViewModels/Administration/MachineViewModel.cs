using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace View.ViewModels.Administration
{
    public class MachineViewModel : ViewModelBase
    {
        public ICommand AddMachineCommand { get { return new RelayCommand(AddMachine);}}
        public ICommand RemoveMachineCommand { get { return new RelayCommand(RemoveMachine, CanRemoveMachine);} }
        public ICommand SaveMachineCommand { get { return new RelayCommand(SaveMachine, CanSaveMachine); } }

        private ObservableCollection<Machine> _machines;
        public ObservableCollection<Machine> Machines
        {
            get { return _machines; }
            set 
            { 
                _machines = value;
                RaisePropertyChanged(() => Machines);
            }
        }

        private IObservable<Machine> _selectedMachine;
        public IObservable<Machine> SelectedMachine
        {
            get { return _selectedMachine; }
            set 
            { 
                _selectedMachine = value;
                RaisePropertyChanged(() => SelectedMachine);
            }
        }

        private void SaveMachine()
        {
            return;
        }

        private bool CanSaveMachine()
        {
            return false;
        }

        private void RemoveMachine()
        {
            return;
        }

        private bool CanRemoveMachine()
        {
            return false;
        }

        private void AddMachine()
        {
            return;
        }
    }
}
