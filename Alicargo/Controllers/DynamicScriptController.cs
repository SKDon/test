using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	[CustomContentType(ContentType = "application/javascript", Order = 2)]
	public partial class DynamicScriptController : Controller
	{
		private readonly IStateConfig _stateConfig;

		public DynamicScriptController(IStateConfig stateConfig)
		{
			_stateConfig = stateConfig;
		}

		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual PartialViewResult Roles()
		{
			return PartialView();
		}

		[OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.ServerAndClient)]
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

		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual PartialViewResult Localization()
		{
			return PartialView();
		}

		[OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.ServerAndClient)]
		public virtual PartialViewResult States()
		{
			var model = _stateConfig.GetType()
									.GetProperties(BindingFlags.Public | BindingFlags.Instance)
									.Where(x => x.PropertyType == typeof(long))
									.ToDictionary(x => x.Name, x => (long)x.GetValue(_stateConfig));

			return PartialView(model);
		}
	}
}