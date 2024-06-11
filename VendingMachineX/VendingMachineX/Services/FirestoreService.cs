using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Classes;
using VendingMachineX.ViewModels;
using VendingMachineX.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System.Data;

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

        public async Task<bool> AddMachine(Machine machine)
        {
            await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(machine.SerialNumber).SetAsync(
                new
                {
                    Direccion = machine.Address,
                    Latitud = machine.Latitude,
                    Longitud = machine.Longitude,
                    Telefono = machine.PhoneNumber
                });
            var queryDoc = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(machine.SerialNumber).GetAsync();
            if(queryDoc.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> GetProductsCount(String serialNumber)
        {
            var query = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(serialNumber).Collection("Productos").GetAsync();
            return query.Count;
        }

        public async Task AddProducts(String serialNumber, Product product)
        {
            String path = "Producto" + product.Position;
            try
            {
                await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(serialNumber).Collection("Productos").Document(path).SetAsync(
                    new
                    {
                        Cantidad = product.Quantity,
                        Nombre = product.Name,
                        Posicion = product.Position,
                        Precio = product.Price
                    }
                );
            }catch ( Exception ex )
            {
                throw ex;
            }
        }

        public async Task<List<SalePointHistory>> ConsultHistory(String serialNumber)
        {
            List<SalePointHistory> history = new List<SalePointHistory>();
            var query = await CrossCloudFirestore.Current.Instance.Collection("Maquinas").Document(serialNumber).Collection("HistorialVentas").GetAsync();
            var exDoc = query.Documents.First();
            int count = exDoc.Data.Count;
            
            for( int i = 0; i < count; i++)
            {
                SalePointHistory salePoint = new SalePointHistory();
                salePoint.Id = "Producto" + (i + 1);
                foreach (var document in query.Documents)
                {
                    foreach (var item in document.Data)
                    {
                        if(item.Key == salePoint.Id)
                        {
                            ProductSale productSale = new ProductSale();
                            productSale.Date = DateTime.ParseExact(document.Id, "dd.MM.yyyy",CultureInfo.InvariantCulture);
                            productSale.Quantity = float.Parse(item.Value.ToString());
                            salePoint.SalePoints.Add(productSale);
                        }
                    }
                }
                history.Add(salePoint);
            }
            foreach (var product in history)
            {
                product.SalePoints = product.SalePoints.OrderBy(p => p.Date).ToList();
            }
            return history;
        }
    }
}
