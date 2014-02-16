using System;
using System.Globalization;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Utilities.Tests
{
	[TestClass]
	public class ExtensionsHelperTests
	{
		[TestMethod]
		public void Test_ToDouble()
		{
			"0.01".ToDouble().ShouldBeEquivalentTo(0.01);
		}

		[TestMethod]
		public void Test_ToDouble_RuCurrentCulture()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

			"0.01".ToDouble().ShouldBeEquivalentTo(0.01);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Test_Exception()
		{
			"0,01".ToDouble();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void Test_Exception_RuCurrentCulture()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

			"0,01".ToDouble().ShouldBeEquivalentTo(0.01);
		}
	}
}
