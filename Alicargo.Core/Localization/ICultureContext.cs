using System;

namespace Alicargo.Core.Localization
{
	public interface ICultureContext
	{
		void Set(Func<string> language);
		string GetLanguage();
	}
}