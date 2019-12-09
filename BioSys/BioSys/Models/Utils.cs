using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace BioSys.Models
{
    public class Utils
    {

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static string GetPropertyValue(object o, string s)
        {
            //StringWriter sw = new StringWriter();
            //return o.GetType().GetProperty(s).GetValue(o, null).ToString();
            try
            {
                return o.GetType().GetProperties().Single(pi => pi.Name == s).GetValue(o, null).ToString();
            }
            catch(NullReferenceException)
            {
                return "";
            }       
            
            //sw.ToString();
        }

        public class MultiDimDictList<K, T> : Dictionary<K, List<T>>
        {
            public void Add(K key, T addObject)
            {
                if (!ContainsKey(key)) Add(key, new List<T>());
                if (!base[key].Contains(addObject)) base[key].Add(addObject);
            }
        }

    }
}