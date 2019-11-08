using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using oys.services.axional;
using System;
using System.Xml;
using Xamarin.Essentials;

namespace SGA.Common.Models
{
    public enum SGASynchronizationMode
    {
        Download = 0,
        Upload = 1,
    }
    public abstract class SGAEntity
    {
        // public DatabaseContext db;// = new DatabaseContext();
        [JsonIgnore]
        private XmlDocument xmlResponse = new XmlDocument();
        [JsonIgnore]
        public static string AxionalServer;
        [JsonIgnore]
        private string AxionalDbms;
        [JsonIgnore]
        private short AxionalVersion;
        [JsonIgnore]
        private string AxionalVersionTxt;
        // Usuario del App
        [JsonIgnore]
        public static string SGAUser;
        [JsonIgnore]
        public static string SGAPassword;
        // Usuario de conexión del app
        [JsonIgnore]
        private string AxionalSoapUser;
        [JsonIgnore]
        private string AxionalSoapPass;
        [JsonIgnore]
        private string AxionalAuthorization;
        [JsonIgnore]
        public bool isAuthorized;
        [JsonIgnore]
        private Axional axional;
        [JsonIgnore]
        public string AxionalError;

        public SGAEntity()
        {
            //AxionalServer = GMAOHelper.getPreferences("url") + ":" + GMAOHelper.getPreferences("puerto");
            //AxionalDbms = GMAOHelper.getPreferences("base");
            //GMAOUser = GMAOHelper.getPreferences("user");
            //GMAOPassword = GMAOHelper.getPreferences("pass");

            AxionalServer = Preferences.Get("AxionalServer", "");
            AxionalDbms = Preferences.Get("ConexBd", "");
            AxionalVersionTxt = Preferences.Get("ConexVersion", "");
            AxionalVersion = Convert.ToInt16(AxionalVersionTxt);
            SGAUser = Preferences.Get("ConexUser", "");
            AxionalSoapUser = Preferences.Get("ConexUser", "");
            
            GetPass();

        }// Constructor por defecto

        async void GetPass()
        {
            var sgaPassword = await SecureStorage.GetAsync("ConexPass");
            SGAPassword = sgaPassword;
            AxionalSoapPass = sgaPassword;
            //TODO: Si el sgaPassword = empty o Null Tiene que salir un Error
        }

        private void setAuthorization()
        {
            axional = new Axional(AxionalServer, AxionalDbms, AxionalSoapUser, AxionalSoapPass, AxionalVersion);
        }

        private void setAuthorization(String _AxionalUser, String _AxionalPassword)
        {
            SGAUser = _AxionalUser;
            SGAPassword = _AxionalPassword;
            axional = new Axional(AxionalServer, AxionalDbms, AxionalSoapUser, AxionalSoapPass, AxionalVersion);
        }// end setAuthorization method

        private void setAuthorization(String _AxionalAuthorization)
        {
            AxionalAuthorization = _AxionalAuthorization;
            //axional = new Axional(AxionalServer, AxionalDbms, AxionalUser, AxionalPassword, AxionalVersion);
            axional = new Axional(AxionalServer, AxionalDbms, AxionalAuthorization);
            axional.SetVersion(AxionalVersion);
        }// end setAuthorization method

