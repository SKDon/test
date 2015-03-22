using System.Web.Mvc;

namespace Alicargo.MvcHelpers.Filters
{
	internal sealed class CustomContentTypeAttribute : ActionFilterAttribute
	{
		public string ContentType { get; set; }

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			filterContext.HttpContext.Response.ContentType = ContentType;
		}
	}
}