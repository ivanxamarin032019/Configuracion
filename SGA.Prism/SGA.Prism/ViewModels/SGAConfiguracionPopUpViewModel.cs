using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using SGA.Prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGA.Prism.ViewModels
{
    public class SGAConfiguracionPopUpViewModel : ViewModelBase
    {
        public DelegateCommand BotonCerrarColoresCommand { get; }
        public DelegateCommand BotonGuardarColoresCommand { get; }

        
        IPageDialogService _pageDialogService { get; }
        public INavigationService _navigationService { get; }

        public SGAConfiguracionPopUpViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
           : base(navigationService)
        {
            Title = "Configuración Paleta de Colores";
            BotonCerrarColoresCommand = new DelegateCommand(Boton_Cerrar);
            BotonGuardarColoresCommand = new DelegateCommand(Boton_Guardar);
        }

        async void Boton_Cerrar()
        {
           bool respuesta = await App.Current.MainPage.DisplayAlert("ATENCIÓN", "¿Está seguro de Cerrar la ventana?", "Aceptar", "Cancelar");
            if(respuesta)
            {
                
                //await PopupNavigation.Instance.RemovePageAsync(this);

            }
        }

        async void Boton_Guardar()
        {

        }
    }
}
