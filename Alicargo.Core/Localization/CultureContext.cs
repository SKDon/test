using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Alicargo.Core.Localization
{
	// todo: refactor
	public sealed class CultureContext : ICultureContext
	{
		private CultureContext() { }

		static CultureContext()
		{
			Current = new CultureContext();
		}

		public static ICultureContext Current { get; set; }

		public void Set(Func<string> language)
		{
			var languageName = language();

			var culture = CultureInfo.GetCultures(CultureTypes.NeutralCultures).First(x =>
				x.TwoLetterISOLanguageName.Equals(languageName,
					StringComparison.InvariantCultureIgnoreCase));

			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		public string GetLanguage()
		{
			return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
		}
	}
}
