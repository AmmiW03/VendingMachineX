using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.CloudFirestore;
using VendingMachineX.ViewModels;
using VendingMachineX.Services;
using VendingMachineX.Models;

namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : TabbedPage
    {
        private String serialNumber;
        public Menu()
        {
            InitializeComponent();
            MenuViewModel viewModel = new MenuViewModel();
            BindingContext = viewModel;

        }
        private async void NewMachine(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new NewMachine());
        }

        private async void MachineDetails(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            if(serialNumber != null)
            {
                await Navigation.PushAsync(new MachineDetails(serialNumber));
            }
        }

        private async void Mapa(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllMachine());    
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MenuViewModel viewModel)
            {
                await viewModel.LoadTask();
                await viewModel.LoadMachines();
                foreach (var machine in viewModel.Machines)
                {
                    Pin pin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = machine.PhoneNumber,
                        Address = machine.Address,
                        Position = new Position(double.Parse(machine.Latitude), double.Parse(machine.Longitude)),
                        Tag = machine.SerialNumber
                    };
                    maps.Pins.Add(pin);
                }
            }
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);
            Location location = await Geolocation.GetLocationAsync(request);
            Position position = new Position(location.Latitude, location.Longitude);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(100000)));
        }
        private void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            lblAddress.Text = $"Ubicación: {e.Pin.Position.Latitude}, {e.Pin.Position.Longitude}";
            lblContact.Text = $"Contacto: {e.Pin.Label}";
            serialNumber = e.Pin.Tag.ToString();
        }

        private async void OnItemTapped(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (MenuViewModel)BindingContext;
            MachineTask task = new MachineTask();
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                task = e.CurrentSelection[0] as MachineTask;
            }
            String action = await DisplayActionSheet("Opciones", "Cancelar", null, "Marcar como completado");
            if (action == "Marcar como completado")
            {
                task.Status = false;
                await viewModel.MarkAsCompleted(task);
            }
        }
    }
}