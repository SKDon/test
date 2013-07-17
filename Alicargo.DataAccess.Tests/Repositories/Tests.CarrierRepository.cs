using System.Linq;
using Alicargo.Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		private Carrier CreateTestCarrier()
		{
			var data = _fixture.Create<Carrier>();

			var id = _carrierRepository.Add(data);
			_unitOfWork.SaveChanges();

			data.Id = id();

			return data;
		}

		[TestMethod]
		public void Test_CarrierRepository_Add_Get()
		{
			var carrier = CreateTestCarrier();

			var actual = _carrierRepository.Get(carrier.Name);

			actual.ShouldBeEquivalentTo(carrier);

			var carriers = _carrierRepository.GetAll();

			Assert.IsTrue(carriers.Any(x => x.Id == carrier.Id));
		}
	}
}
