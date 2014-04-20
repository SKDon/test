using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using Alicargo.Core.Contracts.State;
using Alicargo.MvcHelpers.Filters;
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
		public virtual PartialViewResult Localization()
		{
			return PartialView();
		}

		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual PartialViewResult Roles()
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
					Controller = x.ControllerDescriptor.ControllerName,
					Area = GetAreaName(x.ControllerDescriptor.ControllerType)
				})
				.Distinct()
				.ToArray();

			return PartialView(urs);
		}

		private static string GetAreaName(Type controllerType)
		{
			var fullName = controllerType.FullName;

			const string areas = "Alicargo.Areas.";

			var controllerName = "." + controllerType.Name + ".Controllers";

			return fullName.StartsWith(areas, StringComparison.InvariantCultureIgnoreCase)
				? fullName.Remove(fullName.Length - controllerName.Length).Remove(0, areas.Length)
				: null;
		}
	}
}