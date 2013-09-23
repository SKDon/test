using System.IO;
using Alicargo.Services.Excel;

namespace Alicargo.Services.Abstract
{
	public interface IExcelGenerator
	{
		MemoryStream Get(ApplicationExcelRow[] rows);
	}
}
