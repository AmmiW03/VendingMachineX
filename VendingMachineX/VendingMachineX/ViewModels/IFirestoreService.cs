using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Classes;
using VendingMachineX.Models;

namespace VendingMachineX.ViewModels
{
    public interface IFirestoreService
    {
        Task<List<MachineData>> GetMachines();
        Task<Machine> GetMachine(String serialNumber);
        Task<List<MachineTask>> GetMachineTasks();
        Task MarkAsCompleted(MachineTask task);
        Task<bool> AddMachine(Machine machine);
        Task<int> GetProductsCount(String serialNumber);
        Task AddProducts(String serialNumber, Product product);
        Task<List<SalePointHistory>> ConsultHistory(String serialNumber);
    }
}
