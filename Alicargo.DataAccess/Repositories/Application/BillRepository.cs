using System;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.DataAccess.Repositories.Application
{
	internal sealed class BillRepository : IBillRepository
	{
		public void AddOrReplace(BillEditData data)
		{
		}

		public BillEditData Get(long applicationId)
		{
			return null;
		}
	}
}