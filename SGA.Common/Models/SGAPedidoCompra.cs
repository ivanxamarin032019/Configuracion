using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace SGA.Common.Models
{
    public class SGAPedidoCompras : SGAEntity
    {
        public int ID { get; set; }
        public string NumeroPedido { get; set; }
        public string TipoPedido { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string CodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Almacen { get; set; }
        private string AxionalTabla = "gcompedh";

        #region Constructores

        public SGAPedidoCompras()
        {
        }// end Constructor por defecto

        public SGAPedidoCompras(int _ID, String _NumeroPedido, String _TipoPedido, String _CodigoProveedor, String _NombreProveedor, DateTime? _FechaPedido, 
            String _Almacen)
        {
            ID = _ID;
            NumeroPedido = _NumeroPedido;
            TipoPedido = _TipoPedido;
            CodigoProveedor = _CodigoProveedor;
            NombreProveedor = _NombreProveedor;
            FechaPedido = _FechaPedido;
        }// end Constructor

        #endregion

        #region Métodos Públicos

        public SGAPedidoCompras Find()
        {
            XmlNodeList rows = getAxional(AxionalTabla, " gcompedh.docser='" + NumeroPedido+ "'");

            if (rows.Count.Equals(1))
            {
                TipoPedido = nullStringParse(rows[0].ChildNodes[2]);
                FechaPedido = (DateTime)nullDateParse(rows[0].ChildNodes[3]);
                CodigoProveedor = nullStringParse(rows[0].ChildNodes[4]);
                NombreProveedor = nullStringParse(rows[0].ChildNodes[5]);
                Almacen = nullStringParse(rows[0].ChildNodes[6]);
            }
            else
            {
                FechaPedido = null;
                CodigoProveedor = null;
                NombreProveedor = null;
                Almacen = null;
            }


            return this;
        }// end Find method

        public ObservableCollection<SGAPedidoCompras> getPedidos()
        {
            ObservableCollection<SGAPedidoCompras> mylist = new ObservableCollection<SGAPedidoCompras>();

            XmlNodeList rows = getAxional(AxionalTabla, " 1=1");

            foreach (XmlNode mensaje in rows)
            {
                int _ID = nullInt16Parse(mensaje.ChildNodes[0]);
                string _NumeroPedido = nullStringParse(mensaje.ChildNodes[1]);
                string _TipoPedido = nullStringParse(rows[0].ChildNodes[0]);
                DateTime? _FechaPedido = (DateTime)nullDateParse(rows[0].ChildNodes[1]);
                string _CodigoProveedor = nullStringParse(rows[0].ChildNodes[2]);
                string _NombreProveedor = nullStringParse(rows[0].ChildNodes[3]);
                string _Almacen = nullStringParse(rows[0].ChildNodes[4]);

                mylist.Add(new SGAPedidoCompras(_ID, _NumeroPedido, _TipoPedido,  _CodigoProveedor, _NombreProveedor, _FechaPedido, _Almacen));
            }

            return mylist;
        }

        #endregion

        public override void sync(SGASynchronizationMode modo)
        {
            throw new NotImplementedException();
        }
    }
}
