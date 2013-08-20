using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Tests.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
    [TestClass]
    public class AwbRepositoryTests
    {
        private IAwbRepository _awbRepository;
        private DbTestContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new DbTestContext();

            _awbRepository = new AwbRepository(_context.UnitOfWork);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Cleanup();
        }

        [TestMethod]
        public void Test_AirWaybillRepository_GetAll_Add_Get()
        {
            var oldData = _awbRepository.GetAll();

            var data = CreateTestAirWaybill();

            var newData = _awbRepository.GetAll();

            Assert.AreEqual(oldData.Length + 1, newData.Length);

            var airWaybill = _awbRepository.Get(data.Id).First();

            data.ShouldBeEquivalentTo(airWaybill);
        }

        [TestMethod]
        public void Test_AirWaybillRepository_Count_GetRange()
        {
            var airWaybillDatas = _awbRepository.GetAll();
            var count = _awbRepository.Count();

            Assert.AreEqual(airWaybillDatas.Length, count);

            var range = _awbRepository.GetRange(0, (int)count);

            airWaybillDatas.ShouldBeEquivalentTo(range);
        }

        private AirWaybillData CreateTestAirWaybill()
        {
            var brockerRepository = new BrockerRepository(_context.UnitOfWork);
            var brocker = brockerRepository.GetAll().First();

            var model = _context.Fixture
                                .Build<AirWaybillData>()
                                .With(x => x.StateId, DbTestContext.DefaultStateId)
                                .With(x => x.BrockerId, brocker.Id)
                                .Create();

            var id = _awbRepository.Add(model, _context.RandomBytes(), _context.RandomBytes(),
                                        _context.RandomBytes(), _context.RandomBytes(),
                                        _context.RandomBytes());
            _context.UnitOfWork.SaveChanges();
            model.Id = id();

            return model;
        }

        [TestMethod]
        public void Test_AwbRepository_GetAggregate()
        {
            var id1 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
            var id2 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            var applications = new ApplicationUpdateRepository(_context.UnitOfWork);
            var data1 = CreateApplicationData();
            var data2 = CreateApplicationData();
            var data3 = CreateApplicationData();
            var data4 = CreateApplicationData();
            var a1 = applications.Add(data1, null, null, null, null, null, null);
            var a2 = applications.Add(data2, null, null, null, null, null, null);
            var a3 = applications.Add(data3, null, null, null, null, null, null);
            var a4 = applications.Add(data4, null, null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            applications.SetAirWaybill(a1(), id1());
            applications.SetAirWaybill(a2(), id1());
            applications.SetAirWaybill(a3(), id2());
            applications.SetAirWaybill(a4(), id2());
            _context.UnitOfWork.SaveChanges();

            var aggregates = _awbRepository.GetAggregate(id1(), id2());

            aggregates.Count().ShouldBeEquivalentTo(2);

            var aggregate1 = aggregates.First(x => x.AirWaybillId == id1());
            var aggregate2 = aggregates.First(x => x.AirWaybillId == id2());

            aggregate1.TotalCount.ShouldBeEquivalentTo(data1.Count + data2.Count);
            aggregate2.TotalCount.ShouldBeEquivalentTo(data3.Count + data4.Count);

            aggregate1.TotalWeight.ShouldBeEquivalentTo(data1.Weigth + data2.Weigth);
            aggregate2.TotalWeight.ShouldBeEquivalentTo(data3.Weigth + data4.Weigth);
        }

        private ApplicationData CreateApplicationData()
        {
            return _context.Fixture
                           .Build<ApplicationData>()
                           .With(x => x.ClientId, DbTestContext.TestClientId)
                           .With(x => x.AirWaybillId, null)
                           .With(x => x.CountryId, null)
                           .With(x => x.StateId, DbTestContext.DefaultStateId)
                           .With(x => x.TransitId, 1)
                           .With(x => x.CurrencyId, (int)CurrencyType.Dollar)
                           .Create();
        }

        private AirWaybillData CreateAirWaybillData()
        {
            return _context.Fixture
                           .Build<AirWaybillData>()
                           .With(x => x.StateId, DbTestContext.DefaultStateId)
                           .With(x => x.BrockerId, DbTestContext.TestBrockerId)
                           .Create();
        }
    }
}