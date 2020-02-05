using System;
using System.IO;
using System.Xml.Serialization;

namespace SiriusScientific.Core.Persistence
{
	public static class XmlSerialization
	{
		public static void SerializeToFile<T>(string fileName, T obj)
		{
			XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];

			//XmlSerializer serializer = new XmlSerializer(typeof(T));

			try
			{
				TextWriter textWriter = new StreamWriter(fileName);

				serializer.Serialize(textWriter, obj);

				textWriter.Close();
			}
			catch (DirectoryNotFoundException e)
			{
#if DEBUG
				Console.WriteLine(e);
#endif
				serializer = null;
			}
		}

		public static T DeserializeFromFile<T>(string fileName)
		{
			T obj = default(T);

			StreamReader streamReader;

			XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];

//			XmlSerializer serializer = new XmlSerializer(typeof(T));

			try
			{
				streamReader = new StreamReader(fileName);
			}

			catch (Exception)
			{
				return default(T);
			}

			try
			{
				obj = (T)serializer.Deserialize(streamReader);
			}

			catch (Exception)
			{
				return default(T);
			}

			streamReader.Close();

			serializer = null;

			return obj;
		}
	}
}
