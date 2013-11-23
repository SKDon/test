using System.Reflection;

namespace Alicargo.MvcHelpers
{
	public static class PathConstants
	{
		public const string CommonJs = "~/js";
		public const string RuJs = "~/js/ru";
		public const string ItJs = "~/js/it";
		public const string AwbListJs = "~/js/awb-list-js";
		public const string ApplicationListJs = "~/js/app-list-js";
		public const string ClientListJs = "~/js/client-list-js";
		public const string CalculationJs = "~/js/calc-js";
		public const string ClientCalculationJs = "~/js/client-calc-js";
		public const string SenderCalculationJs = "~/js/sender-calc-js";
		public const string UserListJs = "~/js/user-list-js";
		public const string StateListJs = "~/js/state-list-js";
		public const string StateEditJs = "~/js/state-edit-js";
		public const string StylesPath = "~/css";

		public static readonly string Version =
			Assembly.GetExecutingAssembly().GetName().Version.ToString();

		static PathConstants() {}
		
	}
}