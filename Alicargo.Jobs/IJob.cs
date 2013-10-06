namespace Alicargo.Jobs
{
	public interface IJob<in T>
	{
		void Run(T data);
	}
}
