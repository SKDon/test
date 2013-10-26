using System;
using System.Data.SqlClient;
using Alicargo.Contracts.Exceptions;

namespace Alicargo.DataAccess.DbContext
{
	internal static class SqlExceptionsHelper
	{
		public static void Action(Action action)
		{
			Action(() => action);
		}

		public static T Action<T>(Func<T> action)
		{
			try
			{
				return action();
			}
			catch (SqlException exception)
			{
				if (exception.Number == (int)SqlError.CannotInsertDuplicateKeyRow)
				{
					if (exception.Message.Contains("IX_User_Login"))
					{
						throw new DublicateLoginException("The login is occupied", exception);
					}

					throw new DublicateException("Failed to add dublicate entity", exception);
				}

				throw;
			}
		}
	}
}