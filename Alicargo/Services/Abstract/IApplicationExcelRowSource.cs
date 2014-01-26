using Alicargo.Services.Excel.Rows;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		AdminApplicationExcelRow[] GetAdminApplicationExcelRow(string language);
		ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow(long forwarderId, string language);
		SenderApplicationExcelRow[] GetSenderApplicationExcelRow(string language);
	}
}