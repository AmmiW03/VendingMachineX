using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace VendingMachineX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillHistory : ContentPage
	{

		List<ChartEntry> entries = new List<ChartEntry>()
		{
			new ChartEntry(50)
			{
				Label = "Enero",
                Color = SKColor.Parse("#1287BC"),
            },

			new ChartEntry(15)
			{
				Label = "Febrero",
                Color = SKColor.Parse("#71A4BA"),
            },

            new ChartEntry(30)
            {
                Label = "Marzo",
                Color = SKColor.Parse("#1287BC"),
            }
        };

		public FillHistory ()
		{
			InitializeComponent ();

            Charts.Chart = new LineChart { Entries = entries };

        }
    
    }
}