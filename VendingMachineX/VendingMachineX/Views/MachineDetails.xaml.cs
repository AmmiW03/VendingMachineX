using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachineX.Models;
using VendingMachineX.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MachineDetails : ContentPage
    {
        private String serialNumber;
        public MachineDetails(String serialNumber)
        {
            InitializeComponent();
            this.serialNumber = serialNumber;
            MachineDetailsViewModel viewModel = new MachineDetailsViewModel(serialNumber);
            BindingContext = viewModel;
            viewModel.LoadMachineData(serialNumber).ConfigureAwait(false);
        }

        private async void Products(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new Products(serialNumber));
        }

        private async void NewMachine(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new NewMachine());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<ChartEntry> entries = new List<ChartEntry>();
            if (BindingContext is MachineDetailsViewModel viewModel)
            {
                await viewModel.LoadMachineData(serialNumber);
                foreach (var product in viewModel.Machine.Products)
                {
                    ChartEntry chartEntry = new ChartEntry(product.Quantity)
                    {
                        Label = product.Name,
                        Color = SKColor.Parse("#1287BC"),
                        ValueLabel = product.Quantity.ToString()
                    };
                    entries.Add(chartEntry);
                }
                Charts.Chart = new BarChart { Entries = entries, LabelTextSize = 30, LabelColor = SKColor.Parse("#000000") };

            }
        }

    }
}