using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Calculation.Admin;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManagerWithEvent : IClientManager
	{
		private readonly IClientManager _manager;

		public ClientManagerWithEvent(IClientManager manager)
		{
			_manager = manager;
		}

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrier, TransitEditModel transit,
			AuthenticationModel authentication)
		{
			throw new NotImplementedException();
		}

		public long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
			AuthenticationModel authenticationModel)
		{
			throw new NotImplementedException();
		}

		public void AddToBalance(long clientId, PaymentModel model)
		{
			throw new NotImplementedException();
		}
	}
}