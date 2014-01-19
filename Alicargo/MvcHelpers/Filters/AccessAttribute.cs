using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Abstract;
using Ninject;

namespace Alicargo.MvcHelpers.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class AccessAttribute : FilterAttribute, IAuthorizationFilter
	{
		private readonly RoleType[] _roles;

		[Inject]
		public IIdentityService IdentityService { get; set; }

		public AccessAttribute(params RoleType[] roles)
		{
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!_roles.Any(x => IdentityService.IsInRole(x)))
			{
				filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
			}
		}
	}
}