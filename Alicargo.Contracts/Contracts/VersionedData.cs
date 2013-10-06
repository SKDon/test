namespace Alicargo.Contracts.Contracts
{
	public sealed class VersionedData<TState, TData> where TState : struct
	{
		public VersionData<TState> Version { get; set; }

		public TData Data { get; set; }
	}
}