        public XmlNodeList executeAxionalSQL(String _sql)
        {
            setAuthorization();
            XmlNodeList rows = null;
            try
            {
                axional.executeSQL(_sql);
                xmlResponse.LoadXml(axional.GetStringResponse());
                XmlNodeList faultstring = xmlResponse.GetElementsByTagName("faultstring");
                isAuthorized = true;
                if (faultstring.Count.Equals(1))
                {
                    isAuthorized = false;
                }
                else
                {
                    faultstring = xmlResponse.GetElementsByTagName("errmessg");
                    isAuthorized = true;
                    if (faultstring.Count.Equals(1))
                    {
                        isAuthorized = false;
                        String xml = "<root><r><c>0</c><c>" + faultstring.Item(0).ChildNodes.Item(0).InnerText + "</c></r></root>";
                        XmlDocument xmldom = new XmlDocument();
                        xmldom.LoadXml(xml);
                        rows = xmldom.GetElementsByTagName("r");
                    }
                }
                if (isAuthorized)
                {
                    string strfaultstring = "";
                    XmlNodeList faultstring2 = xmlResponse.GetElementsByTagName("r");

                    if (faultstring2.Count.Equals(1))
                    {
                        strfaultstring = xmlResponse.GetElementsByTagName("r").Item(0).FirstChild.InnerText;
                    }

                    isAuthorized = true;
                    if (strfaultstring.Equals("0"))
                    {
                        AxionalError = xmlResponse.GetElementsByTagName("r").Item(0).ChildNodes.Item(1).InnerText;
                        isAuthorized = false;
                    }
                    rows = xmlResponse.GetElementsByTagName("r");
                }
                return rows;
            }
            catch (Exception e)
            {
                throw new Exception("No se ha podido conectar con Axional. Revise la conexión." + e.Message);
            }
        }// end executeSQL method

        public XmlNodeList executeAxionalXML(String _xml)
        {
            setAuthorization();
            XmlNodeList rows = null;
            try
            {
                axional.executeXML(_xml);
                xmlResponse.LoadXml(axional.GetStringResponse());
                XmlNodeList faultstring = xmlResponse.GetElementsByTagName("faultstring");
                isAuthorized = true;
                if (faultstring.Count.Equals(1))
                {
                    isAuthorized = false;
                }
                else
                {
                    faultstring = xmlResponse.GetElementsByTagName("errmessg");
                    isAuthorized = true;
                    if (faultstring.Count.Equals(1))
                    {
                        isAuthorized = false;
                        String xml = "<root><r><c>0</c><c>" + faultstring.Item(0).ChildNodes.Item(0).InnerText + "</c></r></root>";
                        XmlDocument xmldom = new XmlDocument();
                        xmldom.LoadXml(xml);
                        rows = xmldom.GetElementsByTagName("r");
                    }
                }
                if (isAuthorized)
                {
                    string strfaultstring = "";
                    XmlNodeList faultstring2 = xmlResponse.GetElementsByTagName("r");

                    if (faultstring2.Count.Equals(1))
                    {
                        strfaultstring = xmlResponse.GetElementsByTagName("r").Item(0).FirstChild.InnerText;
                    }

                    isAuthorized = true;
                    if (strfaultstring.Equals("0"))
                    {
                        AxionalError = xmlResponse.GetElementsByTagName("r").Item(0).ChildNodes.Item(1).InnerText;
                        isAuthorized = false;
                    }
                    rows = xmlResponse.GetElementsByTagName("r");
                }

                return rows;
            }
            catch (Exception e)
            {
                throw new Exception("No se ha podido conectar con Axional. Revise la conexión." + e.Message);
            }
        }// end executeXML method

        public byte[] executeAxionalOBJ(String objCode, String objcond, String[] nomvar, String[] valvar, String salida)
        {
            byte[] bytes = null;
            setAuthorization();
            try
            {
                axional.executeOBJ(objCode, objcond, nomvar, valvar, salida);
                xmlResponse.LoadXml(axional.GetStringResponse());
                XmlNodeList faultstring = xmlResponse.GetElementsByTagName("faultstring");
                isAuthorized = true;
                if (faultstring.Count.Equals(1))
                {
                    isAuthorized = false;
                }
                if (isAuthorized)
                {
                    XmlNodeList objectResponse = xmlResponse.GetElementsByTagName("object");
                    String base64String = objectResponse[0].FirstChild.Value;
                    // Convertimos la cadena base64 a bytes
                    bytes = Convert.FromBase64String(base64String);
                }

                return bytes;
            }
            catch (Exception e)
            {
                throw new Exception("No se ha podido conectar con Axional. Revise la conexión." + e.Message);
            }
        }// end executeXML method

