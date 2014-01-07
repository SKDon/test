using System;

namespace Alicargo.Core.Localization
{
	public interface ICultureProvider
	{
		void Set(Func<string> language);
		string GetLanguage();
	}
}