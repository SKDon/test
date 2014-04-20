using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class SettingRepository : ISettingRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public SettingRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public Setting AddOrReplace(Setting setting)
		{
			try
			{
				var rowVersion = _executor.Query<byte[]>("[dbo].[Setting_Merge]", setting);

				return new Setting
				{
					Data = setting.Data,
					RowVersion = rowVersion,
					Type = setting.Type
				};
			}
			catch(DublicateException e)
			{
				throw new UpdateConflictException("Failed to merge setting " + setting.Type, e);
			}
		}

		public Setting Get(SettingType type)
		{
			return _executor.Query<Setting>("[dbo].[Setting_Get]", new { type });
		}
	}
}