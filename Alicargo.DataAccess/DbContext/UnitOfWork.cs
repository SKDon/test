using System;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Transactions;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.DbContext
{
	internal sealed class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly DataContext _context;

		public object Context
		{
			get { return _context; }
		}

		public ITransaction StartTransaction()
		{
			// todo: 3.5. refactor using of transactions
			// TransactionScopeOption.RequiresNew
			return new Transaction(new TransactionScope());
		}

		public UnitOfWork(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			//Debug();
		}

		[Conditional("DEBUG")]
		private void Debug()
		{
			_context.Log = new DebugTextWriter();
		}

		public void SaveChanges()
		{
			try
			{
				_context.SubmitChanges(ConflictMode.FailOnFirstConflict);
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
			_context.Dispose();
		}
	}
}