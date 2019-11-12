using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace SGA.Common.Models
{
    //public class SGARegistroMercancia : SGAEntity
    public class SGARegistroMercancia
    {
        #region Propiedades
        public int ID { get; set; }
        public string Almacen { get; set; }

        public string OrdenInterna { get; set; }
        public string ReferenciaTransportista { get; set; }
        public string NombreConductor { get; set; }
        public int NumeroCajas { get; set; }
        public int NumeroSets { get; set; }
        public string Notas { get; set; }

        private string AxionalTabla = "albranesTransporte";

        #endregion

        #region Constructores

        public SGARegistroMercancia()
        {
        }// end Constructor por defecto

        public SGARegistroMercancia(int _ID, String _OrdenInterna, String _ReferenciaTransportista, String _NombreConductor, int _NumeroCajas, int _NumeroSets,
            String _Notas)
        {
            ID = _ID;
            OrdenInterna = _OrdenInterna;
            ReferenciaTransportista = _ReferenciaTransportista;
            NombreConductor = _NombreConductor;
            NumeroCajas = _NumeroCajas;
            NumeroSets = _NumeroSets;
            Notas = _Notas;
        }// end Constructor

        #endregion

        #region Métodos Públicos

        public ObservableCollection<SGARegistroMercancia> getList()
        {
            ObservableCollection<SGARegistroMercancia> mylist = new ObservableCollection<SGARegistroMercancia>();

            mylist.Add(new SGARegistroMercancia(0, "1002345", "4TYUY78s", "Julián García", 8, 2, "Recibir por la mañana"));
            mylist.Add(new SGARegistroMercancia(1, "1002346", "787Y797a", "Luciana Sabariz", 5, 1, "El material está roto"));
            mylist.Add(new SGARegistroMercancia(2, "1002350", "48A7956b", "Pedro Manrique", 4, 3, null));

            return mylist;
        }

        #endregion

        //public override void sync(SGASynchronizationMode modo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
