using System.Data;

namespace Alicargo.Contracts.Repositories
{
	public interface ISqlProcedureExecutor
	{
		T Get<T>(string sql, object param = null, IDbTransaction transaction = null);
	}
}