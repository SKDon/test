using System;
using System.IO;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel.Rows;

namespace Alicargo.Controllers
{
	public partial class ExcelController : Controller
	{
		private readonly IExcelGenerator<BaseApplicationExcelRow> _generator;
		private readonly IApplicationExcelRowSource _rowSource;
		private readonly IIdentityService _identity;

		public ExcelController(
			IExcelGenerator<BaseApplicationExcelRow> generator,
			IApplicationExcelRowSource rowSource,
			IIdentityService identity)
		{
			_generator = generator;
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
				var rows = _rowSource.GetAdminApplicationExcelRow();

				return _generator.Get(rows, _identity.Language);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				var rows = _rowSource.GetForwarderApplicationExcelRow();

				return _generator.Get(rows, _identity.Language);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				var rows = _rowSource.GetSenderApplicationExcelRow();

				return _generator.Get(rows, _identity.Language);
			}

			throw new NotSupportedException("For other roles the method is not supported");
		}
	}
}
