using System;
using System.Web.Mvc;

namespace Alicargo.Helpers
{
	public sealed class CustomHandleErrorInfo : HandleErrorInfo
	{
		public string Uid { get; private set; }

		public CustomHandleErrorInfo(Exception exception, string controllerName, string actionName, string uid)
			: base(exception, controllerName, actionName)
		{
			Uid = uid;
		}
	}
}