using System.Data;

namespace Alicargo.Contracts.Repositories
{
	public interface ISqlProcedureExecutor
	{
		T Query<T>(string sql, object param = null, IDbTransaction transaction = null);
		void Execute(string sql, object param = null, IDbTransaction transaction = null);
	}
}