using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Ninject;

namespace Alicargo.MvcHelpers
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