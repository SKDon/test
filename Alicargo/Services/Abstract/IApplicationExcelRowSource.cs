using Alicargo.Services.Excel.Rows;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		BaseApplicationExcelRow[] GetAdminApplicationExcelRow(string language);
		BaseApplicationExcelRow[] GetForwarderApplicationExcelRow(long forwarderId, string language);
		BaseApplicationExcelRow[] GetSenderApplicationExcelRow(string language);
	}
}