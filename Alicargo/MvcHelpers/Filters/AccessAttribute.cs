using System;
using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.MvcHelpers.Filters
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class AccessAttribute : FilterAttribute
	{
		private readonly RoleType[] _roles;

		public AccessAttribute(params RoleType[] roles)
		{
			_roles = roles;
		}

		public RoleType[] Roles
		{
			get { return _roles; }
		}
	}
}