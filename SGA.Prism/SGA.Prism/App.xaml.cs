using Newtonsoft.Json;
using Prism;
using Prism.Ioc;
using SGA.Common.Helpers;
using SGA.Common.Models;
using SGA.Common.Service;
using SGA.Prism.ViewModels;
using SGA.Prism.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SGA.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU5NDY4QDMxMzcyZTMzMmUzMENFc3YwRXhCd05rZ0hyTmtJaTd4MWdXZkx3N0FMOWpzUEhNcWw1RmZkTnc9");
            InitializeComponent();

            ////TODO: Cuando se haga el Token y todo lo necesario,descomentar este código
            //var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            //if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            //{
            //    await NavigationService.NavigateAsync("Navigation/Main");
            //}
            //else
            //{
            //    await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            //}

            await NavigationService.NavigateAsync("Navigation/Configuracion");
            //await NavigationService.NavigateAsync("Navigation/Main");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            //containerRegistry.RegisterForNavigation<TestConn, TestConnViewModel>("Test");
            containerRegistry.RegisterForNavigation<SGAConfiguracion, SGAConfiguracionViewModel>("Configuracion");
            containerRegistry.RegisterForNavigation<SGAConfiguracionPopUp, SGAConfiguracionPopUpViewModel>("ConfiguracionPopUp");
        }
    }
}
