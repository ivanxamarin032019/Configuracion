using Xamarin.Forms;
using SGA.Prism.ViewModels;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Contracts;

namespace SGA.Prism.Views
{
    public partial class SGAConfiguracion : ContentPage
    {
        SGAConfiguracionViewModel ViewModel;

        public SGAConfiguracion()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            ViewModel = (SGAConfiguracionViewModel)this.BindingContext;
            ViewModel.RevisarInternet();
        }
    }    
}
