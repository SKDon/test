using System.IO;

namespace Alicargo.Services.Abstract
{
	public interface IExcelGenerator<in T>
	{
		MemoryStream Get(T[] rows, string language);
	}
}
