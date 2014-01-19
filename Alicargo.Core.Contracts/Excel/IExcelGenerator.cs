using System.IO;

namespace Alicargo.Core.Contracts.Excel
{
	public interface IExcelGenerator<in T>
	{
		MemoryStream Get(T[] rows, string language);
	}
}
