using System.Diagnostics;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel;

namespace Alicargo.Controllers.Calculation
{
	public partial class ClientCalculationController : Controller
    {
		private readonly IClientCalculationPresenter _presenter;
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;

		public ClientCalculationController(IClientCalculationPresenter presenter, IClientRepository clients, IIdentityService identity)
		{
			_presenter = presenter;
			_clients = clients;
			_identity = identity;
		}

		[Access(RoleType.Client), HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[Access(RoleType.Client), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var data = _presenter.List(client.Id, take, skip);

			return Json(data);
		}

		[Access(RoleType.Client)]
		public virtual FileResult Excel()
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var data = _presenter.List(client.Id, int.MaxValue, 0);

			var excel = new ExcelClientCalculation();

			var stream = excel.Get(data, _identity.TwoLetterISOLanguageName);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}
    }
}
