using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineX.Models
{
    public class Machine
    {
        public String SerialNumber { get; set; }
        public String Address { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public String PhoneNumber { get; set; }
        public List<Product> Products { get; set; }

        public Machine(Machine machine)
        {
            SerialNumber = machine.SerialNumber;
            Address = machine.Address;
            Latitude = machine.Latitude;
            Longitude = machine.Longitude;
            Products = machine.Products;
        }
        public Machine(){ }
    }
}
