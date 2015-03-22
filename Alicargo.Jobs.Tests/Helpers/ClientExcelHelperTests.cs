using System;
using System.IO;
using System.Linq;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class ClientExcelHelperTests
	{
		private Mock<DateTimeProvider.IDateTimeProvider> _dateTime;

		[TestInitialize]
		public void TestInitialize()
		{
			_dateTime = new Mock<DateTimeProvider.IDateTimeProvider>(MockBehavior.Strict);
			_dateTime.SetupGet(x => x.Now).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));

			DateTimeProvider.SetProvider(_dateTime.Object);
		}

		[TestMethod]
		public void Test_EscapeFileName()
		{
			var invalidString = Path.GetInvalidFileNameChars().Aggregate(" \t\r\n", (s, c) => s + c);

			invalidString.EscapeFileName().ShouldBeEquivalentTo("_____________________________________________");
		}
	}
}