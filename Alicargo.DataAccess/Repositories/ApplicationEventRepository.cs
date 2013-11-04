using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ApplicationEventRepository : IApplicationEventRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ApplicationEventRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(long applicationId, ApplicationEventType eventType)
		{
			try
			{
				_executor.Execute("[dbo].[ApplicationEvent_Add]", new { applicationId, eventType });
			}
			catch (DublicateException ex)
			{
				if (ex.InnerException == null || !ex.InnerException.Message.Contains("IX_ApplicationEvent_ApplicationId_EventType"))
				{
					throw;
				}
			}
		}

		public ApplicationEventData GetNext(DateTimeOffset olderThan)
		{
			return _executor.Query<ApplicationEventData>("[dbo].[ApplicationEvent_GetNext]", new { olderThan });
		}

		public byte[] Touch(long id, byte[] rowVersion)
		{
			var bytes = _executor.Query<byte[]>("[dbo].[ApplicationEvent_Touch]", new { id, rowVersion });

			if (bytes == null || bytes.Length == 0)
			{
				throw new EntityUpdateConflict("Failed to touch application event " + id);
			}

			return bytes;
		}

		public void Delete(long id, byte[] rowVersion)
		{
			_executor.Execute("[dbo].[ApplicationEvent_Delete]", new { id, rowVersion });
		}
	}
}