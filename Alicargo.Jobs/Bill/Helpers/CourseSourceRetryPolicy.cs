using System;
using System.Threading.Tasks;
using Alicargo.Core.Contracts.Common;

namespace Alicargo.Jobs.Bill.Helpers
{
	public sealed class CourseSourceRetryPolicy : ICourseSource
	{
		private readonly ushort _attempts;
		private readonly ICourseSource _courseSource;
		private readonly ILog _log;
		private readonly TimeSpan _waitPeriod;

		public CourseSourceRetryPolicy(ICourseSource courseSource, ushort attempts, ILog log, TimeSpan waitPeriod)
		{
			_courseSource = courseSource;
			_attempts = attempts;
			_log = log;
			_waitPeriod = waitPeriod;
		}

		public async Task<decimal> GetEuroToRuble(string url)
		{
			ushort counter = 0;
			do
			{
				counter++;
				try
				{
					return await _courseSource.GetEuroToRuble(url);
				}
				catch(Exception e)
				{
					if(counter < _attempts)
					{
						_log.Warning("Attempt " + counter + " to get course of euro to ruble is failed." + Environment.NewLine + e);
					}
					else
					{
						throw;
					}
				}

				await Task.Delay(_waitPeriod);
			} while(true);
		}
	}
}