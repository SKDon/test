﻿using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.Services.Users.Client;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Client
{
	[TestClass]
	public class ClientManagerTests
	{
		[TestMethod, ExpectedException(typeof(AccessForbiddenException))]
		public void Test_Update_Not_HaveAccessToClient()
		{
			var container = new MockContainer();
			var clientId = container.Create<long>();
			var data = container.Create<ClientData>();

			container.ClientRepository.Setup(x => x.Get(clientId)).Returns(data);
			container.ClientPermissions.Setup(x => x.HaveAccessToClient(data)).Returns(false);

			var manager = container.Create<ClientManager>();

			manager.Update(clientId, It.IsAny<ClientModel>(), It.IsAny<TransitEditModel>());
		}
	}
}