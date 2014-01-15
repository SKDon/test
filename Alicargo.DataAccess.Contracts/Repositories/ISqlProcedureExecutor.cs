namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ISqlProcedureExecutor
	{
		T Query<T>(string sql, object param = null);
		T[] Array<T>(string sql, object param = null);
		int Execute(string sql, object param = null);
	}
}