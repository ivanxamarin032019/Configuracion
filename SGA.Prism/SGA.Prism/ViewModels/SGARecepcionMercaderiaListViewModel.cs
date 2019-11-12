using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SGA.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SGA.Prism.ViewModels
{
    public class SGARecepcionMercaderiaListViewModel : ViewModelBase
    {
        private SGARegistroMercancia message = new SGARegistroMercancia();
        private IPageDialogService _dialogService;

        private DelegateCommand _continuarCommand;
        private DelegateCommand _addCommand;
        private DelegateCommand _itemTappedCommand;

        public INavigationService _navigationService { get; }

        public ObservableCollection<SGARegistroMercancia> SGAListItems { get; set; }

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
        public DelegateCommand ItemTappedCommand => _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand(ItemTapped));

        private void ItemTapped()
        {
            //var result = (Transfer)value;
        }

        private async void Continuar()
        {
            await _navigationService.NavigateAsync("RecepcionMercaderia");
        }

        private void LoadList()
        {
            SGAListItems = new ObservableCollection<SGARegistroMercancia>();
            SGAListItems = message.getList();
        }

        private async void Add()
        {
            await _navigationService.NavigateAsync("RecepcionMercaderia");
        }


    }
}
