using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Classes;
using VendingMachineX.ViewModels;
using VendingMachineX.Models;

namespace VendingMachineX.Services
{
    internal class FirestoreService : IFirestoreService
    {
        public FirestoreService()
        {

        }

        public async Task<List<MachineData>> GetMachines()
        {
            List<MachineData> machines =  new List<MachineData>();
            try
            {
                var query = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").GetAsync();
                foreach (var document in query.Documents)
                {
                    MachineData machine = new MachineData();
                    machine.SerialNumber = document.Id;
                    foreach(var data in document.Data)
                    {
                        if (data.Key == "Latitud")
                        {
                            machine.Latitude = data.Value.ToString();
                        }
                        if(data.Key == "Longitud")
                        {
                            machine.Longitude = data.Value.ToString();
                        }
                        if(data.Key == "Telefono")
                        {
                            machine.PhoneNumber = data.Value.ToString();
                        }
                        if(data.Key == "Direccion")
                        {
                            machine.Address = data.Value.ToString();
                        }
                    }
                    machines.Add(machine);
                }
                return machines;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Machine> GetMachine(String serialNumber)
        {
            Machine machine = new Machine()
            {
                Products = new List<Product>()
            };
            var queryDoc = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(serialNumber).GetAsync();
            machine.SerialNumber = serialNumber;
            foreach (var data in queryDoc.Data)
            {
                if (data.Key == "Latitud")
                {
                    machine.Latitude = data.Value.ToString();
                }
                if (data.Key == "Longitud")
                {
                    machine.Longitude = data.Value.ToString();
                }
                if (data.Key == "Telefono")
                {
                    machine.PhoneNumber = data.Value.ToString();
                }
                if (data.Key == "Direccion")
                {
                    machine.Address = data.Value.ToString();
                }
            }
           var queryCol = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(serialNumber).Collection("Productos").GetAsync();
            foreach(var doc in queryCol.Documents) 
            {
                Product product = new Product();
                foreach(var data in doc.Data)
                {
                    if(data.Key == "Cantidad")
                    {
                        product.Quantity = float.Parse(data.Value.ToString());
                    }
                    if(data.Key == "Nombre")
                    {
                        product.Name = data.Value.ToString();
                    }
                    if(data.Key == "Posicion")
                    {
                        product.Position = int.Parse(data.Value.ToString());
                    }
                    if(data.Key == "Precio")
                    {
                        product.Price = int.Parse(data.Value.ToString());
                    }
                }
                machine.Products.Add(product);
            }
            return machine;
        }
        public async Task<List<MachineTask>> GetMachineTasks()
        {
            List<MachineTask> machineTasks = new List<MachineTask>();
            var query = await CrossCloudFirestore.Current.Instance.Collection("Tareas").GetAsync();
            foreach( var doc in query.Documents)
            {
                MachineTask machineTask = new MachineTask();
                machineTask.Id = doc.Id;
                foreach( var data in doc.Data)
                {
                    if (data.Key == "Cantidad")
                    {
                        machineTask.Quantity = float.Parse(data.Value.ToString());
                    }
                    if(data.Key == "Maquina")
                    {
                        machineTask.Machine = data.Value.ToString();
                    }
                    if(data.Key == "Producto")
                    {
                        machineTask.Product = data.Value.ToString();
                    }
                    if (data.Key == "Estatus")
                    {
                        machineTask.Status = bool.Parse(data.Value.ToString());
                    }
                }
                if(machineTask.Status == true)
                {
                    machineTasks.Add(machineTask);
                }
            }
            return machineTasks;
        }
        public async Task MarkAsCompleted(MachineTask task)
        {
            await CrossCloudFirestore.Current.Instance.Collection("Tareas").Document(task.Id).SetAsync(
                new { 
                        Cantidad = task.Quantity, 
                        Maquina = task.Machine,
                        Estatus = task.Status,
                        Producto = task.Product
                    }
            );

        }
    }
}
