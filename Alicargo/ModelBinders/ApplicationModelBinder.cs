using System.Web;
using System.Web.Mvc;
using Alicargo.Helpers;
using Alicargo.ViewModels.Application;

namespace Alicargo.ModelBinders
{
	// todo: finish the binder to fill CarrierSelectModel and ClientId
	internal sealed class ApplicationModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (ApplicationModel)base.BindModel(controllerContext, bindingContext);

			ReadFiles(model, controllerContext.HttpContext.Request);

			return model;
		}

		private static void ReadFiles(ApplicationModel model, HttpRequestBase request)
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

			if (model.DeliveryBillFile == null && model.DeliveryBillFileName == null)
				request.ReadFile("DeliveryBillFile", (name, bytes) =>
				{
					model.DeliveryBillFileName = name;
					model.DeliveryBillFile = bytes;
				});

			if (model.Torg12File == null && model.Torg12FileName == null)
				request.ReadFile("Torg12File", (name, bytes) =>
				{
					model.Torg12FileName = name;
					model.Torg12File = bytes;
				});

			if (model.CPFile == null && model.CPFileName == null)
				request.ReadFile("CPFile", (name, bytes) =>
				{
					model.CPFileName = name;
					model.CPFile = bytes;
				});
		}
	}
}