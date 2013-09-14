using System.Web.Mvc;
using Alicargo.MvcHelpers;
using Ninject;
using log4net;

namespace Alicargo.App_Start
{
    internal static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
		{
			filters.Add(new CustomHandleErrorAttribute(kernel.Get<ILog>()));

			filters.Add(new CultureFilter(CompositionRootHelper.GetTwoLetterISOLanguageName(kernel)));
		}
	}
}