using System.IO;

namespace Alicargo.Core.Services
{
	public interface IExcelGenerator
	{
		MemoryStream Get<T>(T[] rows, string twoLetterISOLanguageName);
	}
}
