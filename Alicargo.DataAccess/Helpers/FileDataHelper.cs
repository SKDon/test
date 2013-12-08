using System;

namespace Alicargo.DataAccess.Helpers
{
	internal static class FileDataHelper
	{
		[Obsolete]
		public static void SetFile(byte[] file, string name, Action<byte[]> setFile, Action<string> setName)
		{
			if (file != null && file.Length > 0)
			{
				setFile(file);
				setName(name);
			}
			else if (string.IsNullOrWhiteSpace(name))
			{
				setFile(null);
				setName(null);
			}
		}
	}
}