using Plugin.Connectivity;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SGA.Common.Service;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using SGA.Prism.Views;
using Prism.Navigation.Xaml;
using Rg.Plugins.Popup.Contracts;

namespace SGA.Prism.ViewModels
{
    public class SGAConfiguracionViewModel : ViewModelBase
    {
        private string conexIP;
        private string conexPuerto;
        private string conexUser;
        private string conexPass;
        private string conexBd;
        private string imagenLogo;
        private string conexVersion;
        private bool conexRecordar;
        private bool botonGuardar;
        private bool labelConnection;
        private bool conexImagen;
        private bool hidden;
        private bool revisarParametros;
        private bool l_RevisarInternet;
        private bool l_RevisarServidor;
        private bool l_RevisarUsuario;
        private readonly IApiService _apiService;
        public bool isValid;


        public string ConexIP { get => conexIP; set { conexIP = value; RaisePropertyChanged(); } }
        public string ConexPuerto { get => conexPuerto; set { conexPuerto = value; RaisePropertyChanged(); } }
        public string ConexUser { get => conexUser; set { conexUser = value; RaisePropertyChanged(); } }
        public string ConexPass { get => conexPass; set { conexPass = value; RaisePropertyChanged(); } }
        public string ConexBd { get => conexBd; set { conexBd = value; RaisePropertyChanged(); } }
        public string ConexVersion { get => conexVersion; set { conexVersion = value; RaisePropertyChanged(); } }

        public bool HidePage { get => hidden; set { hidden = value; RaisePropertyChanged(); } }
        public bool RevisarParametros { get => revisarParametros; set { revisarParametros = value; RaisePropertyChanged(); } }
        public bool L_RevisarInternet { get => l_RevisarInternet; set { l_RevisarInternet = value; RaisePropertyChanged(); } }
        public bool L_RevisarServidor { get => l_RevisarServidor; set { l_RevisarServidor = value; RaisePropertyChanged(); } }
        public bool L_RevisarUsuario { get => l_RevisarUsuario; set { l_RevisarUsuario = value; RaisePropertyChanged(); } }


        public bool ConexRecordar { get => conexRecordar; set { conexRecordar = value; RaisePropertyChanged(); } }
        public bool CanConnect { get; private set; }
        public bool BotonGuardar { get => botonGuardar; set { botonGuardar = value; RaisePropertyChanged(); } }
        public bool LabelConnection { get => labelConnection; set { labelConnection = value; RaisePropertyChanged(); } }
        public bool ConexImagen { get => conexImagen; set { conexImagen = value; RaisePropertyChanged(); } }
        public string ImagenLogo { get => imagenLogo; set { imagenLogo = value; RaisePropertyChanged(); } }


        public DelegateCommand ButtonCommand { get; }
        public DelegateCommand ButtonSelecImgCommand { get; }
        public DelegateCommand PopUpCommand { get; }

        IPageDialogService _pageDialogService { get; }
        public INavigationService _navigationService { get; }
        public object PopUpNavigation { get; private set; }
        public IPopupNavigation IPopupNavigation { get; private set; }

        public SGAConfiguracionViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IPageDialogService pageDialogService)
           : base(navigationService)
        {
            Title = "Panel de Configuración";
            HidePage = false;
            RevisarParametros = true;
            L_RevisarInternet = false;
            L_RevisarServidor = false;
            L_RevisarUsuario = false;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _apiService = apiService;
            ButtonCommand = new DelegateCommand(Button_Clicked);
            ButtonSelecImgCommand = new DelegateCommand(ButtonSelecImg_Cliked);
            PopUpCommand = new DelegateCommand(PopUp);

            RevisarParamConex();

            //TODO: Eliminar esto
            ConexIP = "cant";
            //ConexPuerto = "80";
            ConexBd = "ctl_load";
            ConexVersion = "3";
            ConexUser = "oys";
            ConexPass = "pan";

        }

        public async void RevisarInternet()
        {
            RevisarParametros = false;
            L_RevisarInternet = true;
            var isOK = true;

            string huesped = "google.com";
            var revisarConexion = await _apiService.CheckConnection(huesped);
            if (!revisarConexion)
            {
                HidePage = true;
                RevisarParametros = false;
                L_RevisarInternet = false;
                await App.Current.MainPage.DisplayAlert("No hay Internet", "Por favor Actívelo o revise la conexión", "OK");
                LabelConnection = true;
                isOK = false;
                return;
                //await _navigationService.NavigateAsync("/Configuracion");
            }

            L_RevisarInternet = false;
            RevisarServidor();
        }

