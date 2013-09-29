using Alicargo.Services.Excel;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		AdminApplicationExcelRow[] GetAdminApplicationExcelRow();
		ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow();
		SenderApplicationExcelRow[] GetSenderApplicationExcelRow();
	}
}