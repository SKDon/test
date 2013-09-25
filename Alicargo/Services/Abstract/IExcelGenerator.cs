using System.IO;
using Alicargo.Services.Excel;

namespace Alicargo.Services.Abstract
{
	public interface IExcelGenerator
	{
		MemoryStream Get<T>(T[] rows) where T : ApplicationExcelRow;
	}
}
