using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Models;
using VendingMachineX.Services;

namespace VendingMachineX.ViewModels
{
    public class ProductsViewModel
    {
        private int _productsCount;

        public int ProductsCount
        { 
            get => _productsCount;
            set
            {
                _productsCount = value;
                OnPropertyChanged();
            }
        }

        private readonly IFirestoreService _firestoreService;
        public event PropertyChangedEventHandler PropertyChanged;

        public ProductsViewModel()
        {
            _firestoreService = new FirestoreService();
        }
        public ProductsViewModel(String serialNumber)
        {
            _firestoreService = new FirestoreService();
            LoadProductsCount(serialNumber);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadProductsCount(String serialNumber)
        {
            ProductsCount = await _firestoreService.GetProductsCount(serialNumber);
        }

        public async Task AddProducts(String serialNumber, Product product)
        {
            await _firestoreService.AddProducts(serialNumber, product);
        }
    }
}
