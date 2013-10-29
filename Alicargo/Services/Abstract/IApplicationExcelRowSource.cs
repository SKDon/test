using Alicargo.Services.Excel.Rows;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		AdminApplicationExcelRow[] GetAdminApplicationExcelRow();
		ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow();
		SenderApplicationExcelRow[] GetSenderApplicationExcelRow();
	}
}