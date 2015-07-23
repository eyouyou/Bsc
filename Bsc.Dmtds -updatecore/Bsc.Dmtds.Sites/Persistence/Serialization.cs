using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Bsc.Dmtds.Common.IO;

namespace Bsc.Dmtds.Sites.Persistence
{
    public class Serialization
    {
        public static T DeserializeSettings<T>(string filePath)
        {
            return Deserialize<T>(filePath);
        }
        public static void Serialize<T>(T o, string filePath)
        {
            Serialize(o, new[] { typeof(T) }, filePath);
        }
        public static void Serialize<T>(T o, IEnumerable<Type> knownTypes, string filePath)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(T), knownTypes);
            string folderPath = Path.GetDirectoryName(filePath);
            IOUtility.EnsureDirectoryExists(folderPath);
            var settings = new XmlWriterSettings()
            {
                CheckCharacters = false,
                Indent = true,
                IndentChars = "\t"
            };
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    ser.WriteObject(writer, o);
                }
            }
        }
        public static T Deserialize<T>(string filePath)
        {
            return (T)Deserialize(typeof(T), new[] { typeof(T) }, filePath);
        }
        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, string filePath)
        {
            DataContractSerializer ser = new DataContractSerializer(type, knownTypes);
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (stream.Length > 0)
                {
                    try
                    {
                        return ser.ReadObject(stream);
                    }
                    catch (Exception e)
                    {
                        Bsc.Dmtds.Common.HealthMonitoring.Log.LogException(e);

                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
        } 
    }
}