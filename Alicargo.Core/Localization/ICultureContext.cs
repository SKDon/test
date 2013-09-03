using System;

namespace Alicargo.Core.Localization
{
	public interface ICultureContext
	{
		void Set(Func<string> getTwoLetterISOLanguageName);
		string GetTwoLetterISOLanguageName();
	}
}