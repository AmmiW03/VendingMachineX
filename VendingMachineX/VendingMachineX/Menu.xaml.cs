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
    public partial class Menu : TabbedPage
    {
        public Menu()
        {
            InitializeComponent();
        }
        private async void NewMachine(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new NewMachine());
        }

        private async void MachineDetails(object sender, EventArgs e)
        {
            // Aquí es donde se realiza la navegación a la otra página
            await Navigation.PushAsync(new MachineDetails());
        }
    }
}