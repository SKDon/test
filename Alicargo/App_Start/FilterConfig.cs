using System.Web.Mvc;
using Alicargo.Core.Services;
using Alicargo.MvcHelpers.Filters;
using Ninject;

namespace Alicargo.App_Start
{
    internal static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
		{
			filters.Add(new CustomHandleErrorAttribute(kernel.Get<ILog>()));

			filters.Add(new CultureFilterAttribute(CompositionRootHelper.GetTwoLetterISOLanguageName(kernel)));
		}
	}
}