namespace Alicargo.Services.Abstract
{
	public interface IAwbStateManager
	{
		void SetState(long airWaybillId, long stateId);
	}
}
