using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;

namespace Alicargo.MvcHelpers.Extensions
{
	internal static class FileExtensions
	{
		public static byte[] GetBytes(this HttpPostedFileBase file)
		{
			if (file == null) return null;

			var bytes = new byte[file.ContentLength];
			file.InputStream.Read(bytes, 0, file.ContentLength);

			return bytes;
		}

		public static FileResult GetFileResult(this FileHolder file)
		{
			const string contentType = "application/octet-stream";
			var ms = new MemoryStream();

			ms.Write(file.FileData, 0, file.FileData.Length);
			ms.Position = 0;

			return new FileStreamResult(ms, contentType) { FileDownloadName = file.FileName };
		}		

		// todo: 1. test for download files of an application
		public static void ReadFile(this HttpRequestBase request, string id, Action<string, byte[]> action)
		{
			var file = request.Files[id + "Data"];
			var name = request.Form[id + "Name"];

			if (file != null)
			{
				action(name ?? file.FileName, file.GetBytes());
			}
			else
			{
				action(name, null);
			}
		}

		public static void ReadFile(this HttpPostedFileBase file, Action<byte[]> setData, Action<string> setName)
		{
			if (file != null)
			{
				setData(file.GetBytes());
				setName(file.FileName);
			}
			else
			{
				setData(null);
				setName(null);
			}
		}
	}
}