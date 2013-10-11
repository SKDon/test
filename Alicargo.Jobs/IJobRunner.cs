using System.Threading;

namespace Alicargo.Jobs
{
	public interface IJobRunner
	{
		void Run(CancellationTokenSource tokenSource);
		string Name { get; }
	}
}