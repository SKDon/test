using System;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ITransaction : IDisposable
	{
		void Complete();
	}
}