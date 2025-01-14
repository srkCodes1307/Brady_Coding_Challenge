using System.Xml.Serialization;

namespace BradyCodeChallenge.Helper
{
    public static class XmlHelper
    {
        public static void SerializeToXmlAndSave<T>(T obj, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var streamWriter = new StreamWriter(filePath))
            {
                serializer.Serialize(streamWriter, obj);
            }
        }


        public static T DeserializeXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fs);
            }
        }
    }
}
