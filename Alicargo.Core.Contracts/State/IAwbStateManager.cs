namespace Alicargo.Core.Contracts.State
{
	public interface IAwbStateManager
	{
		void SetState(long airWaybillId, long stateId);
	}
}
