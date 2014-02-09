using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Client.ClientAdd
{
	internal sealed class FileHelperStub : IClientExcelHelper
	{
		public IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages)
		{
			return null;
		}
	}
}