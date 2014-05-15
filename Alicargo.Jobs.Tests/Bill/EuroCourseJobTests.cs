using System;
using System.Text;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.Bill;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Bill
{
	[TestClass]
	public class EuroCourseJobTests
	{
		private EuroCourseJob _job;
		private Mock<ISettingRepository> _settings;
		private Mock<IHttpClient> _httpClient;
		private Mock<ISerializer> _serializer;
		private Mock<IHolder<DateTimeOffset>> _previousTime;
		private DateTimeOffset _now;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_now = new DateTimeOffset(new DateTime(2000, 1, 1));
			_settings = new Mock<ISettingRepository>(MockBehavior.Strict);
			_httpClient = new Mock<IHttpClient>(MockBehavior.Strict);
			_serializer = new Mock<ISerializer>(MockBehavior.Strict);
			_previousTime = new Mock<IHolder<DateTimeOffset>>(MockBehavior.Strict);
			DateTimeProvider.SetProvider(new DateTimeProviderStub(_now));
			_fixture = new Fixture();

			_job = new EuroCourseJob(_settings.Object, _httpClient.Object, _serializer.Object, _previousTime.Object);
		}

		[TestMethod]
		public void TestWork()
		{
			var data = _fixture.Create<Setting>();
			var oldData = _fixture.Create<byte[]>();
			data.Data = oldData;
			var settings = _fixture.Create<BillSettings>();
			settings.AutoUpdatePeriod = TimeSpan.FromDays(1);
			var bytes = Encoding.ASCII.GetBytes("EUR;2014-05-15;;;;47.6173;;;1");
			var newData = _fixture.Create<byte[]>();

			_settings.Setup(x => x.Get(SettingType.Bill)).Returns(data);
			_serializer.Setup(x => x.Deserialize<BillSettings>(oldData)).Returns(settings);
			_previousTime.Setup(x => x.Get()).Returns(_now.AddDays(-1));
			_httpClient.Setup(x => x.Get(settings.SourceUrl)).Returns(bytes);
			_serializer.Setup(x => x.Serialize(settings)).Returns(newData);
			_settings.Setup(x => x.AddOrReplace(data)).Returns(data);
			_previousTime.Setup(x => x.Set(_now));

			_job.Work();

			_settings.Verify(x => x.Get(SettingType.Bill));
			_serializer.Verify(x => x.Deserialize<BillSettings>(oldData));
			_previousTime.Verify(x => x.Get());
			_httpClient.Verify(x => x.Get(settings.SourceUrl));
			_serializer.Verify(x => x.Serialize(It.Is<BillSettings>(s => s.EuroToRuble == (decimal)47.6173)));
			_settings.Verify(x => x.AddOrReplace(It.Is<Setting>(s => s.Data == newData)));
			_previousTime.Verify(x => x.Set(_now));
		}
	}
}
