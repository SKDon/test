using System;

namespace Alicargo.Contracts.Repositories
{
	public interface ITransaction : IDisposable
	{
		void Complete();
	}
}