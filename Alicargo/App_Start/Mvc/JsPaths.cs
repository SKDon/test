using Alicargo.MvcHelpers.BundleHelpres;

namespace Alicargo.Mvc
{
	public static class JsPaths
	{
		[Path("~/Scripts/jQuery/jquery-{version}.js",
		 "~/Scripts/jQuery/jquery-ui-{version}.js",
		 "~/Scripts/jQuery/jquery.validate.js",
		 "~/Scripts/jQuery/jquery.validate.unobtrusive.js",
		 "~/Scripts/jQuery/globalize/globalize.js",
		 "~/Scripts/jQuery/jquery.cookie.js",
		 "~/Content/bootstrap/js/bootstrap.js",
		 "~/Content/bootstrap/fileupload/bootstrap-fileupload.js",
		 "~/Scripts/kendo/2014.1.318/kendo.web.min.js",
		 "~/Scripts/app/CurrencyType.js",
		 "~/Scripts/app/Common.js",
		 "~/Scripts/app/Alicargo.js")]
		public const string CommonJs = "~/js";

		[Path("~/Scripts/jQuery/globalize/cultures/globalize.culture.ru.js",
		 "~/Scripts/kendo/2014.1.318/cultures/kendo.culture.ru.min.js",
		 "~/Scripts/app/ru.js")]
		public const string RuJs = "~/js/ru";

		[Path("~/Scripts/jQuery/globalize/cultures/globalize.culture.it.js",
		 "~/Scripts/kendo/2014.1.318/cultures/kendo.culture.it.min.js",
		 "~/Scripts/app/it.js")]
		public const string ItJs = "~/js/it";

		[Path("~/Scripts/app/AirWaybill/Columns.js", "~/Scripts/app/AirWaybill/Grid.js")]
		public const string AwbListJs = "~/js/awb-list-js";

		[Path("~/Scripts/app/Application/Columns.js", "~/Scripts/app/Application/Grid.js")]
		public const string ApplicationListJs = "~/js/app-list-js";

		[Path("~/Scripts/app/Client/Grid.js")]
		public const string ClientListJs = "~/js/client-list-js";

		[Path("~/scripts/app/calculation/admin/Columns.js",
		 "~/scripts/app/calculation/admin/DataSource.js",
		 "~/scripts/app/calculation/admin/Grid.js")]
		public const string CalculationJs = "~/js/calc-js";

		[Path("~/scripts/app/calculation/client/Columns.js", "~/scripts/app/calculation/client/Grid.js")]
		public const string ClientCalculationJs = "~/js/client-calc-js";

		[Path("~/scripts/app/calculation/sender/Columns.js", "~/scripts/app/calculation/sender/Grid.js")]
		public const string SenderCalculationJs = "~/js/sender-calc-js";

		[Path("~/Scripts/app/User/Grid.js")]
		public const string UserListJs = "~/js/user-list-js";

		[Path("~/Scripts/app/State/Grid.js")]
		public const string StateListJs = "~/js/state-list-js";

		[Path("~/Scripts/app/State/Edit.js")]
		public const string StateEditJs = "~/js/state-edit-js";

		[Path("~/Scripts/app/Template/Grid.js")]
		public const string TemplateListJs = "~/js/template-list-js";

		[Path("~/Scripts/app/Template/Edit.js")]
		public const string TemplateEditJs = "~/js/template-edit-js";

		[Path("~/Scripts/app/files.js")]
		public const string FilesJs = "~/js/files-js";

		[Path("~/Scripts/app/City/Grid.js")]
		public const string CityListJs = "~/js/city-list-js";

		[Path("~/Scripts/app/Country/Grid.js")]
		public static string CountryListJs = "~/js/сountry-list-js";
	}
}