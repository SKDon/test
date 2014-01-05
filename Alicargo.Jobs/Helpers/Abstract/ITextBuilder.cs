using System.Collections.Generic;

namespace Alicargo.Jobs.Helpers.Abstract
{
	internal interface ITextBuilder
	{
		string GetText(string template, string language, IDictionary<string, string> data);
	}
}