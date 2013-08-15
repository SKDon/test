using System.Linq;
using Alicargo.Contracts.Contracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		[TestMethod]
		public void Test_AirWaybillRepository_GetAll_Add_Get()
		{
			var oldData = _airWaybillRepository.GetAll();

			var data = CreateTestAirWaybill();

			var newData = _airWaybillRepository.GetAll();

			Assert.AreEqual(oldData.Length + 1, newData.Length);

			var airWaybill = _airWaybillRepository.Get(data.Id).First();

			data.ShouldBeEquivalentTo( airWaybill);
		}

		[TestMethod]
		public void Test_AirWaybillRepository_Count_GetRange()
		{
			var airWaybillDatas = _airWaybillRepository.GetAll();
			var count = _airWaybillRepository.Count();

			Assert.AreEqual(airWaybillDatas.Length, count);

			var range = _airWaybillRepository.GetRange(0, (int)count);

			airWaybillDatas.ShouldBeEquivalentTo(range);
		}

		private AirWaybillData CreateTestAirWaybill()
		{
			var brocker = _brockerRepository.GetAll().First();

			var model = _fixture
				.Build<AirWaybillData>()
				.With(x => x.StateId, DefaultStateId)
				.With(x => x.BrockerId, brocker.Id)
				.Create();

			var id = _airWaybillRepository.Add(model, RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes());
			_unitOfWork.SaveChanges();
			model.Id = id();

			return model;
		}
	}
}
