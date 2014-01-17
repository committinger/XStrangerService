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
            if (obj == null || !obj.GetType().IsSerializable)
                return string.Empty;
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        public static string SerializeData(object obj)
        {
            if (obj == null || !obj.GetType().IsSerializable)
                return string.Empty;
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

        public static T Deserialize<T>(Stream stream)
        {
            if (stream == null)
                return default(T);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(stream);
        }
    }
}
