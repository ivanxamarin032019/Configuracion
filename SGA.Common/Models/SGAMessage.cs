using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace SGA.Common.Models
{
    public class SGAMessageList : SGAEntity
    {
        #region Propiedades
        public List<SGAMessage> Mensajes = new List<SGAMessage>();

        private string AxionalTabla = "mSGAMessage";
        #endregion

        #region Constructor
        public SGAMessageList()
        {

        }
        #endregion
        
        public SGAMessageList Add(List<SGAMessage> _list)
        {
            Mensajes = _list;
            return this;
        }
        public override void sync(SGASynchronizationMode modo)
        {
            throw new NotImplementedException();
        }

        #region Persistencia Axional

        public void ConfirmarLecturaAxional()
        {
            JObject j = this.toJSON();
            string jsonEntidades = j.ToString();

            XmlNodeList rows = processAxional(AxionalTabla, jsonEntidades, "Confirmar");
            if (rows.Count.Equals(1))
            {
                if (rows[0].ChildNodes.Count.Equals(2))
                {
                    String errmsg = rows[0].ChildNodes.Item(1).InnerText;
                    throw new Exception(errmsg);
                }
            }
        }

       
        #endregion

    }
    public class SGAMessage : SGAEntity
    {
        public int ID { get; set; }
        public String Usuario { get; set; }
        public Boolean Leido { get; set; }
        public int      Icono { get; set; }
        public string TextoMensaje { get; set; }

        public String PedidoCompra { get; set; }

        private string AxionalTabla = "mSGAMessage";

        #region Constructores

        public SGAMessage()
        {
        }// end Constructor por defecto

        public SGAMessage(int _ID, String _Usuario, Boolean _Leido, int _Icono, string _TextoMensaje, String _PedidoCompra)
        {
            ID = _ID;
            Usuario = _Usuario;
            Leido = _Leido;
            Icono = _Icono;
            TextoMensaje = _TextoMensaje;
            PedidoCompra = _PedidoCompra;
        }// end Constructor

        #endregion

        #region Métodos Públicos

        //public List<SGAMessage> getMessages(Boolean pending)
        public ObservableCollection<SGAMessage> getMessages(Boolean pending)

        {
            List<SGAMessage> mylist = new List<SGAMessage>();
            //ObservableCollection<SGAMessage> mylist = new ObservableCollection<SGAMessage>();

            String cond = " 1=1";
            if (pending)
            {
                cond = " ind_read = 0";
            }

            XmlNodeList rows = getAxional(AxionalTabla, cond);

            foreach (XmlNode mensaje in rows)
            {
                int _ID = nullInt16Parse(mensaje.ChildNodes[0]);
                string _Usuario = nullStringParse(mensaje.ChildNodes[1]);
                Boolean _Leido = nullBooleanParse(mensaje.ChildNodes[2]);
                int _Icono = nullInt16Parse(mensaje.ChildNodes[3]);
                string _TextoMensaje = nullStringParse(mensaje.ChildNodes[4]);
                string _PedidoCompra = nullStringParse(mensaje.ChildNodes[5]);
                mylist.Add(new SGAMessage(_ID, _Usuario, _Leido, _Icono, _TextoMensaje, _PedidoCompra));
            }

            //return mylist;
            return new ObservableCollection<SGAMessage>(mylist);

        }

        #endregion


        public override void sync(SGASynchronizationMode modo)
        {
            throw new NotImplementedException();
        }
    }
}
