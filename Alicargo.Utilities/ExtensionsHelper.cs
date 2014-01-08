using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Alicargo.Utilities
{
	public static class ExtensionsHelper
	{
		public static long ToLong(this string @string)
		{
			return long.Parse(@string, NumberStyles.AllowLeadingSign);
		}

		public static int ToInt(this string @string)
		{
			return int.Parse(@string, NumberStyles.AllowLeadingSign);
		}

		public static bool IsCritical(this Exception ex)
		{
			while (true)
			{
				if (ex == null) return false;

				if (ex is OutOfMemoryException) return true;
				if (ex is TargetInvocationException) return true;
				if (ex is AccessViolationException) return true;
				if (ex is COMException) return true;
				if (ex is AppDomainUnloadedException) return true;
				if (ex is BadImageFormatException) return true;
				if (ex is CannotUnloadAppDomainException) return true;
				if (ex is InvalidProgramException) return true;
				if (ex is StackOverflowException) return true;
				if (ex is ThreadAbortException) return true;

				ex = ex.InnerException;
			}
		}
	}
}