namespace Alicargo.Jobs.Bill.Helpers
{
	public interface ICourseSource
	{
		decimal GetEuroToRuble(string url);
	}
}