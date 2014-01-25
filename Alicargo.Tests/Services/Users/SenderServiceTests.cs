using Alicargo.Services.Users;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Services.Users
{
	[TestClass]
	public class SenderServiceTests
	{
		private MockContainer _context;
		private SenderService _service;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_service = _context.Create<SenderService>();
		}

		[TestMethod]
		public void Test_GetByCountryOrAny_GetFirstInCountry()
		{
			var oldSenderId = _context.Create<long>();

			var first = _context.Create<long>();

			_context.SenderRepository.Setup(x => x.GetByCountry(TestConstants.TestCountryId))
				.Returns(new[] { first, _context.Create<long>(), _context.Create<long>() });

			var actual = _service.GetByCountryOrAny(TestConstants.TestCountryId, oldSenderId);

			actual.ShouldBeEquivalentTo(first);
		}

		[TestMethod]
		public void Test_GetByCountryOrAny_OldSenderInCountryList()
		{
			var oldSenderId = _context.Create<long>();

			_context.SenderRepository.Setup(x => x.GetByCountry(TestConstants.TestCountryId))
				.Returns(new[] { _context.Create<long>(), _context.Create<long>(), oldSenderId });

			var actual = _service.GetByCountryOrAny(TestConstants.TestCountryId, oldSenderId);

			actual.ShouldBeEquivalentTo(oldSenderId);
		}
	}
}