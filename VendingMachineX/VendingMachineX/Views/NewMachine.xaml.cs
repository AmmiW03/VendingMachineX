using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Models;
using VendingMachineX.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMachine : ContentPage
    {
        private Position currentPosition;
        public NewMachine()
        {
            InitializeComponent();
        }

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtAddress.Text != null && txtSerialNumber.Text != null && txtPhoneNumber.Text != null)
            {
                if (txtAddress.Text != String.Empty && txtSerialNumber.Text != String.Empty && txtPhoneNumber.Text != String.Empty)
                {
                    btnUpload.IsEnabled = true;
                }
                else
                {
                    btnUpload.IsEnabled = false;
                }
            }
            else
            {
                btnUpload.IsEnabled = false;
            }
        }

        public async void OnUploadClicked(object sender, EventArgs e)
        {
            Machine machine = new Machine()
            {
                Address = txtAddress.Text,
                Latitude = currentPosition.Latitude.ToString(),
                Longitude = currentPosition.Longitude.ToString(),
                SerialNumber = txtSerialNumber.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Products = new List<Product>(),
            };

            if(BindingContext is NewMachineViewModel viewModel)
            {
                if(await viewModel.AddMachine(machine))
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Error al subir los datos de la maquina", "OK");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);
            Location location = await Geolocation.GetLocationAsync(request);
            currentPosition = new Position(location.Latitude, location.Longitude);
            Pin pin = new Pin()
            {
                Type = PinType.Place,
                Label = "Posicion Actual",
                Address = currentPosition.Latitude.ToString() + "," + currentPosition.Longitude.ToString(),
                Position = currentPosition,
                Tag = "id_Ubicacion",
            };

            maps.Pins.Add(pin);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMeters(100000)));
        }
    }
}