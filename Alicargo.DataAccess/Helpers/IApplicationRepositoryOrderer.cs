using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Helpers
{
	internal interface IApplicationRepositoryOrderer
	{
		IQueryable<Application> Order(IQueryable<Application> applications, IList<Order> orders);
	}
}