using System.Web.Optimization;
using Alicargo.App_Start;
using Alicargo.MvcHelpers;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof (BundleConfig), "RegisterBundles")]

namespace Alicargo.App_Start
{
	internal class BundleConfig
	{
		public static void RegisterBundles()
		{
			RegisterCommon();

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ApplicationListJs)
				.Include(
					"~/Scripts/app/Application/Columns.js",
					"~/Scripts/app/Application/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.AwbListJs)
				.Include(
					"~/Scripts/app/AirWaybill/Columns.js",
					"~/Scripts/app/AirWaybill/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ClientListJs)
				.Include(
					"~/Scripts/app/Client/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.UserListJs)
				.Include(
					"~/Scripts/app/User/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.StateListJs)
				.Include(
					"~/Scripts/app/State/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.TemplateListJs)
				.Include(
					"~/Scripts/app/Template/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.TemplateEditJs)
				.Include(
					"~/Scripts/app/Template/Edit.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.StateEditJs)
				.Include(
					"~/Scripts/app/State/Edit.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.CalculationJs)
				.Include(
					"~/scripts/app/calculation/admin/Columns.js",
					"~/scripts/app/calculation/admin/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ClientCalculationJs)
				.Include(
					"~/scripts/app/calculation/client/Columns.js",
					"~/scripts/app/calculation/client/Grid.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.SenderCalculationJs)
				.Include(
					"~/scripts/app/calculation/sender/Columns.js",
					"~/scripts/app/calculation/sender/Grid.js"
				));

			//BundleTable.EnableOptimizations = true;
		}

		private static void RegisterCommon()
		{
			BundleTable.Bundles.IgnoreList.Clear();
			BundleTable.Bundles.IgnoreList.Ignore(".intellisense.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore("-vsdoc.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore(".debug.js", OptimizationMode.Always);

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.CommonJs)
				.Include(
					"~/Scripts/jquery-{version}.js",
					"~/Scripts/jquery-ui-{version}.js",
					"~/Scripts/jquery.validate.js",
					"~/Scripts/jquery.validate.unobtrusive.js",
					"~/Content/bootstrap/js/bootstrap.js",
					"~/Content/bootstrap/fileupload/bootstrap-fileupload.js",
					"~/Scripts/globalize/globalize.js",
					"~/Scripts/kendo/2013.2.716/kendo.web.min.js",
					"~/Scripts/jquery.cookie.js",
					"~/Scripts/app/CurrencyType.js",
					"~/Scripts/app/Common.js",
					"~/Scripts/app/Alicargo.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.RuJs)
				.Include(
					"~/Scripts/globalize/cultures/globalize.culture.ru.js",
					"~/Scripts/kendo/2013.2.716/cultures/kendo.culture.ru.min.js",
					"~/Scripts/app/ru.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ItJs)
				.Include(
					"~/Scripts/globalize/cultures/globalize.culture.it.js",
					"~/Scripts/kendo/2013.2.716/cultures/kendo.culture.it.min.js",
					"~/Scripts/app/it.js"
				));

			BundleTable.Bundles.Add(new StyleRelativePathTransformBundle(PathConstants.StylesPath)
				.Include(
					"~/Content/themes/jMetro/jquery-ui.css",
					"~/Content/bootstrap/css/bootstrap.css",
					"~/Content/bootstrap/css/bootstrap-responsive.css",
					"~/Content/bootstrap/fileupload/bootstrap-fileupload.css",
					"~/Content/kendo/2013.2.716/kendo.common.min.css",
					"~/Content/kendo/2013.2.716/kendo.default.min.css",
					"~/Content/app/kendo.css",
					"~/Content/app/layout.css",
					"~/Content/app/common.css",
					"~/Content/app/entity.css",
					"~/Content/app/calculation-grid.css"
				));
		}
	}
}