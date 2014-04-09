using Alicargo.MvcHelpers.BundleHelpres;

namespace Alicargo.Mvc
{
	public sealed class CssPaths
	{
		[Path("~/Content/themes/jMetro/jquery-ui.css",
			"~/Content/bootstrap/css/bootstrap.css",
			"~/Content/bootstrap/css/bootstrap-responsive.css",
			"~/Content/bootstrap/fileupload/bootstrap-fileupload.css",
			"~/Content/kendo/2014.1.318/kendo.common.min.css",
			"~/Content/kendo/2014.1.318/kendo.default.min.css",
			"~/Content/app/kendo.css",
			"~/Content/app/layout.css",
			"~/Content/app/common.css",
			"~/Content/app/calculation-grid.css",
			"~/Content/app/payment.css")] 
		public const string Common = "~/css";
	}
}