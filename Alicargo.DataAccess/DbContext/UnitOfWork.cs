using System;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Transactions;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;

namespace Alicargo.DataAccess.DbContext
{
	internal sealed class UnitOfWork : IUnitOfWork, IDisposable
	{
		public DataContext Context { get; private set; }

		public ITransaction StartTransaction()
		{
			// todo: refactor using of transactions
			// TransactionScopeOption.RequiresNew
			return new Transaction(new TransactionScope());
		}

		public UnitOfWork(IDbConnection connection)
		{
			Context = new AlicargoDataContext(connection);
			Debug();
		}

		[Conditional("DEBUG")]
		private void Debug()
		{
			Context.Log = new DebugTextWriter();
		}

		public void SaveChanges()
		{
			try
			{
				Context.SubmitChanges(ConflictMode.FailOnFirstConflict);
			}
			catch (System.Data.SqlClient.SqlException exception)
			{
				if (exception.Number == (int)SqlError.CannotInsertDuplicateKeyRow)
				{
					throw new DublicateException("Failed to add dublicate entity", exception);
				}

				throw;
			}
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}
}