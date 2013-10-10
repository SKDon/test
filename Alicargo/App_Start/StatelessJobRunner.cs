﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Alicargo.Core.Services;
using Alicargo.Jobs;

namespace Alicargo.App_Start
{
	internal sealed class StatelessJobRunner : IJobRunner
	{
		private readonly string _connectionString;
		private readonly Func<IDbConnection, IJob> _getJob;
		private readonly ILog _log;
		private readonly TimeSpan _pausePeriod;

		public StatelessJobRunner(
			Func<IDbConnection, IJob> getJob,
			ILog log,
			string connectionString,
			TimeSpan pausePeriod)
		{
			_getJob = getJob;
			_log = log;
			_connectionString = connectionString;
			_pausePeriod = pausePeriod;
		}

		public void Run(CancellationTokenSource tokenSource)
		{
			while (!tokenSource.IsCancellationRequested)
			{
				try
				{
					using (var connection = new SqlConnection(_connectionString))
					{
						var job = _getJob(connection);

						job.Run();
					}
				}
				catch (Exception e)
				{
					_log.Error("An error occurred during a job running", e);

					if (e.IsCritical())
					{
						tokenSource.Cancel(false);
					}
				}

				tokenSource.Token.WaitHandle.WaitOne(_pausePeriod);
			}
		}
	}
}