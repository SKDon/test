using System;
using System.Web.Mvc;
using Alicargo.Utilities.Localization;

namespace Alicargo.MvcHelpers.Filters
{
    internal sealed class CultureFilterAttribute : IAuthorizationFilter
	{
		private readonly Func<string> _getLanguage;

		public CultureFilterAttribute(Func<string> getLanguage)
		{
			_getLanguage = getLanguage;
		}

	    public void OnAuthorization(AuthorizationContext filterContext)
		{
			CultureProvider.Current.Set(_getLanguage);
	    }
	}
}