using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class SettingRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private SettingRepository _repository;
		private Serializer _serializer;

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
			_serializer = new Serializer();

			_repository = new SettingRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString), _serializer);
		}

		[TestMethod]
		public void Test_Add()
		{
			var setting = _fixture.Create<Setting>();
			setting.Type = (SettingType)(-1);

			setting = _repository.AddOrReplace(setting);

			var actual = _repository.Get(setting.Type);

			actual.ShouldBeEquivalentTo(setting);
		}

		[TestMethod]
		[ExpectedException(typeof(UpdateConflictException))]
		public void Test_EntityUpdateConflictException()
		{
			var setting = _fixture.Build<Setting>()
				.With(x => x.Type, SettingType.Bill)
				.Create();

			_repository.AddOrReplace(setting);
		}

		[TestMethod]
		public void Test_GetUnknown()
		{
			var setting = _repository.Get((SettingType)(-1));

			setting.Should().BeNull();
		}

		[TestMethod]
		public void Test_Update()
		{
			var old = _repository.Get(SettingType.Bill);

			var setting = _fixture.Build<Setting>()
				.With(x => x.Type, old.Type)
				.With(x => x.RowVersion, old.RowVersion)
				.Create();

			setting = _repository.AddOrReplace(setting);

			var actual = _repository.Get(old.Type);

			actual.ShouldBeEquivalentTo(setting);
		}
	}
}