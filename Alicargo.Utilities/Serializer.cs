using System.IO;
using System.Text;
using Alicargo.Contracts.Helpers;
using Newtonsoft.Json;

namespace Alicargo.Core.Services
{
	public sealed class Serializer : ISerializer
	{
		private readonly JsonSerializer _serializer = new JsonSerializer();

		public byte[] Serialize<T>(T data)
		{
			if (Equals(data, default(T)))
			{
				return null;
			}

			using (var stream = new MemoryStream())
			using (var writer = new StreamWriter(stream, Encoding.UTF8))
			{
				_serializer.Serialize(writer, data, typeof(T));
				writer.Flush();

				return stream.ToArray();
			}
		}

		//public string GetJson<T>(T data)
		//{
		//	if (Equals(data, default(T)))
		//	{
		//		return null;
		//	}
		//	var builder = new StringBuilder();
		//	var writer = new StringWriter(builder);
		//	_serializer.Serialize(writer, data, typeof(T));
		//	writer.Flush();

		//	return builder.ToString();
		//}

		public T Deserialize<T>(byte[] data)
		{
			if (data == null)
			{
				return default(T);
			}

			using (var stream = new MemoryStream(data))
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				return (T)new JsonSerializer().Deserialize(reader, typeof(T));
			}
		}
	}
}