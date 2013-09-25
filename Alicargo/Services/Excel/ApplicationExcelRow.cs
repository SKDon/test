using Alicargo.Core.Localization;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Excel
{
	public abstract class ApplicationExcelRow
	{
		protected ApplicationExcelRow(ApplicationListItem application)
		{
			Application = application;
		}

		protected ApplicationListItem Application { get; private set; }

		[DisplayNameLocalized(typeof (Entities), "DisplayNumber")]
		public string DisplayNumber
		{
			get { return Application.DisplayNumber; }
		}

		[DisplayNameLocalized(typeof (Entities), "FactoryName")]
		public string FactoryName
		{
			get { return Application.FactoryName; }
		}

		[DisplayNameLocalized(typeof (Entities), "Mark")]
		public string MarkName
		{
			get { return Application.MarkName; }
		}
	}
}