using Alicargo.Services.Users;
using Alicargo.TestHelpers;
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
	}
}