using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Helpers
{
	[TestClass]
	public class ApplicationRepositoryOrdererTests
	{
		private IApplicationRepositoryOrderer _orderer;

		[TestInitialize]
		public void TestInitialize()
		{
			_orderer = new ApplicationRepositoryOrderer();
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void Test_KeyNotFoundException()
		{
			var list = new[] { GetApplication(1, "1", 1, "1") }.AsQueryable();

			var orders = new[] { new Order { OrderType = (OrderType)(-1) } };

			_orderer.Order(list, orders);
		}

		[TestMethod]
		public void Test_Order()
		{
			var list = new[]
			{
				GetApplication(1, "1", 1, "1"),
				GetApplication(2, "1", 3, "1"),
				GetApplication(3, "1", 5, "0"),
				GetApplication(4, "1", 4, "0"),
				GetApplication(5, "0", 2, "0")
			}.AsQueryable();

			var expected = list
				.OrderByDescending(x => x.State.Name)
				.ThenByDescending(x => x.Id)
				.ThenBy(x => x.AirWaybill.Bill).ToArray();

			var orders = new[]
			{
				new Order
				{
					OrderType = OrderType.State,
					Desc = true
				},
				new Order
				{
					OrderType = OrderType.Id,
					Desc = true
				},
				new Order
				{
					OrderType = OrderType.AirWaybill,
					Desc = false
				}
			};

			var actual = _orderer.Order(list, orders).ToArray();

			for(var i = 0; i < orders.Length; i++)
			{
				Assert.AreSame(expected[i], actual[i]);
			}
		}

		private static Application GetApplication(long id, string bill, long stateId, string legalEntity)
		{
			return new Application
			{
				Id = id,
				AirWaybill = new AirWaybill
				{
					Bill = bill
				},
				State = new State
				{
					Id = stateId,
					Name = stateId.ToString(CultureInfo.InvariantCulture)
				},
				Client = new Client
				{
					LegalEntity = legalEntity
				}
			};
		}
	}
}