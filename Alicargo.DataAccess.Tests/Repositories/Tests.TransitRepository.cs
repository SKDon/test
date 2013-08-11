using System.Linq;
using Alicargo.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		[TestMethod]
		public void Test_TransitRepository_Add_Get()
		{
			var transit = new TransitData(CreateTestTransit());

			var actual = _transitRepository.Get(transit.Id).First();

			transit.ShouldBeEquivalentTo(actual);
		}

		[TestMethod]
		public void Test_TransitRepository_Update()
		{
			var oldData =  new TransitData(CreateTestTransit());

			var newData = _fixture.Create<TransitData>();
			newData.CarrierId = oldData.CarrierId;
			newData.Id = oldData.Id;
			_transitRepository.Update(newData);
			_unitOfWork.SaveChanges();
			var actual = _transitRepository.Get(newData.Id).First();

			oldData.Should().NotBeSameAs(actual); // todo: test
			newData.ShouldBeEquivalentTo(actual);
		}

		Transit CreateTestTransit()
		{
			var transit = _fixture.Create<TransitData>();
			var carrier = CreateTestCarrier();
			transit.CarrierId = carrier.Id;

			var id = _transitRepository.Add(transit);
			_unitOfWork.SaveChanges();
			transit.Id = id();

			return new Transit(transit, carrier.Name);
		}
	}
}
