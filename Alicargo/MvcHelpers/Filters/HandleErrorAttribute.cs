using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Alicargo.Contracts.Exceptions;
using Alicargo.Core.Services.Abstract;
using WebGrease.Css.Extensions;

namespace Alicargo.MvcHelpers.Filters
{
    internal sealed class CustomHandleErrorAttribute : HandleErrorAttribute
	{
		private readonly ILog _log;

		public CustomHandleErrorAttribute(ILog log)
		{
			_log = log;
		}

		public override void OnException(ExceptionContext filterContext)
		{
			if (filterContext.ExceptionHandled) return;

			var uid = Guid.NewGuid().ToString();

			Log(filterContext, uid);

			HandleError(filterContext, uid);
		}

		private void HandleError(ExceptionContext filterContext, string uid)
		{
			var exception = filterContext.Exception;

			if (!filterContext.IsChildAction)
			{
				if (exception is EntityNotFoundException)
				{
					HandleErrorImpl(filterContext, uid);
					filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					return;
				}

				if (exception is AccessForbiddenException)
				{
					HandleErrorImpl(filterContext, uid);
					filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
					return;
				}

				if (exception is InvalidLogicException)
				{
					HandleErrorImpl(filterContext, uid);
					filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
					return;
				}
			}

			base.OnException(filterContext);
		}

		private void HandleErrorImpl(ExceptionContext filterContext, string uid)
		{
			var controllerName = (string)filterContext.RouteData.Values["controller"];
			var actionName = (string)filterContext.RouteData.Values["action"];
			var model = new CustomHandleErrorInfo(filterContext.Exception, controllerName, actionName, uid);
			filterContext.Result = new ViewResult
			{
				ViewName = View,
				MasterName = Master,
				ViewData = new ViewDataDictionary<CustomHandleErrorInfo>(model),
				TempData = filterContext.Controller.TempData
			};
			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
		}

		private void Log(ExceptionContext filterContext, string uid)
		{
			var builder = new StringBuilder();

			builder.AppendLine("Uid: " + uid);

			if (filterContext.HttpContext.Request.Url != null)
				builder.Append("Url: ").Append(filterContext.HttpContext.Request.Url.OriginalString);

			var form = filterContext.HttpContext.Request.Form;
			if (form != null && form.AllKeys.Any())
			{
				builder.AppendLine().Append("Form: ");
				form.AllKeys.Where(x => x != "__RequestVerificationToken")
					.ForEach(x => builder.AppendLine().Append('\t').Append(x).Append(": ").Append(form[x]));
			}

			_log.Error(builder.ToString(), filterContext.Exception);
		}
	}
}