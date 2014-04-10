using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;

namespace Alicargo.MvcHelpers.Filters
{
	internal sealed class AccessAuthorizationFilter : IAuthorizationFilter
	{
		private readonly Func<IIdentityService> _identityService;

		public AccessAuthorizationFilter(Func<IIdentityService> identityService)
		{
			_identityService = identityService;
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			var roles = filterContext.ActionDescriptor
				.GetFilterAttributes(false)
				.OfType<AccessAttribute>()
				.SelectMany(x => x.Roles)
				.ToArray();

			if(roles.Length == 0)
			{
				return;
			}

			if(!roles.Any(x => _identityService().IsInRole(x)))
			{
				filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}
		}
	}
}