        public async void RevisarServidor()
        {
            L_RevisarServidor = true;
            var isOK = true;
            var connectivity = CrossConnectivity.Current;
            int conexPuertoInt = Convert.ToInt32(ConexPuerto);
            var servidorActivoAccesible = await connectivity.IsRemoteReachable(ConexIP, conexPuertoInt, 5000);

            if (servidorActivoAccesible == false)
            {
                HidePage = true;
                L_RevisarServidor = false;
                await App.Current.MainPage.DisplayAlert("Error Del Servidor", "Revise que haya introducido la IP y Puerto válidos.", "Aceptar");
                LabelConnection = false;
                BotonGuardar = true;
                //RevisarParamConex();
                isOK = false;
                return;
                //await _navigationService.NavigateAsync("/Configuracion");
            }

            if (isOK)
            {
                L_RevisarServidor = false;
                //RevisarPass();
                RecordarConexion();
            }
        }

        //public async void RevisarPass()
        //{
        //    string ValidPass = Preferences.Get("ConexPass", "");
        //    if (ValidPass.Contains("null"))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", "Debe de introducir la contraseña", "Aceptar");
        //        HidePage = true;
        //        return;
        //    }

        //    RecordarConexion();
        //}

        public async void RecordarConexion()
        {
            L_RevisarUsuario = true;
            var isValid = Preferences.Get("ConexRecordar", false);
            if (isValid)
            {

                await _navigationService.NavigateAsync("/Login");
            }
            else
            {
                HidePage = true;
                RevisarParametros = false;
                L_RevisarUsuario = false;
                //RevisarParamConex();
            }
        }


        public void RevisarParamConex()
        {

            if (Preferences.ContainsKey("ConexIP"))
            {
                ConexIP = Preferences.Get("ConexIP", "");
            }

            if (Preferences.ContainsKey("ConexPuerto"))
            {
                ConexPuerto = Preferences.Get("ConexPuerto", "");
            }

            if (Preferences.ContainsKey("ConexBd"))
            {
                ConexBd = Preferences.Get("ConexBd", "");
            }

            if (Preferences.ContainsKey("ConexVersion"))
            {
                ConexVersion = Preferences.Get("ConexVersion", "");
            }

            if (Preferences.ContainsKey("ImagenLogo"))
            {
                ImagenLogo = Preferences.Get("ImagenLogo", "");
            }

            if (Preferences.ContainsKey("ConexUser"))
            {
                ConexUser = Preferences.Get("ConexUser", "");
            }

            if (Preferences.ContainsKey("ConexPass"))
            {
                ConexPass = Preferences.Get("ConexPass", "");
            }

            if (Preferences.ContainsKey("ConexRecordar"))
            {
                ConexRecordar = Preferences.Get("ConexRecordar", false);
            }

        }

        async void PopUp()
        {
            IPopupNavigation = PopupNavigation.Instance;
            await IPopupNavigation.PushAsync(new SGAConfiguracionPopUp());
        }

    async void ButtonSelecImg_Cliked()
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Imágenes no permitidas", ":( No tiene permisos para subir imágenes.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            ImagenLogo = file.Path;
            Preferences.Set("ImagenLogo", ImagenLogo);
        }

        async void PonerDatosPersistencia()
        {
            string url = "http://" + ConexIP + ":" + ConexPuerto;

            Preferences.Set("AxionalServer", url);
            Preferences.Set("ConexIP", ConexIP);
            Preferences.Set("ConexPuerto", ConexPuerto);
            Preferences.Set("ConexBd", ConexBd);
            Preferences.Set("ConexVersion", ConexVersion);
            Preferences.Set("ConexUser", ConexUser);
            Preferences.Set("ConexRecordar", ConexRecordar);

            await SecureStorage.SetAsync("ConexPass", ConexPass);
            var sgaPassword = await SecureStorage.GetAsync("ConexPass");
            Preferences.Set("ConexPass", sgaPassword);

            await _navigationService.NavigateAsync("/Login");
        }

        async void Button_Clicked()
        {
            //BotonGuardar = false;           

            if (string.IsNullOrEmpty(ConexIP))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de introducir una IP válida.", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(ConexPuerto) || ConexPuerto.Length < 2)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de introducir un Puerto de 2 Números.", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(ConexUser))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de introducir un Usuario .", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(ConexPass) || ConexPass.Length < 1)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debes de introducir una contaseña válida y mayor de 5 caracteres .", "Aceptar");
                return;
            }


            var connection = await _apiService.CheckConnection("google.com");
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Revisa la Conexión de Internet.", "Aceptar");
                return;
            }


           // RevisarServidor();
            //isValid = await RevisarServidor();

            PonerDatosPersistencia();
        }
    }
}

