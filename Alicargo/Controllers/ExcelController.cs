using System;
using System.IO;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ExcelController : Controller
	{
		private readonly IExcelGenerator _generator;
		private readonly IApplicationExcelRowSource _rowSource;
		private readonly IIdentityService _identity;


		public ExcelController(
			IExcelGenerator generator,
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

				return _generator.Get(rows);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				var rows = _rowSource.GetForwarderApplicationExcelRow();

				return _generator.Get(rows);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				var rows = _rowSource.GetSenderApplicationExcelRow();

				return _generator.Get(rows);
			}

			throw new NotSupportedException("For other roles the method is not supported");
		}
	}
}
