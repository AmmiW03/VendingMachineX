using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VendingMachineX.ViewModels;
using System.Threading;
using System.Runtime.CompilerServices;
using VendingMachineX.Models;

namespace VendingMachineX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalesHistory : ContentPage
	{
        public String serialNumber;

        public SalesHistory(String serialNumber)
        {
            InitializeComponent();
            this.serialNumber = serialNumber;
            SalesHistoryViewModel viewModel = new SalesHistoryViewModel();
            BindingContext = viewModel;
            viewModel.LoadHistory(serialNumber).ConfigureAwait(false);
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            if(BindingContext is SalesHistoryViewModel viewModel)
            {
                await viewModel.LoadHistory(serialNumber);
                int aux = 0;
                foreach(var product in viewModel.SalesHistory)
                {
                    List<Entry> entries = new List<Entry>();
                    foreach(var salePoint in product.SalePoints)
                    {
                        Entry entry = new Entry(salePoint.Quantity)
                        {
                            Label = salePoint.Date.ToString("MMM dd"),
                            ValueLabel = salePoint.Quantity.ToString(),
                            Color = GetRandomColor()  // Asigna un color aleatorio a cada punto
                        };
                        entry.Label = salePoint.Date.ToString();
                        entries.Add(entry);
                    }
                    switch (aux)
                    {
                        case 0:
                            Chart1.Chart = new LineChart { Entries = entries };
                            break;
                        case 1:
                            Chart2.Chart = new LineChart { Entries = entries };
                            break;
                        case 2:
                            Chart3.Chart = new LineChart { Entries = entries };
                            break;
                        case 3:
                            Chart4.Chart = new LineChart { Entries = entries };
                            break;
                        case 4:
                            Chart5.Chart = new LineChart { Entries = entries };
                            break;
                        case 5:
                            Chart6.Chart = new LineChart { Entries = entries };
                            break;
                    }
                    aux++;
                }
            }
        }
        private SKColor GetRandomColor()
        {
            Random random = new Random();
            return new SKColor(
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256));
        }
    }
}