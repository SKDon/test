namespace Alicargo.Jobs.Helpers.Abstract
{
	internal interface ITextBuilder<in T>
	{
		string GetText(string template, string language, T data);
	}
}