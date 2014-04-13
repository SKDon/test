using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Application;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Application
{
	[TestClass]
	public class ApplicationListItemMapperTests
	{
		private const int CargoOnTransitStateId = 0;
		private Dictionary<long, long> _calculations;
		private List<CityData> _cities;
		private MockContainer _context;
		private List<CountryData> _countries;
		private ApplicationExtendedData[] _data;
		private Dictionary<long, string> _localazedStates;
		private ApplicationListItemMapper _mapper;
		private long[] _stateAvailability;

		[TestInitialize]
		public void TestInitialize()
		{
			const int count = 20;
			_context = new MockContainer();
			_countries = new List<CountryData>();
			_cities = new List<CityData>();
			_localazedStates = new Dictionary<long, string>();
			_stateAvailability = Enumerable.Range(0, count / 2).Select(x => (long)x).ToArray();
			_data = _context.CreateMany<ApplicationExtendedData>(count).ToArray();
			_calculations = _data.Take(count / 2).ToDictionary(x => x.Id, x => _context.Create<long>());

			for(var i = 0; i < count; i++)
			{
				var country = new CountryData
				{
					Id = i,
					Name = _context.Create<string>(),
					Position = i
				};
				_countries.Add(country);
				_cities.Add(new CityData
				{
					Id = i,
					Name = _context.Create<string>(),
					Position = i
				});
				_localazedStates.Add(i, _context.Create<string>());

				_data[i].StateId = i;
				_data[i].CountryId = i / 2;
				_data[i].TransitCityId = i / 3;
			}

			_context.ApplicationFileRepository.Setup(x => x.GetInfo(It.IsAny<long[]>(), It.IsAny<ApplicationFileType>()))
				.Returns(new Dictionary<long, FileInfo[]>(0));

			_context.IdentityService.SetupGet(x => x.Language).Returns(TwoLetterISOLanguageName.English);
			_context.CountryRepository.Setup(x => x.All(TwoLetterISOLanguageName.English)).Returns(_countries.ToArray());
			_context.CityRepository.Setup(x => x.All(TwoLetterISOLanguageName.English)).Returns(_cities.ToArray());
			_context.StateService.Setup(x => x.GetStateAvailabilityToSet()).Returns(_stateAvailability);
			_context.StateConfig.SetupGet(x => x.CargoOnTransitStateId).Returns(CargoOnTransitStateId);
			_context.ApplicationRepository.Setup(x => x.GetCalculations(It.IsAny<long[]>())).Returns(_calculations);
			_context.StateRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<long[]>())).Returns(
				(string lang, long[] ids) => ids.ToDictionary(
					x => x,
					x =>
						new StateData
						{
							Name = "State " + x,
							Position = (int)x,
							LocalizedName = _localazedStates[x]
						}
					));

			_mapper = _context.Create<ApplicationListItemMapper>();
		}

		[TestMethod]
		public void Test_ApplicationListItemMapper_Map()
		{
			var items = _mapper.Map(_data, TwoLetterISOLanguageName.English);

			_data.ShouldAllBeEquivalentTo(items, options => options
				.ExcludingMissingProperties()
				.Excluding(x => x.DisplayNumber)
				.Excluding(x => x.FactureCost)
				.Excluding(x => x.FactureCostEx)
				.Excluding(x => x.PickupCost)
				.Excluding(x => x.TransitCost));

			for(var i = 0; i < _data.Length; i++)
			{
				var item = items[i];
				var data = _data[i];
				var country = _countries.First(x => x.Id == data.CountryId).Name;
				var city = _cities.First(x => x.Id == data.TransitCityId).Name;

				item.CountryName.ShouldBeEquivalentTo(country);
				item.State.ShouldBeEquivalentTo(new ApplicationStateModel
				{
					StateId = i,
					StateName = _localazedStates[i]
				});
				item.DisplayNumber.ShouldBeEquivalentTo(data.GetApplicationDisplay());
				item.FactureCost.ShouldBeEquivalentTo(data.GetAdjustedFactureCost());
				item.FactureCostEx.ShouldBeEquivalentTo(data.GetAdjustedFactureCostEx());
				item.PickupCost.ShouldBeEquivalentTo(data.GetAdjustedPickupCost());
				item.TransitCost.ShouldBeEquivalentTo(data.GetAdjustedTransitCost());
				item.TransitCity.ShouldBeEquivalentTo(city);
				item.CanClose.ShouldBeEquivalentTo(item.StateId == CargoOnTransitStateId);
				item.CanSetState.ShouldBeEquivalentTo(_stateAvailability.Contains(item.StateId));
				item.TransitDeliveryTypeString.Should().NotBeNullOrEmpty();
				item.TransitMethodOfTransitString.Should().NotBeNullOrEmpty();
				item.CanSetTransitCost.ShouldBeEquivalentTo(!_calculations.ContainsKey(item.Id));
				item.CPFiles.Should().BeNull();
				item.Torg12Files.Should().BeNull();
				item.DeliveryBillFiles.Should().BeNull();
				item.PackingFiles.Should().BeNull();
				item.SwiftFiles.Should().BeNull();
				item.InvoiceFiles.Should().BeNull();
			}
		}
	}
}