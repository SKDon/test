using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Optimization;
using Alicargo.Mvc;
using Alicargo.MvcHelpers.BundleHelpres;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(BundleConfig), "RegisterBundles")]

namespace Alicargo.Mvc
{
	internal sealed class BundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.IgnoreList.Clear();
			BundleTable.Bundles.IgnoreList.Ignore(".intellisense.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore("-vsdoc.js", OptimizationMode.Always);
			BundleTable.Bundles.IgnoreList.Ignore(".debug.js", OptimizationMode.Always);

			BindConstants(typeof(JsPaths).GetFields(), value => new ScriptBundle(value));
			BindConstants(typeof(CssPaths).GetFields(), value => new StyleRelativePathTransformBundle(value));

			//BundleTable.EnableOptimizations = true;
		}

		private static void BindConstants(IEnumerable<FieldInfo> fields, Func<string, Bundle> getBundle)
		{
			foreach (var field in fields)
			{
				var attribute = field.GetCustomAttribute<PathAttribute>();
				if (attribute != null)
				{
					var value = (string)field.GetValue(null);
					var include = getBundle(value);

					include.Include(attribute.Paths);

					BundleTable.Bundles.Add(include);
				}
			}
		}
	}
}