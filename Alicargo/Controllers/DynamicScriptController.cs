using System.Web.Mvc;

namespace Alicargo.Controllers
{
	public partial class DynamicScriptController : Controller
	{
		private const string ApplicationJavascriptContentType = "application/javascript";

		protected override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			Response.ContentType = ApplicationJavascriptContentType;
		}

		public virtual PartialViewResult Roles()
		{
			return PartialView();
		}

		public virtual PartialViewResult Urls()
		{
			return PartialView();
		}

		public virtual PartialViewResult Localization()
		{
			return PartialView();
		}
	}
}
