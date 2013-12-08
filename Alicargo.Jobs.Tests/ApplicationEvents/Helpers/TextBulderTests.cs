using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Services;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Jobs.ApplicationEvents.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.ApplicationEvents.Helpers
{
	[TestClass]
	public class TextBulderTests
	{
		private TextBulder _bulder;
		private ApplicationDetailsData _detailsData;
		private ApplicationSetStateEventData _eventData;
		private Mock<IApplicationFileRepository> _files;
		private Fixture _fixture;
		private Serializer _serializer;
		private Mock<IStateRepository> _states;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_states = new Mock<IStateRepository>(MockBehavior.Strict);
			_files = new Mock<IApplicationFileRepository>(MockBehavior.Strict);
			_serializer = new Serializer();
			_bulder = new TextBulder(_serializer, _states.Object, _files.Object);

			_eventData = new ApplicationSetStateEventData
			{
				StateId = TestConstants.CargoIsFlewStateId,
				Timestamp = _fixture.Create<DateTimeOffset>()
			};
			_detailsData = _fixture.Create<ApplicationDetailsData>();
			_detailsData.StateId = TestConstants.DefaultStateId;
			_detailsData.CurrencyId = (int) CurrencyName.Names.First().Key;
			_detailsData.CountryName = new[]
			{new KeyValuePair<string, string>(TwoLetterISOLanguageName.Russian, _fixture.Create<string>())};
		}

		[TestMethod]
		public void Test_GetText()
		{
			var properties = typeof (TextLocalizedData).GetProperties().Where(x => x.PropertyType == typeof (string)).ToArray();
			var sb = new StringBuilder();
			foreach (var property in properties)
			{
				sb.AppendFormat("{{{0} [{0}: {{0}}]}}", property.Name).AppendLine();
			}
			sb.Append("{DisplayNumber[Number: \"{0}\"]}").AppendLine();
			sb.Append("{DisplayNumber}").AppendLine();

			_states.Setup(x => x.Get(TwoLetterISOLanguageName.Russian, _eventData.StateId))
				.Returns(_fixture.Create<Dictionary<long, StateData>>());
			_states.Setup(x => x.Get(TwoLetterISOLanguageName.Russian, _detailsData.StateId))
				.Returns(_fixture.Create<Dictionary<long, StateData>>());
			_files.Setup(x => x.GetNames(_detailsData.Id, It.IsAny<ApplicationFileType>()))
				.Returns(_fixture.Create<Dictionary<long, string>>());

			var text = _bulder.GetText(sb.ToString(), TwoLetterISOLanguageName.Russian, ApplicationEventType.SetState,
				_detailsData, _serializer.Serialize(_eventData));

			text.Should().NotContain("{").And.NotContain("}");
		}
	}
}