using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Excel;
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
		private readonly IApplicationExcelRowSource _rowSource;
		private readonly IIdentityService _identity;

		public ExcelController(
			IExcelGenerator<BaseApplicationExcelRow> generator,
			IForwarderRepository forwarders,
			IApplicationExcelRowSource rowSource,
			IIdentityService identity)
		{
			_generator = generator;
			_forwarders = forwarders;
			_rowSource = rowSource;
			_identity = identity;
		}

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
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

			if (_identity.IsInRole(RoleType.Sender))
			{
				var rows = _rowSource.GetSenderApplicationExcelRow(_identity.Language);

				return _generator.Get(rows, _identity.Language);
			}

			throw new NotSupportedException("For other roles the method is not supported");
		}
	}
}
