using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MachineDetails : ContentPage
    {
        public MachineDetails()
        {
            InitializeComponent();
        }

        private async void Products(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new Products());
        }
    }
}