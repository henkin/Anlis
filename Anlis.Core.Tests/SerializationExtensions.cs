using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Anlis.Core.Tests
{
    public static class SerializationExtensions
    {
        public static void SerializeToBinary(this object o, string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            var bFormatter = new XmlSerializer(o.GetType());
            bFormatter.Serialize(stream, o);
            stream.Close();
        }

        public static T DeSerializeSerialize<T>(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            var bFormatter = new XmlSerializer(typeof(T));
            T o = (T)bFormatter.Deserialize(stream);
            stream.Close();
            return o;
        }
    }
}