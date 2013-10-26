using System;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.DbContext
{
	public sealed class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly DataContext _context;

		public object Context
		{
			get { return _context; }
		}

		public UnitOfWork(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			Debug();
		}

		[Conditional("TRACE_SQL")]
		private void Debug()
		{
			_context.Log = new DebugTextWriter();
		}

		public void SaveChanges()
		{
			SqlExceptionsHelper.Action(() => _context.SubmitChanges(ConflictMode.FailOnFirstConflict));			
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}