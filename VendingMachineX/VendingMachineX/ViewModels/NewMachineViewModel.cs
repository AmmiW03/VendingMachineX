using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Models;
using VendingMachineX.Services;

namespace VendingMachineX.ViewModels
{
    public class NewMachineViewModel
    {
        private readonly IFirestoreService _firestoreService;

        public NewMachineViewModel()
        {
            _firestoreService = new FirestoreService();
        }
        public NewMachineViewModel(IFirestoreService firestoreService)
        {
            _firestoreService = new FirestoreService();
        }

        public async Task<bool> AddMachine(Machine machine)
        {
            return await _firestoreService.AddMachine(machine);
        }
    }
}
