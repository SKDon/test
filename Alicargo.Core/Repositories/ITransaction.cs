using System;

namespace Alicargo.Core.Repositories
{
	public interface ITransaction : IDisposable
	{
		void Complete();
	}
}