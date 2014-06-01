using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.Application
{
	[TestClass]
	public class ApplicationFileRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private ApplicationFileRepository _repository;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_repository = new ApplicationFileRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_Add_Get_Delete()
		{
			var bytes = _fixture.Create<byte[]>();
			var name = _fixture.Create<string>();

			var id = _repository.Add(TestConstants.TestApplicationId, ApplicationFileType.CP, name, bytes);
			var file = _repository.Get(id);
			_repository.Delete(id);

			file.Data.ShouldAllBeEquivalentTo(bytes);
			file.Name.ShouldBeEquivalentTo(name);
			_repository.Get(id).Should().BeNull();
		}

		[TestMethod]
		public void Test_GetNames()
		{
			var bytes = _fixture.Create<byte[]>();
			var name = _fixture.CreateMany<string>(2).ToArray();
			const ApplicationFileType fileType = (ApplicationFileType)(-1);

			var id1 = _repository.Add(TestConstants.TestApplicationId, fileType, name[0], bytes);
			var id2 = _repository.Add(TestConstants.TestApplicationId, fileType, name[1], bytes);

			var names = _repository.GetNames(TestConstants.TestApplicationId, fileType);

			names.Should().HaveCount(2);
			names[0].Id.ShouldBeEquivalentTo(id1);
			names[1].Id.ShouldBeEquivalentTo(id2);
			names[0].Name.ShouldBeEquivalentTo(name[0]);
			names[1].Name.ShouldBeEquivalentTo(name[1]);
		}

		[TestMethod]
		public void Test_GetInfo()
		{
			var bytes = _fixture.Create<byte[]>();
			var name = _fixture.CreateMany<string>(2).ToArray();
			const ApplicationFileType fileType = (ApplicationFileType)(-1);

			var id1 = _repository.Add(TestConstants.TestApplicationId, fileType, name[0], bytes);
			var id2 = _repository.Add(TestConstants.TestApplicationId, fileType, name[1], bytes);

			var dictionary = _repository.GetInfo(new[] { TestConstants.TestApplicationId }, fileType);

			dictionary.Should().HaveCount(1);
			var names = dictionary[0];
			names.Should().HaveCount(2);
			names[0].Id.ShouldBeEquivalentTo(id1);
			names[1].Id.ShouldBeEquivalentTo(id2);
			names[0].Name.ShouldBeEquivalentTo(name[0]);
			names[1].Name.ShouldBeEquivalentTo(name[1]);
		}
	}
}