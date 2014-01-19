namespace Alicargo.Core.Contracts.AirWaybill
{
	public interface IAwbStateManager
	{
		void SetState(long airWaybillId, long stateId);
	}
}
