using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Client;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class SenderCalculationPresenter : ISenderCalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public SenderCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_clientRepository = clientRepository;
		}

		public ClientCalculationListCollection List(long clientId, int take, long skip)
		{
			long total;
			var applications = GetCalculatedApplicationsWithAwb(clientId, take, skip, out total);

			// ReSharper disable PossibleInvalidOperationException
			var awbsData = _awbRepository.Get(applications.Select(x => x.AirWaybillId.Value).ToArray())
				// ReSharper restore PossibleInvalidOperationException
										 .OrderByDescending(x => x.DateOfArrival)
										 .ToArray();


			var items = GetItems(awbsData, applications);

			var groups = GetGroups(awbsData, items);

			return new ClientCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total
			};
		}

		private ApplicationListItemData[] GetCalculatedApplicationsWithAwb(long clientId, int take, long skip, out long total)
		{
			var applications = _applicationRepository.List(clientId: clientId)
													 .OrderBy(x => x.AirWaybillId)
													 .ThenByDescending(x => x.Id)
													 .ToArray();
			var appIds = applications.Select(x => x.Id).ToArray();
			var calculations = _applicationRepository.GetCalculations(appIds);
			applications = applications.Where(x => calculations.ContainsKey(x.Id) && x.AirWaybillId.HasValue).ToArray();
			total = applications.LongLength;

			return applications.Skip((int)skip).Take(take).ToArray();
		}

		private static List<ClientCalculationGroup> GetGroups(IEnumerable<AirWaybillData> awbsData,
			IEnumerable<ClientCalculationItem> items)
		{
			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				return new ClientCalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = AwbHelper.GetAirWaybillDisplay(awbsData.First(x => x.Id == g.Key)),
					aggregates = new ClientCalculationGroup.Aggregates(g.Sum(x => x.Profit))
				};
			}).ToList();

			return groups;
		}

		private IEnumerable<ClientCalculationItem> GetItems(IEnumerable<AirWaybillData> awbsData,
			ApplicationListItemData[] applications)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var nics = _clientRepository.GetNicByApplications(appIds);
			var ranks = awbsData.Select((pair, i) => new {pair.Id, Rank = i}).ToDictionary(x => x.Id, x => x.Rank);

			return applications.Select(a => new ClientCalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCost,
				TariffPerKg = a.TariffPerKg,
				TransitCost = a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weigth),
				Profit = CalculationHelper.GetProfit(a),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				ClassName = a.ClassId.HasValue
					? ((ClassType) a.ClassId.Value).ToLocalString()
					: ""
			}).OrderBy(x => ranks[x.AirWaybillId]).ToArray();
		}
	}
}