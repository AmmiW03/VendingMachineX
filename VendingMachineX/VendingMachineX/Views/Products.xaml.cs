using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VendingMachineX.ViewModels;
using VendingMachineX.Models;


namespace VendingMachineX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
        String serialNumber {  get; set; }
        public Products()
        {
            InitializeComponent();
            ProductsViewModel viewModel = new ProductsViewModel();
            BindingContext = viewModel;
            viewModel.LoadProductsCount(serialNumber).ConfigureAwait(false);
            Maquina.Text = serialNumber;
        }

        public Products(string serialNumber)
        {
            InitializeComponent();
            ProductsViewModel viewModel = new ProductsViewModel();
            BindingContext = viewModel;
            viewModel.LoadProductsCount(serialNumber).ConfigureAwait(false);
            this.serialNumber = serialNumber;
            Maquina.Text = serialNumber;
        }

        public async void OnUploadButtonClicked(object sender, EventArgs e)
        {
            Product product = new Product()
            {
                Name = txtNombreP.Text,
                Quantity = float.Parse(txtCantidad.Text),
                Position = int.Parse(txtPosicion.Text),
                Price = int.Parse(txtPrecio.Text)
            };

            if(BindingContext is ProductsViewModel viewModel)
            {
                await viewModel.AddProducts(serialNumber,product);
            }
        }
    }
}