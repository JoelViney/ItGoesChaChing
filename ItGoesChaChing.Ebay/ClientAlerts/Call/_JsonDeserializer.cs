using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	internal class JsonDeserializer
	{
		internal T DeSerialiseObject<T>(string jsonString)
		{
			T output;

			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
			settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss.fffZ");

			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), settings);

			using (Stream stream = GenerateStreamFromString(jsonString))
			{
				var obj = (T)serializer.ReadObject(stream);
				output = (T)obj;
			}

			return output;
		}

		private Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
