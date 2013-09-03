using System;
using System.Web.Mvc;
using Alicargo.Core.Localization;

namespace Alicargo.Helpers
{
    internal sealed class CultureFilter : IAuthorizationFilter
	{
		private readonly Func<string> _getTwoLetterISOLanguageName;

		public CultureFilter(Func<string> getTwoLetterISOLanguageName)
		{
			_getTwoLetterISOLanguageName = getTwoLetterISOLanguageName;
		}

	    public void OnAuthorization(AuthorizationContext filterContext)
		{
			CultureContext.Current.Set(_getTwoLetterISOLanguageName);
	    }
	}
}