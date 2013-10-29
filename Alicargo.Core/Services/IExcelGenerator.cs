using System.IO;

namespace Alicargo.Core.Services
{
	public interface IExcelGenerator<in T>
	{
		MemoryStream Get(T[] rows, string twoLetterISOLanguageName);
	}
}
