﻿using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface IClientExcelHelper
	{
		IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages);
	}
}