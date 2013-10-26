﻿using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class SenderCalculationController : Controller
	{
		private readonly ISenderCalculationPresenter _presenter;
		private readonly ISenderRepository _senders;
		private readonly IIdentityService _identity;

		public SenderCalculationController(ISenderCalculationPresenter presenter, ISenderRepository senders, IIdentityService identity)
		{
			_presenter = presenter;
			_senders = senders;
			_identity = identity;
		}

		[Access(RoleType.Sender), HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[Access(RoleType.Sender), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			Debug.Assert(_identity.Id != null);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			if (!senderId.HasValue)
			{
				throw new InvalidDataException();
			}

			var data = _presenter.List(senderId.Value, take, skip);

			return Json(data);
		}

	}
}
