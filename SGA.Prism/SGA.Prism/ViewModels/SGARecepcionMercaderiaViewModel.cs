using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SGA.Common.Models;

namespace SGA.Prism.ViewModels
{

    public class SGARecepcionMercaderiaViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;
        private bool botonGuardar;
        private string imagenCaptura;
        private string transportista;
        private string ordenInterna;
        private string selectedAlmacen;
        private string notas;
        private int sets;
        private int cajas;
        private string nombreConductor;
        private string referencia;

        public INavigationService _navigationService { get; }
        public bool BotonGuardar { get => botonGuardar; set { botonGuardar = value; RaisePropertyChanged(); } }

        public DelegateCommand ButtonSelecImgCommand { get; }
        public DelegateCommand ButtonCommand { get; }
        public string ImagenCaptura { get => imagenCaptura; set { imagenCaptura = value; RaisePropertyChanged(); } }
        public string Transportista { get => transportista; set { transportista = value; RaisePropertyChanged(); } }
        public string OrdenInterna { get => ordenInterna; set { ordenInterna = value; RaisePropertyChanged(); } }  
        public string SelectedAlmacen { get => selectedAlmacen; set { selectedAlmacen = value; RaisePropertyChanged(); } }
        public string Notas { get => notas; set { notas = value; RaisePropertyChanged(); } }
        public int Sets { get => sets; set { sets = value; RaisePropertyChanged(); } }
        public int Cajas { get => cajas; set { cajas = value; RaisePropertyChanged(); } }
        public string NombreConductor { get => nombreConductor; set { nombreConductor = value; RaisePropertyChanged(); } }
        public string Referencia { get => referencia; set { referencia = value; RaisePropertyChanged(); } }

        public SGARecepcionMercaderiaViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Registro mercadería";
            _navigationService = navigationService;
            _dialogService = dialogService;
            ButtonCommand = new DelegateCommand(Button_Clicked);
            ButtonSelecImgCommand = new DelegateCommand(ButtonSelecImg_Cliked);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var par1 = parameters["Parametro1"];
            if (par1 != null)
            {
                var infoRecuperada = (SGARegistroMercancia)par1;

                SelectedAlmacen = infoRecuperada.Almacen;
                OrdenInterna = infoRecuperada.OrdenInterna;
                Transportista = infoRecuperada.NombreConductor;
                Notas = infoRecuperada.Notas;
                Sets= infoRecuperada.NumeroSets; 
                Cajas = infoRecuperada.NumeroCajas;
                NombreConductor = infoRecuperada.NombreConductor;
                Referencia = infoRecuperada.ReferenciaTransportista;
            }
            else
            {
                //return new List<string> { "Bryan", "Pau", "Oscar" };
            }

        }

        public IList<string> TransportistaList
        {
            get
            {
                return new List<string> { "Bryan", "Pau", "Oscar" };
            }
        }

        public IList<string> AlmacenList
        {
            get
            {
                return new List<string> { "Cantallops", "OyS", "Bcn" };
            }
        }

        async void ButtonSelecImg_Cliked()
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Imágenes no permitidas", ":( No tiene permisos para subir imágenes.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92,
                Directory = "ImagenesApp",
                Name = "Img.jpg"
            });

            if (file == null)
                return;

            ImagenCaptura = file.Path;
        }

        async void Button_Clicked()
        {
            //Stream stream = await signature.GetImageStreamAsync(SignatureImageFormat.Jpeg);
        }

        

    }
}
