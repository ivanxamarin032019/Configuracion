using System;
using System.Collections.Generic;
using System.Xml;

namespace SGA.Common.Models
{
    public class SGACentro : SGAEntity
    {
        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        private string AxionalTabla = "cur_gmao_centros";

        #region Constructores

        public SGACentro()
        {
        }// end Constructor por defecto

        public SGACentro(string _Codigo, string _Descripcion)
        {
            Codigo = _Codigo;
            Descripcion = _Descripcion;
        }// end Constructor

        #endregion


        //#region Sincronización

        public List<SGACentro> getAxional()
        {
            List<SGACentro> lstSGACentros = new List<SGACentro>();

            XmlNodeList rows = getAxional(AxionalTabla, null);

            foreach (XmlNode centro in rows)
            {
                Codigo = nullStringParse(centro.ChildNodes[0]);
                Descripcion = nullStringParse(centro.ChildNodes[1]);
                lstSGACentros.Add(new SGACentro(Codigo, Descripcion));
            }

            return lstSGACentros;

        }// end method getAxionalUsers

        public override void sync(SGASynchronizationMode modo)
        {
            throw new NotImplementedException();
        }
    }
}