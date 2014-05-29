using System;
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
	public class AwbFileRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private AwbFileRepository _repository;

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

			_repository = new AwbFileRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_Add_Get_Delete()
		{
			var bytes = _fixture.Create<byte[]>();
			var name = _fixture.Create<string>();

			var id = _repository.Add(TestConstants.TestAwbId, AwbFileType.AWB, name, bytes);
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
			const AwbFileType fileType = (AwbFileType)(-1);

			_repository.Add(TestConstants.TestAwbId, fileType, name[0], bytes);
			_repository.Add(TestConstants.TestAwbId, fileType, name[1], bytes);

			_repository.GetNames(TestConstants.TestAwbId,fileType)
		}
	}
}
