using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Helpers
{
	internal sealed class StyleRelativePathTransform : IBundleTransform
	{
		private readonly Regex _pattern;

		public StyleRelativePathTransform()
		{
			_pattern = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase);
		}

		public void Process(BundleContext context, BundleResponse response)
		{
			TransformUrls(context, response);
		}

		private void TransformUrls(BundleContext context, BundleResponse response)
		{
			response.Content = String.Empty;

			var builder = new StringBuilder();

			foreach (var cssFileInfo in response.Files)
			{
				if (cssFileInfo.VirtualFile == null) continue;

				string content;
				using (var streamReader = new StreamReader(cssFileInfo.VirtualFile.Open())) { content = streamReader.ReadToEnd(); }
				if (content.IsNullOrWhiteSpace()) continue;

				var matches = _pattern.Matches(content);
				if (matches.Count > 0)
				{
					foreach (Match match in matches)
					{
						var cssRelativeUrl = match.Groups[2].Value;
						var rootRelativeUrl = TransformUrl(context, cssRelativeUrl, cssFileInfo);

						var quote = match.Groups[1].Value;
						var replace = String.Format("url({0}{1}{0})", quote, rootRelativeUrl);
						content = content.Replace(match.Groups[0].Value, replace);
					}
				}

				builder.AppendLine(content);
			}

			response.ContentType = "text/css";
			response.Content = builder.ToString();
		}

		private static string TransformUrl(BundleContext context, string cssRelativeUrl, BundleFile cssFileInfo)
		{
			if (cssRelativeUrl.StartsWith("http://") || cssRelativeUrl.StartsWith("https://")
				|| cssRelativeUrl.StartsWith("/") || cssRelativeUrl.StartsWith("data:image"))
			{
				return cssRelativeUrl;
			}

			var cssFilePath = context.HttpContext.Server.MapPath(cssFileInfo.IncludedVirtualPath);
			Debug.Assert(cssFileInfo.VirtualFile.Name != null);
			var cssFileDirectory = cssFilePath.Substring(0, cssFilePath.Length - cssFileInfo.VirtualFile.Name.Length);

			var path = Path.GetFullPath(Path.Combine(cssFileDirectory, cssRelativeUrl));
			return RelativeFromAbsolutePath(context.HttpContext, path);
		}

		private static string RelativeFromAbsolutePath(HttpContextBase context, string path)
		{
			var request = context.Request;
			var applicationPath = request.PhysicalApplicationPath;
			var virtualDir = request.ApplicationPath;
			virtualDir = virtualDir == "/" ? virtualDir : (virtualDir + "/");

			Debug.Assert(applicationPath != null);
			return path.Replace(applicationPath, virtualDir).Replace(Path.DirectorySeparatorChar, '/');
		}
	}
}