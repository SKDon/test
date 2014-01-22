using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Controllers.User
{
	public partial class ForwarderController : Controller
	{
		private readonly ICityRepository _cities;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;

		public ForwarderController(
			ICityRepository cities,
			IForwarderRepository forwarders,
			IIdentityService identity)
		{
			_cities = cities;
			_forwarders = forwarders;
			_identity = identity;
		}

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Create()
		{
			BindBag();

			return View();
		}

		private void BindBag()
		{
			ViewBag.Cities = _cities.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);
		}

		[Access(RoleType.Admin, RoleType.Sender)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			BindBag();

			var data = _forwarders.Get(id);

			var model = new ForwarderModel
			{
				Authentication = new AuthenticationModel(data.Login),
				CityId = data.CityId,
				Email = data.Email,
				Name = data.Name
			};

			return View(model);
		}
	}
}