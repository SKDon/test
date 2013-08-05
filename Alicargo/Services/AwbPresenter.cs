using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services
{
	public sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IReferenceRepository _referenceRepository;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateService _stateService;
		private readonly IIdentityService _identityService;

		public AwbPresenter(
			IReferenceRepository referenceRepository,
			IBrockerRepository brockerRepository,
			IStateService stateService,
			IIdentityService identityService)
		{
			_referenceRepository = referenceRepository;
			_brockerRepository = brockerRepository;
			_stateService = stateService;
			_identityService = identityService;
		}		

		public ListCollection<ReferenceModel> List(int take, int skip)
		{
			long? brockerId = null;
			if (_identityService.IsInRole(RoleType.Brocker) && _identityService.Id.HasValue)
			{
				var brocker = _brockerRepository.GetByUserId(_identityService.Id.Value);
				brockerId = brocker.Id;
			}
			var total = _referenceRepository.Count(brockerId);

			var data = _referenceRepository.GetRange(skip, take, brockerId)
					.Select(x => new ReferenceModel(x))
					.ToArray();

			SetAdditionalData(data);

			return new ListCollection<ReferenceModel> { Data = data, Total = total };
		}

		private void SetAdditionalData(params ReferenceModel[] data)
		{
			var aggregates = _referenceRepository.GetAggregate(data.Select(x => x.Id).ToArray())
				.ToDictionary(x => x.ReferenceId, x => x);
			var localizedStates = _stateService.GetLocalizedDictionary();

			foreach (var model in data)
			{
				var aggregate = aggregates[model.Id];

				model.TotalCount = aggregate.TotalCount;
				model.TotalWeight = aggregate.TotalWeight;

				model.State = new ApplicationStateModel
				{
					StateId = model.StateId,
					StateName = localizedStates[model.StateId]
				};
			}
		}

		// todo: test SetAdditionalData
		public ReferenceModel Get(long id)
		{
			var data = _referenceRepository.Get(id).FirstOrDefault();

			if (data == null) throw new EntityNotFoundException("Refarence: " + id);

			var model = new ReferenceModel(data);

			SetAdditionalData(model);

			return model;
		}		
	}
}
