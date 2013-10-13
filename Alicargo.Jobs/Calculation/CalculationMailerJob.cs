using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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
			_calculations.Process(CalculationState.ClientUpdated, CalculationState.Emailing, RunImpl);
		}

		private void RunImpl(VersionedData<CalculationState, CalculationData> item)
		{
			try
			{
				_mailer.Send(item.Data);

				_calculations.SetState(item, CalculationState.Done);
			}
			catch (Exception e)
			{
				_log.Error("Failed to send an email for the calculation " + item.Version.Id, e);

				if (e.IsCritical())
				{
					throw;
				}

				_calculations.SetState(item, CalculationState.Error);
			}
		}
	}
}