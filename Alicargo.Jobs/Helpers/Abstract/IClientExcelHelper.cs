using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface IClientExcelHelper
	{
		IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages);
	}
}