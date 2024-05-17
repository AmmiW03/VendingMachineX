using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMachine : ContentPage
    {
        public NewMachine()
        {
            InitializeComponent();

            Pin Ubicacion = new Pin()
            {
                Type = PinType.Place,
                Label = "MAQ 1",
                Address = "UPT",
                Position = new Position(19.714212135634778, -98.97817274973542),
                Tag = "id_Ubicacion",
            };

            Pin Ubicacion2 = new Pin()
            {
                Type = PinType.Place,
                Label = "MAQ 2",
                Address = "UPT",
                Position = new Position(19.69782725371813, -98.97999627729354),
                Tag = "id_Ubicacion",
            };

            maps.Pins.Add(Ubicacion);
            maps.Pins.Add(Ubicacion2);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(Ubicacion.Position, Distance.FromMeters(500)));

        }
    }
}