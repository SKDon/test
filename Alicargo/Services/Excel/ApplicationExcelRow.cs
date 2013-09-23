using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel
{
	public sealed class ApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

		public ApplicationExcelRow(ApplicationListItem application)
		{
			_application = application;
		}
	}
}