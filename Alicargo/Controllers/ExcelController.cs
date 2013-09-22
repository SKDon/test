using System.Diagnostics;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ExcelController : Controller
	{
		private readonly IExcelGenerator _generator;
		private readonly IIdentityService _identity;
		private readonly IClientRepository _clients;

		public ExcelController(
			IExcelGenerator generator,
			IIdentityService identity,
			IClientRepository clients)
		{
			_generator = generator;
			_identity = identity;
			_clients = clients;
		}

		[Access(RoleType.Client)]
		public virtual FileResult Applications()
		{
			Debug.Assert(_identity.Id != null);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var stream = _generator.Applications(client.Id);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Applications.xlsx");
		}
	}
}
