using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace SGA.Common.Models
{
    public class SGAAlmacen : SGAEntity
    {
        #region Propiedades
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        private string AxionalTabla = "galmacen";

        #endregion

        #region Constructores

        public SGAAlmacen()
        {
        }// end Constructor por defecto

        public SGAAlmacen(String _Codigo, String _Nombre)
        {
            Codigo = _Codigo;
            Nombre = _Nombre;
        }// end Constructor

        #endregion

        #region Métodos Públicos

        public ObservableCollection<SGAAlmacen> getList()
        {
            ObservableCollection<SGAAlmacen> mylist = new ObservableCollection<SGAAlmacen>();

            XmlNodeList rows = getAxional(AxionalTabla, " 1=1");

            foreach (XmlNode mensaje in rows)
            {
                String _Codigo = nullStringParse(mensaje.ChildNodes[0]);
                String _Nombre = nullStringParse(mensaje.ChildNodes[1]);

                mylist.Add(new SGAAlmacen(_Codigo, _Nombre));
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
