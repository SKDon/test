using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel.Rows;

namespace Alicargo.Controllers
{
	public partial class ExcelController : Controller
	{
		private readonly IExcelGenerator<BaseApplicationExcelRow> _generator;
		private readonly IForwarderRepository _forwarders;
		private readonly ICarrierRepository _carriers;
		private readonly ISenderRepository _senders;
		private readonly IApplicationExcelRowSource _rowSource;
		private readonly IIdentityService _identity;

		public ExcelController(
			IExcelGenerator<BaseApplicationExcelRow> generator,
			IForwarderRepository forwarders,
			ICarrierRepository carriers,
			ISenderRepository senders,
			IApplicationExcelRowSource rowSource,
			IIdentityService identity)
		{
			_generator = generator;
			_forwarders = forwarders;
			_carriers = carriers;
			_senders = senders;
			_rowSource = rowSource;
			_identity = identity;
		}

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender, RoleType.Carrier)]
		public virtual FileResult Applications()
		{
			var stream = GetStream();

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "applications.xlsx");
		}

		private MemoryStream GetStream()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				var rows = _rowSource.GetAdminApplicationExcelRow(_identity.Language);

				return _generator.Get(rows, _identity.Language);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				Debug.Assert(_identity.Id != null);

				var forwarderId = _forwarders.GetByUserId(_identity.Id.Value);

				if(!forwarderId.HasValue)
				{
					throw new InvalidLogicException("Expected that current user is forwarder");
				}

				var rows = _rowSource.GetForwarderApplicationExcelRow(forwarderId.Value, _identity.Language);

				return _generator.Get(rows, _identity.Language);
			}

			if(_identity.IsInRole(RoleType.Carrier))
			{
				Debug.Assert(_identity.Id != null);

				var carrierId = _carriers.GetByUserId(_identity.Id.Value);

				if(!carrierId.HasValue)
				{
					throw new InvalidLogicException("Expected that current user is carrier");
				}

				var rows = _rowSource.GetCarrierApplicationExcelRow(carrierId.Value, _identity.Language);

				return _generator.Get(rows, _identity.Language);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				Debug.Assert(_identity.Id != null);

				var senderId = _senders.GetByUserId(_identity.Id.Value);

				if(!senderId.HasValue)
				{
					throw new InvalidLogicException("Expected that current user is sender");
				}

				var rows = _rowSource.GetSenderApplicationExcelRow(senderId.Value, _identity.Language);

				return _generator.Get(rows, _identity.Language);
			}

			throw new NotSupportedException("For other roles the method is not supported");
		}
	}
}
