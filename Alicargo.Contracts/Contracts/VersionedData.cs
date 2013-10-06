using System;

namespace Alicargo.Contracts.Contracts
{
	public sealed class VersionedData<TState, TData> where TState : struct
	{
		public long Id { get; set; }
		public byte[] RowVersion { get; set; }
		public TState State { get; set; }
		public DateTimeOffset StateTimestamp { get; set; }

		public TData Data { get; set; }
	}
}
