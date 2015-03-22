using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Core.Tests.Services
{
	[TestClass]
	public class SerializerTests
	{
		[TestMethod]
		public void Test_Serializer()
		{
			var serializer = new Serializer();
			var fixture = new Fixture();
			var data = fixture.Create<ApplicationEditData>();

			var bytes = serializer.Serialize(data);

			var actual = serializer.Deserialize<ApplicationEditData>(bytes);

			actual.ShouldBeEquivalentTo(data);
		}
	}
}
