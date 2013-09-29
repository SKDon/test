using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Calculation
{
	internal sealed class ClientCalculationExcelRow
	{
		private readonly ApplicationListItemData _application;

		public ClientCalculationExcelRow(ApplicationListItemData application)
		{
			_application = application;
		}
	}
}