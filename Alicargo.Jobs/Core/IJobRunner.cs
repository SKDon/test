using System.Threading;

namespace Alicargo.Jobs.Core
{
	public interface IJobRunner
	{
		void Run(CancellationTokenSource tokenSource);
		string Name { get; }
	}
}