        public String nullStringParse(XmlNode _input)
        {
            String returnstring = "";
            try
            {
                returnstring = _input.FirstChild.Value;
            }
            catch (Exception e)
            {
                int j = 0;
            }
            return returnstring;
        }// end nullStringParse method

        public Int16 nullInt16Parse(XmlNode _input)
        {
            Int16 returnint = 0;
            try
            {
                returnint = Int16.Parse(_input.FirstChild.Value);
            }
            catch (Exception e) { }
            return returnint;
        }// end nullInt16Parse method

        public Boolean nullBooleanParse(XmlNode _input)
        {
            Boolean returnint = false;
            try
            {
                returnint = Boolean.Parse(_input.FirstChild.Value);
            }
            catch (Exception e) { }
            return returnint;
        }// end nullBooleanParse 

        public DateTime? nullDateParse(XmlNode _input)
        {
            DateTime? returndate = null;
            try
            {
                returndate = DateTime.Parse(_input.FirstChild.Value);
            }
            catch (Exception e) { }
            return returndate;
        }// end nullDateParse method

        public DateTime DateParse(XmlNode _input)
        {
            DateTime returndate = DateTime.Today;
            try
            {
                returndate = DateTime.Parse(_input.FirstChild.Value);
            }
            catch (Exception e) { }
            return returndate;
        }// end DateParse method

        public XmlNodeList getAxional(String AxionalTabla, String TablaCondicion)
        {
            if (TablaCondicion == null)
            {
                TablaCondicion = "<null/>";
            }

            var xml = "<call name='ctl_appsga_download'>";
            xml += "<args>";
            xml += "<arg>" + AxionalTabla + "</arg>";
            xml += "<arg>" + SGAUser + "</arg>";
            xml += "<arg>" + TablaCondicion + "</arg>";
            xml += "</args>";
            xml += "</call>";

            XmlNodeList rows = executeAxionalXML(xml);
            return rows;
        }// end getAxional default method

        public XmlNodeList processAxional(String AxionalTabla, String Entidades, String Action)
        {

            var xml = "<call name='ctl_appsga_upload'>";
            xml += "<args>";
            xml += "<arg>" + AxionalTabla + "</arg>";
            xml += "<arg>" + Entidades + "</arg>";
            xml += "<arg>" + Action + "</arg>";
            xml += "</args>";
            xml += "</call>";

            XmlNodeList rows = executeAxionalXML(xml);
            return rows;
        }// end getAxional default method

        public abstract void sync(SGASynchronizationMode modo);

        public JObject toJSON()
        {
            JObject o = (JObject)JToken.FromObject(this);
            return o;
        }// end toJSON method

        //public T save<T>()
        //{
        //    db = new DatabaseContext();
        //    using (db.connection)
        //    {
        //        // Iniciamos transacción
        //        db.connection.BeginTransaction();
        //        try
        //        {
        //            save<T>(db.connection);
        //            // Commit Transaction
        //            db.connection.Commit();
        //        }
        //        catch (SQLite.SQLiteException e)
        //        {
        //            db.connection.Rollback();
        //            throw e;
        //        }//end try-catch
        //        db.connection.Dispose();
        //    }
        //    return (T)Convert.ChangeType(this, typeof(T));
        //}// end save method

        //public abstract T save<T>(SQLiteConnection cn);

        //public T delete<T>()
        //{
        //    db = new DatabaseContext();
        //    using (db.connection)
        //    {
        //        // Iniciamos transacción
        //        db.connection.BeginTransaction();
        //        try
        //        {
        //            delete<T>(db.connection);
        //            // Commit Transaction
        //            db.connection.Commit();
        //        }
        //        catch (SQLite.SQLiteException e)
        //        {
        //            db.connection.Rollback();
        //            throw e;
        //        }//end try-catch
        //        db.connection.Dispose();
        //    }

        //    return (T)Convert.ChangeType(this, typeof(T));
        //}// end delete method

        //public abstract T delete<T>(SQLiteConnection cn);

    }// end GMAOEntity class
}