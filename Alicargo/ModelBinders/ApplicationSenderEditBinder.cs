using System.Web;
using System.Web.Mvc;
using Alicargo.MvcHelpers;
using Alicargo.ViewModels.Application;

namespace Alicargo.ModelBinders
{
	internal sealed class ApplicationSenderEditModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (ApplicationSenderEdit)base.BindModel(controllerContext, bindingContext);

			ReadFiles(model, controllerContext.HttpContext.Request);

			return model;
		}

		private static void ReadFiles(ApplicationSenderEdit model, HttpRequestBase request)
		{
			if (model.InvoiceFile == null && model.InvoiceFileName == null)
				request.ReadFile("InvoiceFile", (name, bytes) =>
				{
					model.InvoiceFileName = name;
					model.InvoiceFile = bytes;
				});

			if (model.PackingFile == null && model.PackingFileName == null)
				request.ReadFile("PackingFile", (name, bytes) =>
				{
					model.PackingFileName = name;
					model.PackingFile = bytes;
				});

			if (model.SwiftFile == null && model.SwiftFileName == null)
				request.ReadFile("SwiftFile", (name, bytes) =>
				{
					model.SwiftFileName = name;
					model.SwiftFile = bytes;
				});
		}
	}
}