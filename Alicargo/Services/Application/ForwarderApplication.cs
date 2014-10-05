using System.Linq;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ForwarderApplication : IForwarderApplication
	{
		private readonly ICityRepository _cities;
		private readonly IClientRepository _clients;
		private readonly ITransitRepository _transits;

		public ForwarderApplication(ICityRepository cities, IClientRepository clients, ITransitRepository transits)
		{
			_cities = cities;
			_clients = clients;
			_transits = transits;
		}

		public void UpdateDeliveryData(ApplicationListItem[] applicationItems, string language)
		{
			var clientTransitIds = _clients.GetAll()
				.ToDictionary(x => x.TransitId, x => x.ClientId);

			var transits = _transits.Get(clientTransitIds.Select(x => x.Key).ToArray())
				.ToDictionary(x => clientTransitIds[x.Id], x => x);

			var cities = _cities.All(language).ToDictionary(x => x.Id, x => x.Name);

			foreach(var item in applicationItems)
			{
				if(item.IsPickup)
				{
					var transit = transits[item.ClientId];

					item.TransitCity = cities[transit.CityId];
					item.CarrierContact = transit.RecipientName;
					item.CarrierAddress = transit.Address;
					item.CarrierPhone = transit.Phone;
				}
			}
		}
	}
}