using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel
{
	public sealed class SenderApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

		public SenderApplicationExcelRow(ApplicationListItem application)
		{
			_application = application;
		}
	}
}