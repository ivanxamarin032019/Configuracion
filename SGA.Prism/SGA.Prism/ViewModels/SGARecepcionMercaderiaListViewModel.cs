﻿using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SGA.Common.Models;
using System.Collections.ObjectModel;

namespace SGA.Prism.ViewModels
{
    public class SGARecepcionMercaderiaListViewModel : ViewModelBase
    {
        private SGARegistroMercancia message = new SGARegistroMercancia();
        private IPageDialogService _dialogService;

        private DelegateCommand _continuarCommand;
        private DelegateCommand _addCommand;
        private DelegateCommand<SGARegistroMercancia> _itemTappedCommand;
        private string pickerAlmacen;

        public INavigationService _navigationService { get; }

        public ObservableCollection<SGARegistroMercancia> SGAListItems { get; set; }
        public string PickerAlmacen { get => pickerAlmacen; set { pickerAlmacen = value; RaisePropertyChanged(); } }

        public SGARecepcionMercaderiaListViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Listado de Recepción";
            _navigationService = navigationService;
            _dialogService = dialogService;

            LoadList();
        }

        public DelegateCommand ContinuarCommand => _continuarCommand ?? (_continuarCommand = new DelegateCommand(Continuar));
        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(Add));
        public DelegateCommand<SGARegistroMercancia> ItemTappedCommand => _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<SGARegistroMercancia>(ItemTapped));

        private async void ItemTapped(SGARegistroMercancia obj)
        {
            NavigationParameters par = new NavigationParameters();
            par.Add("Parametro1", obj);
            await _navigationService.NavigateAsync("RecepcionMercaderia", par);
        }



        private async void Continuar()
        {
            await _navigationService.NavigateAsync("RecepcionMercaderia");
        }

        private void LoadList()
        {
            SGAListItems = new ObservableCollection<SGARegistroMercancia>();
            SGAListItems = message.getList();
            //PickerAlmacen = message.Almacen;
        }

        private async void Add()
        {
            await _navigationService.NavigateAsync("RecepcionMercaderia");
        }


    }
}
