using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers.Calculation
{
	[Access(RoleType.Admin, RoleType.Manager)]
	public partial class RegistryOfPaymentsController : Controller
	{
		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}
	}
}