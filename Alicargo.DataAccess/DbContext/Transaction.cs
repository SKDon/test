using System.Transactions;
using Alicargo.Core.Repositories;

namespace Alicargo.DataAccess.DbContext
{
	sealed class Transaction : ITransaction
	{
		private readonly TransactionScope _scope;

		public Transaction(TransactionScope scope)
		{
			_scope = scope;
		}

		public void Dispose()
		{
			_scope.Dispose();
		}

		public void Complete()
		{
			_scope.Complete();
		}
	}
}