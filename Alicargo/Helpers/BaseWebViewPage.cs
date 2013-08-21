using System.Web.Mvc;
using Alicargo.Services.Abstract;
using Ninject;

namespace Alicargo.Helpers
{
    public abstract class BaseWebViewPage : WebViewPage
	{
		[Inject]
		public IIdentityService IdentityService { get; set; }

		[Inject]
		public IStateConfig StateConfig { get; set; }
	}

	public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
	{
		[Inject]
		public IIdentityService IdentityService { get; set; }

		[Inject]
		public IStateConfig StateConfig { get; set; }
	}
}