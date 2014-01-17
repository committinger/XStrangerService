using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XSSFramework.Serialize
{
    public sealed class XmlUtils
    {
        private XmlUtils() { }

        public static string Serialize(object obj)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                using (StringWriter sw = new StringWriter())
                {
                    xs.Serialize(sw, obj);
                    return sw.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string SerializeData(object obj)
        {
            try
            {
                if (obj == null)
                    return null;
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                XmlWriterSettings xws = new XmlWriterSettings() { OmitXmlDeclaration = true };
                using (StringWriter sw = new StringWriter())
                using (XmlWriter xw = XmlWriter.Create(sw, xws))
                {
                    xs.Serialize(xw, obj);
                    xw.Flush();
                    return sw.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T Deserialize<T>(Stream stream)
        {
            if (stream == null)
                return default(T);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(stream);
        }
    }
}
