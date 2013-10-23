using System.Reflection;

namespace Alicargo.MvcHelpers
{
	public static class PathConstants
	{
		public const string CommonJs = "~/js";
		public const string RuJs = "~/js/ru";
		public const string ItJs = "~/js/it";
		public const string AwbGridJs = "~/js/awb-grid-js";
		public const string ApplicationListJs = "~/js/app-list-js";
		public const string StylesPath = "~/css";

		public static readonly string Version =
			Assembly.GetExecutingAssembly().GetName().Version.ToString();

		static PathConstants() { }
	}
}