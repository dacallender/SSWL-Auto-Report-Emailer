using System.IO;
using Newtonsoft.Json;

namespace SiriusScientific.Core.Persistence
{
	public static class JsonSerialization
	{
		public static void Serialize<T>(T obj, string path, bool append)
		{
			if (append)
			{
				File.AppendAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				}));
			}

			else File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			}));
		}

		public static T Deserialize<T>(string path)
		{
			if (!File.Exists(path))
			{
				using (var fileStream = File.Create(path))
				{
					fileStream.Close();
				}
			}

			using (StreamReader file = File.OpenText(path))
			{
				JsonSerializer serializer = new JsonSerializer()
				{
					NullValueHandling = NullValueHandling.Ignore
				};

				return (T)serializer.Deserialize(file, typeof(T));
			}
		}
	}
}