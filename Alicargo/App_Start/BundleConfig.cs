using System.Reflection;
using System.Web.Optimization;
using Alicargo.App_Start;
using Alicargo.MvcHelpers;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(BundleConfig), "RegisterBundles")]

namespace Alicargo.App_Start
{
	internal sealed class BundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.IgnoreList.Clear();
			BundleTable.Bundles.IgnoreList.Ignore(".intellisense.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore("-vsdoc.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore(".debug.js", OptimizationMode.Always);

			BindConstants();

			BundleTable.Bundles.Add(new StyleRelativePathTransformBundle(PathConstants.StylesPath)
				.Include(
					"~/Content/themes/jMetro/jquery-ui.css",
					"~/Content/bootstrap/css/bootstrap.css",
					"~/Content/bootstrap/css/bootstrap-responsive.css",
					"~/Content/bootstrap/fileupload/bootstrap-fileupload.css",
					"~/Content/kendo/2013.3.1119/kendo.common.min.css",
					"~/Content/kendo/2013.3.1119/kendo.default.min.css",
					"~/Content/app/kendo.css",
					"~/Content/app/layout.css",
					"~/Content/app/common.css",
					"~/Content/app/entity.css",
					"~/Content/app/calculation-grid.css"
				));

			//BundleTable.EnableOptimizations = true;
		}

		private static void BindConstants()
		{
			var fields = typeof (PathConstants).GetFields();

			foreach (var field in fields)
			{
				var attribute = field.GetCustomAttribute<PathAttribute>();
				if (attribute != null)
				{
					var value = (string) field.GetValue(null);
					BundleTable.Bundles.Add(new ScriptBundle(value).Include(attribute.Paths));
				}
			}
		}
	}
}