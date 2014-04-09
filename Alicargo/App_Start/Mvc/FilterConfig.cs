using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.MvcHelpers.Filters;
using Ninject;

namespace Alicargo.Mvc
{
	internal static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
		{
			filters.Add(new CustomHandleErrorAttribute(kernel.Get<ILog>()));

			filters.Add(new CultureFilterAttribute(CompositionRootHelper.GetLanguage(kernel)));
		}
	}
}