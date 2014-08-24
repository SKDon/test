using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers.Calculation
{
	[Access(RoleType.Admin, RoleType.Manager)]
	public partial class RegistryOfPaymentsController : Controller
	{
		private readonly IClientBalanceRepository _clientBalance;

		public RegistryOfPaymentsController(IClientBalanceRepository clientBalance)
		{
			_clientBalance = clientBalance;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var data = _clientBalance.GetRegistryOfPayments();

			return Json(new ListCollection<RegistryOfPaymentsData>
			{
				Data = data,
				Groups = null,
				Total = data.Length
			});
		}
	}
}