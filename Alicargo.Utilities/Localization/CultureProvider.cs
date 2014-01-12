using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Alicargo.Utilities.Localization
{
	public static class CultureProvider
	{
		private static ICultureProvider _current;

		static CultureProvider()
		{
			SetProvider(new DefaultCultureProvider());
		}

		public static void SetProvider(ICultureProvider value)
		{
			_current = value;
		}

		public static void Set(Func<string> language)
		{
			_current.Set(language);
		}

		public static string Get()
		{
			return _current.GetLanguage();
		}

		public static CultureInfo GetCultureInfo()
		{
			return CultureInfo.GetCultureInfo(_current.GetLanguage());
		}

		private sealed class DefaultCultureProvider : ICultureProvider
		{
			void ICultureProvider.Set(Func<string> language)
			{
				var languageName = language();

				var culture = CultureInfo.GetCultures(CultureTypes.NeutralCultures).First(x =>
					x.TwoLetterISOLanguageName.Equals(languageName,
						StringComparison.InvariantCultureIgnoreCase));

				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;
			}

			string ICultureProvider.GetLanguage()
			{
				return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			}
		}

		public interface ICultureProvider
		{
			void Set(Func<string> language);
			string GetLanguage();
		}
	}
}