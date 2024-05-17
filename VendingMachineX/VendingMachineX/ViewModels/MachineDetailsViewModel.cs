using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Classes;
using VendingMachineX.Services;
using VendingMachineX.Models;
using Microcharts;
using SkiaSharp;
using System.Runtime.CompilerServices;
using System.Linq;

#pragma warning disable CS4014

namespace VendingMachineX.ViewModels
{
    public class MachineDetailsViewModel : INotifyPropertyChanged
    {
        private Machine _machine;
        public Machine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                OnPropertyChanged();
            }
        }
        private readonly IFirestoreService _firestoreService;
        public event PropertyChangedEventHandler PropertyChanged;
        public MachineDetailsViewModel()
        {
            _firestoreService = new FirestoreService();
            LoadMachineData();
        }
        public MachineDetailsViewModel(String serialNumber)
        {
            _firestoreService = new FirestoreService();
            LoadMachineData(serialNumber);
        }

        public MachineDetailsViewModel(IFirestoreService firestoreService,String serialNumber)
        {
            _firestoreService = firestoreService;
            LoadMachineData(serialNumber);
        }

        public async Task LoadMachineData(String serialNumber)
        {
            Machine = new Machine(await _firestoreService.GetMachine(serialNumber));
        }
        public void LoadMachineData()
        {
            Machine = new Machine();
        }   

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
