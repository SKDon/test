using System;
using System.IO;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Helpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class ClientExcelHelperTests
	{
		private Fixture _fixture;
		private Mock<DateTimeProvider.IDateTimeProvider> _dateTime;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_dateTime = new Mock<DateTimeProvider.IDateTimeProvider>(MockBehavior.Strict);
			_dateTime.SetupGet(x => x.Now).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));

			DateTimeProvider.SetProvider(_dateTime.Object);
		}

		[TestMethod]
		public void Test_GetWithInvalidFileNameChars()
		{
			var invalidString = Path.GetInvalidFileNameChars().Aggregate(" \t\r\n", (s, c) => s + c);

			var clientData = _fixture.Create<ClientData>();
			clientData.Nic = invalidString + invalidString;
			clientData.Language = TwoLetterISOLanguageName.English;

			var actual = ClientExcelHelper.GetName(clientData);

			actual.ShouldBeEquivalentTo("calculation_1_1_2000___________________________________________________________________________________________.xlsx");
		}
	}
}
