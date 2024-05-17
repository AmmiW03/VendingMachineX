using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Classes;
using VendingMachineX.Services;
using VendingMachineX.Models;

#pragma warning disable CS4014

namespace VendingMachineX.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MachineData> _machines;
        public ObservableCollection<MachineData> Machines
        {
            get => _machines;
            set
            {
                _machines = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MachineTask> _tasks;
        public ObservableCollection<MachineTask> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IFirestoreService _firestoreService;

        public MenuViewModel()
        {
            _firestoreService = new FirestoreService();
            LoadMachines();
            LoadTask();
        }
        public MenuViewModel(IFirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
            LoadMachines();
            LoadTask();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadMachines()
        {
            Machines = new ObservableCollection<MachineData>(await _firestoreService.GetMachines());
        }
        public async Task LoadTask()
        {
            Tasks = new ObservableCollection<MachineTask>(await _firestoreService.GetMachineTasks());
        }
        public async Task MarkAsCompleted(MachineTask task)
        {
            await _firestoreService.MarkAsCompleted(task);
            Tasks.Remove(task);
        }
    }
}
