using System.Diagnostics;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Calculation;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers.Calculation
{
	public partial class ClientCalculationController : Controller
    {
		private readonly IClientCalculationPresenter _presenter;
		private readonly IExcelClientCalculation _excel;
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;

		public ClientCalculationController(
			IClientCalculationPresenter presenter,
			IExcelClientCalculation excel,
			IClientRepository clients, 
			IIdentityService identity)
		{
			_presenter = presenter;
			_excel = excel;
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

			var data = _presenter.List(client.ClientId, take, skip);

			return Json(data);
		}

		[Access(RoleType.Client)]
		public virtual FileResult Excel()
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var data = _presenter.List(client.ClientId, int.MaxValue, 0);

			var stream = _excel.Get(data.Groups, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}
    }
}
