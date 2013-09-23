using Alicargo.Core.Localization;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Excel
{
	public sealed class ApplicationExcelRow
	{
		private readonly ApplicationListItem _application;

		public ApplicationExcelRow(ApplicationListItem application)
		{
			_application = application;
		}

		[DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string DisplayNumber { get { return _application.DisplayNumber; } }

		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get { return _application.FactoryName; } }

		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get { return _application.MarkName; } }
	}
}