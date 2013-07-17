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
		public void Test_ReferenceRepository_GetAll_Add_Get()
		{
			var oldData = _referenceRepository.GetAll();

			var data = CreateTestReference();

			var newData = _referenceRepository.GetAll();

			Assert.AreEqual(oldData.Length + 1, newData.Length);

			var reference = _referenceRepository.Get(data.Id).First();

			data.ShouldBeEquivalentTo( reference);
		}

		[TestMethod]
		public void Test_ReferenceRepository_Count_GetRange()
		{
			var referenceDatas = _referenceRepository.GetAll();
			var count = _referenceRepository.Count();

			Assert.AreEqual(referenceDatas.Length, count);

			var range = _referenceRepository.GetRange(0, (int)count);

			referenceDatas.ShouldBeEquivalentTo(range);
		}

		private ReferenceData CreateTestReference()
		{
			var brocker = _brockerRepository.GetAll().First();

			var model = _fixture
				.Build<ReferenceData>()
				.With(x => x.StateId, DefaultStateId)
				.With(x => x.BrockerId, brocker.Id)
				.Create();

			var id = _referenceRepository.Add(model, RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes(), RandomBytes());
			_unitOfWork.SaveChanges();
			model.Id = id();

			return model;
		}
	}
}
