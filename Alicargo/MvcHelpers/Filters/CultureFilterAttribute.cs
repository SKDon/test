using System;
using System.Web.Mvc;
using Alicargo.Core.Localization;

namespace Alicargo.MvcHelpers.Filters
{
    internal sealed class CultureFilterAttribute : IAuthorizationFilter
	{
		private readonly Func<string> _getTwoLetterISOLanguageName;

		public CultureFilterAttribute(Func<string> getTwoLetterISOLanguageName)
		{
			_getTwoLetterISOLanguageName = getTwoLetterISOLanguageName;
		}

	    public void OnAuthorization(AuthorizationContext filterContext)
		{
			CultureContext.Current.Set(_getTwoLetterISOLanguageName);
	    }
	}
}