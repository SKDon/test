using System;
using System.Data.Linq;
using System.Data.SqlClient;
using Alicargo.DataAccess.Contracts.Exceptions;

namespace Alicargo.DataAccess.DbContext
{
	internal static class SqlExceptionsHelper
	{
		public static void Action(Action action)
		{
			Action(() =>
			{
				action();
				return (object)null;
			});
		}

		public static T Action<T>(Func<T> action)
		{
			try
			{
				return action();
			}
			catch (SqlException exception)
			{
				switch ((SqlError)exception.Number)
				{
					case SqlError.CannotInsertDuplicateKeyRow:
						if (exception.Message.Contains("IX_User_Login"))
						{
							throw new DublicateLoginException("The login is occupied", exception);
						}

						throw new DublicateException("Failed to add dublicate entity", exception);

					case SqlError.DeleteStatementConflictedWihtConstraint:
						throw new DeleteConflictedWithConstraintException("Can't delete an entity", exception);
				}

				throw;
			}
		}

		[Obsolete]
		public static void SaveChanges(this AlicargoDataContext context)
		{
			Action(() => context.SubmitChanges(ConflictMode.FailOnFirstConflict));			
		}
	}
}