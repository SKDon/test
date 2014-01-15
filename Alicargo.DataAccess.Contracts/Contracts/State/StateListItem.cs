namespace Alicargo.DataAccess.Contracts.Contracts.State
{
	public sealed class StateListItem
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public bool IsSystem { get; set; }

		public int Position { get; set; }
	}
}