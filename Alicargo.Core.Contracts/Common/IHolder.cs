namespace Alicargo.Core.Contracts.Common
{
	public interface IHolder<T>
	{
		T Get();
		void Set(T value);
	}
}