namespace Alicargo.Core.Contracts.Excel
{
	public interface IDrawable
	{
		int Draw(int row);

		long Position { get; }
	}
}