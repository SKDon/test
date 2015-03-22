namespace Alicargo.DataAccess.Contracts.Contracts.State
{
	public sealed class StateData
	{
		public string LocalizedName { get; set; }

		public string Name { get; set; }

		public int Position { get; set; }

		public bool IsSystem { get; set; }
	}
}