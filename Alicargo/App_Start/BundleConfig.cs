using System.Web.Optimization;
using Alicargo.Helpers;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Alicargo.App_Start.BundleConfig), "RegisterBundles")]

namespace Alicargo.App_Start
{
    internal class BundleConfig
	{
        public static void RegisterBundles()
		{
			BundleTable.Bundles.IgnoreList.Clear();
			BundleTable.Bundles.IgnoreList.Ignore(".intellisense.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore("-vsdoc.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore(".debug.js", OptimizationMode.Always);

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ScriptsPath)
				.Include(
					"~/Scripts/jquery-{version}.js",
					"~/Scripts/jquery-ui-{version}.js",
					"~/Scripts/jquery.validate.js",
					"~/Scripts/jquery.validate.unobtrusive.js",
					"~/Content/bootstrap/js/bootstrap.js",
					"~/Content/bootstrap/fileupload/bootstrap-fileupload.js",
					"~/Scripts/jquery.globalize/globalize.js",
					"~/Scripts/kendo/2013.2.716/kendo.web.min.js",
					"~/Scripts/app/CurrencyType.js",
					"~/Scripts/app/Common.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ScriptsPathRu)
				.Include(
					"~/Scripts/jquery.globalize/cultures/globalize.culture.ru.js",
					"~/Scripts/kendo/2013.2.716/cultures/kendo.culture.ru.min.js",
					"~/Scripts/app/ru.js"
				));

			BundleTable.Bundles.Add(new ScriptBundle(PathConstants.ScriptsPathIt)
				.Include(
					"~/Scripts/jquery.globalize/cultures/globalize.culture.it.js",
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
					"~/Content/app/entity.css"
				));
		}
	}
}
