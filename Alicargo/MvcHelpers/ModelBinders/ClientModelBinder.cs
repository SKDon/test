using System.Web;
using System.Web.Mvc;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.ViewModels.User;

namespace Alicargo.MvcHelpers.ModelBinders
{
	internal sealed class ClientModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (ClientModel)base.BindModel(controllerContext, bindingContext);

			ReadFiles(controllerContext.HttpContext.Request, model);

			return model;
		}

		private static void ReadFiles(HttpRequestBase request, ClientModel model)
		{
			if (model.ContractFile == null && model.ContractFileName == null)
				request.ReadFile("ContractFile", (name, bytes) =>
				{
					model.ContractFileName = name;
					model.ContractFile = bytes;
				});
		}
	}
}