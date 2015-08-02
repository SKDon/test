using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Alicargo.Utilities
{
	public static class ExtensionsHelper
	{
		public static string EscapeFileName(this string fileName)
		{
			return Regex.Replace(fileName, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "\\s]", "_");
		}

		public static bool IsCritical(this Exception ex)
		{
			while(true)
			{
				if(ex == null) return false;

				if(ex is OutOfMemoryException) return true;
				if(ex is TargetInvocationException) return true;
				if(ex is AccessViolationException) return true;
				if(ex is COMException) return true;
				if(ex is AppDomainUnloadedException) return true;
				if(ex is BadImageFormatException) return true;
				if(ex is CannotUnloadAppDomainException) return true;
				if(ex is InvalidProgramException) return true;
				if(ex is StackOverflowException) return true;
				if(ex is ThreadAbortException) return true;

				ex = ex.InnerException;
			}
		}

		public static int ToInt(this string @string)
		{
			return int.Parse(@string, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
		}

		public static double ToDouble(this string @string)
		{
			return double.Parse(@string, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
		}

		public static long ToLong(this string @string)
		{
			return long.Parse(@string, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
		}

		public static TOut With<TIn, TOut>(this TIn self, Func<TIn, TOut> func, TOut @default = default (TOut))
		{
			return ReferenceEquals(self, default(TIn))
				? @default
				: func(self);
		}
	}
}