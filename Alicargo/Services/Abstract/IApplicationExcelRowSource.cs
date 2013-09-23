using Alicargo.Services.Excel;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		ApplicationExcelRow[] GetByClient(long clientId);
	}
}