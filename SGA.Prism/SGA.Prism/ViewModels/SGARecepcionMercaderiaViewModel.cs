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

namespace SGA.Prism.ViewModels
{

    public class SGARecepcionMercaderiaViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;
        private bool botonGuardar;
        private string imagenCaptura;

        public INavigationService _navigationService { get; }
        public bool BotonGuardar { get => botonGuardar; set { botonGuardar = value; RaisePropertyChanged(); } }

        public DelegateCommand ButtonSelecImgCommand { get; }
        public DelegateCommand ButtonCommand { get; }
        public string ImagenCaptura { get => imagenCaptura; set { imagenCaptura = value; RaisePropertyChanged(); } }

        public SGARecepcionMercaderiaViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Registro mercadería";
            _navigationService = navigationService;
            _dialogService = dialogService;
            ButtonCommand = new DelegateCommand(Button_Clicked);
            ButtonSelecImgCommand = new DelegateCommand(ButtonSelecImg_Cliked);


        }

        public IList<string> AlmacenList
        {
            get
            {
                return new List<string> { "Cantallops", "OyS", "Bcn" };
            }
        }

        public IList<string> TransportistaList
        {
            get
            {
                return new List<string> { "Bryan", "Pau", "Oscar" };
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
