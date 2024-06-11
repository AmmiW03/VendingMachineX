using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Models;
using VendingMachineX.Services;
#pragma warning disable CS4014

namespace VendingMachineX.ViewModels
{
    public class SalesHistoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SalePointHistory> _salesHistory;
        public ObservableCollection<SalePointHistory> SalesHistory 
        {
            get => _salesHistory;
            set
            {
                _salesHistory = value;
                OnPropertyChanged();
            }
        }
        private readonly IFirestoreService _firestoreService;
        public event PropertyChangedEventHandler PropertyChanged;

        public SalesHistoryViewModel()
        {
            _firestoreService = new FirestoreService();
            LoadHistory();
        }
        public SalesHistoryViewModel(String serialNumber)
        {
            _firestoreService = new FirestoreService();
            LoadHistory(serialNumber);
        }
        public SalesHistoryViewModel(IFirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
            LoadHistory();
        }
        public SalesHistoryViewModel(IFirestoreService firestoreService, String serialNumber)
        {
            _firestoreService = firestoreService;
            LoadHistory(serialNumber);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadHistory(String serialNumber)
        {
            SalesHistory = new ObservableCollection<SalePointHistory>(await _firestoreService.ConsultHistory(serialNumber));
        }
        public void LoadHistory()
        {
            SalesHistory = new ObservableCollection<SalePointHistory>();
        }
    }
}
