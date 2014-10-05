using System.IO;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel;

namespace Alicargo.Controllers.Calculation
{
	public partial class SenderCalculationController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly ISenderCalculationPresenter _presenter;
		private readonly ISenderRepository _senders;

		public SenderCalculationController(ISenderCalculationPresenter presenter, ISenderRepository senders,
			IIdentityService identity)
		{
			_presenter = presenter;
			_senders = senders;
			_identity = identity;
		}

		[Access(RoleType.Sender)]
		public virtual FileResult Excel()
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			if(!senderId.HasValue)
			{
				throw new InvalidDataException();
			}

			var data = _presenter.List(senderId.Value, int.MaxValue, 0);

			var excel = new ExcelSenderCalculation();

			var stream = excel.Get(data, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}

		[HttpGet]
		[Access(RoleType.Sender)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Sender)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			if(!senderId.HasValue)
			{
				throw new InvalidDataException();
			}

			var data = _presenter.List(senderId.Value, take, skip);

			return Json(data);
		}
	}
}