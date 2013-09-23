using System.IO;

namespace Alicargo.Services.Abstract
{
	public interface IExcelGenerator
	{
		MemoryStream Get<T>(T[] rows);
	}
}
