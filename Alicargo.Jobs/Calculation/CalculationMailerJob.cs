using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationMailerJob : IJob
	{
		private readonly ICalculationRepository _calculations;
		private readonly ICalculationMailer _mailer;
		private readonly ILog _log;

		public CalculationMailerJob(ICalculationRepository calculations, ICalculationMailer mailer, ILog log)
		{
			_calculations = calculations;
			_mailer = mailer;
			_log = log;
		}

		public void Run()
		{
			var data = _calculations.Get(CalculationState.New);
			foreach (var item in data)
			{
				if (SetState(item, CalculationState.New))
				{
					try
					{
						_mailer.Send(item.Data);

						SetState(item, CalculationState.Sended);
					}
					catch (Exception e)
					{
						_log.Error("Failed to send an email for the calculation " + item.Version.Id, e);

						if (e.IsCritical())
						{
							throw;
						}

						SetState(item, CalculationState.Error);
					}
				}
			}
		}

		private bool SetState(VersionedData<CalculationState, CalculationData> item, CalculationState state)
		{
			try
			{
				var data = _calculations.SetState(item.Version.Id, item.Version.RowVersion, state);

				item.Version.RowVersion = data.RowVersion;
				item.Version.State = data.State;
				item.Version.StateTimestamp = data.StateTimestamp;
			}
			catch (EntityUpdateConflict)
			{
				return false;
			}

			return true;
		}
	}
}