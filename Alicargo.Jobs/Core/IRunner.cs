using System.Threading;

namespace Alicargo.Jobs.Core
{
	public interface IRunner
	{
		void Run(CancellationTokenSource tokenSource);
		string Name { get; }
	}
}