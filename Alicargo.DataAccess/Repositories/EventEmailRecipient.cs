using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class EventEmailRecipient : IEventEmailRecipient
	{
		private readonly ISqlProcedureExecutor _executor;

		public EventEmailRecipient(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public RoleType[] GetRecipientRoles(EventType eventType)
		{
			return _executor.Array<RoleType>("[dbo].[EventEmailRecipient_Get]", new { EventTypeId = (int)eventType });
		}

		public void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization)
		{
			var table = TableParameters.GeIdsTable("Recipients", recipients.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[EmailTemplate_MergeEvent]", new
			{
				EventTypeId = eventType,
				localization.Body,
				localization.IsBodyHtml,
				localization.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend
			});

			_executor.Execute("[dbo].[EventEmailRecipient_Set]", new TableParameters(new { EventTypeId = eventType }, table));
		}
	}
}