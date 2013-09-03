using System;
using System.Web.Mvc;
using Alicargo.Core.Localization;

namespace Alicargo.Helpers
{
    internal sealed class CultureActionFilter : ActionFilterAttribute
	{
		private readonly Func<string> _getTwoLetterISOLanguageName;

		public CultureActionFilter(Func<string> getTwoLetterISOLanguageName)
		{
			_getTwoLetterISOLanguageName = getTwoLetterISOLanguageName;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			CultureContext.Current.Set(_getTwoLetterISOLanguageName);
		}
	}
}