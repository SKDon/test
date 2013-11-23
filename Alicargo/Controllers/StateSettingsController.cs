using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public partial class StateSettingsController : Controller
    {
	    private readonly IStateSettingsRepository _settings;

	    public StateSettingsController(IStateSettingsRepository settings)
	    {
		    _settings = settings;
	    }

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Index(long id)
		{
			return View();
		}
    }
}