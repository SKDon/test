using System.Globalization;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Core.Tests.Helpers
{
	[TestClass]
	public class LocalizationHelperTests
	{
		[TestMethod]
		public void Test_GetDeliveryType()
		{
			LocalizationHelper.GetDeliveryType(DeliveryType.ToDoor, CultureInfo.GetCultureInfo("ru"))
				.ShouldBeEquivalentTo("До двери");
		}

		[TestMethod]
		public void Test_GetMethodOfDelivery()
		{
			LocalizationHelper.GetMethodOfDelivery(MethodOfDelivery.Avia, CultureInfo.GetCultureInfo("ru"))
				.ShouldBeEquivalentTo("Авиа");
		}
	}
}