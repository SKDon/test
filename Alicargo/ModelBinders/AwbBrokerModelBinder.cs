using System.Web;
using System.Web.Mvc;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.ModelBinders
{
	internal sealed class AwbBrokerModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (AwbBrokerModel)base.BindModel(controllerContext, bindingContext);

			ReadFiles(controllerContext.HttpContext.Request, model);

			return model;
		}

		private static void ReadFiles(HttpRequestBase request, AwbBrokerModel model)
		{
			if (model.GTDFile == null && model.GTDFileName == null)
				request.ReadFile("GTDFile", (name, bytes) =>
				{
					model.GTDFileName = name;
					model.GTDFile = bytes;
				});

			if (model.GTDAdditionalFile == null && model.GTDAdditionalFileName == null)
				request.ReadFile("GTDAdditionalFile", (name, bytes) =>
				{
					model.GTDAdditionalFileName = name;
					model.GTDAdditionalFile = bytes;
				});

			if (model.PackingFile == null && model.PackingFileName == null)
				request.ReadFile("PackingFile", (name, bytes) =>
				{
					model.PackingFileName = name;
					model.PackingFile = bytes;
				});

			if (model.InvoiceFile == null && model.InvoiceFileName == null)
				request.ReadFile("InvoiceFile", (name, bytes) =>
				{
					model.InvoiceFileName = name;
					model.InvoiceFile = bytes;
				});
		}
	}
}