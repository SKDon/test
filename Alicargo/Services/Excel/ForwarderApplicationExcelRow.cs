using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel
{
	public sealed class ForwarderApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

		public ForwarderApplicationExcelRow(ApplicationListItem application)
		{
			_application = application;
		}
	}
}