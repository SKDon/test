using System.Web.Mvc;
using Alicargo.Core.Localization;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Ninject;
using log4net;

namespace Alicargo.App_Start
{
	public static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
		{
			filters.Add(new CustomHandleErrorAttribute(kernel.Get<ILog>()));

			filters.Add(new CultureActionFilter(() => kernel.Get<IIdentityService>().TwoLetterISOLanguageName
			                                               ?? TwoLetterISOLanguageName.Russian));
		}
	}
}