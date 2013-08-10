using System.Linq;
using Alicargo.Core.Contracts;
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
			var oldData = _AirWaybillRepository.GetAll();

			var data = CreateTestAirWaybill();

			var newData = _AirWaybillRepository.GetAll();

			Assert.AreEqual(oldData.Length + 1, newData.Length);

			var AirWaybill = _AirWaybillRepository.Get(data.Id).First();

			data.ShouldBeEquivalentTo( AirWaybill);
		}

		[TestMethod]
		public void Test_AirWaybillRepository_Count_GetRange()
		{
			var AirWaybillDatas = _AirWaybillRepository.GetAll();
			var count = _AirWaybillRepository.Count();

			Assert.AreEqual(AirWaybillDatas.Length, count);

			var range = _AirWaybillRepository.GetRange(0, (int)count);

			AirWaybillDatas.ShouldBeEquivalentTo(range);
		}

		private AirWaybillData CreateTestAirWaybill()
		{
			var brocker = _brockerRepository.GetAll().First();

			var model = _fixture
				.Build<AirWaybillData>()
				.With(x => x.StateId, DefaultStateId)
				.With(x => x.BrockerId, brocker.Id)
				.Create();

			var id = _AirWaybillRepository.Add(model, RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes());
			_unitOfWork.SaveChanges();
			model.Id = id();

			return model;
		}
	}
}
