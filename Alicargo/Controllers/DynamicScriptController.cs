using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using Alicargo.MvcHelpers;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	[CustomContentType(ContentType = "application/javascript", Order = 2)]
	public partial class DynamicScriptController : Controller
	{
		public virtual PartialViewResult Roles()
		{
			return PartialView();
		}

		[OutputCache(Duration = 3600, Location = OutputCacheLocation.ServerAndClient)]
		public virtual PartialViewResult Urls()
		{
			var urs = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(x => x.BaseType == typeof(Controller) && !x.Name.StartsWith("T4MVC_") && x.Name.EndsWith("Controller"))
				.Select(x => new ReflectedControllerDescriptor(x))
				.SelectMany(x => x.GetCanonicalActions())
				.Select(x => new DynamicScriptMethodDescription
				{
					Action = x.ActionName,
					Controller = x.ControllerDescriptor.ControllerName
				})
				.ToArray();

			return PartialView(urs);
		}

		public virtual PartialViewResult Localization()
		{
			return PartialView();
		}
	}
}
