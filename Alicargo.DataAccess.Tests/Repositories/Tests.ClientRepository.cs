//using Alicargo.Contracts.Contracts;
//using Alicargo.Core.Models;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Ploeh.AutoFixture;

//namespace Alicargo.DataAccess.Tests.Repositories
//{
//	public partial class Tests
//	{
//		private Client CreateTestClient()
//		{
//			var user = CreateTestUser();
//			var transit = CreateTestTransit();

//			var data = _fixture.Create<Client>();
//			data.UserId = user.Id;
//			data.Transit = TransitEditModel.GetModel(transit);
//			data.AuthenticationModel = new AuthenticationModel(user.Login);
//			data.TransitId = transit.Id;

//			var id = _clientRepository.Add(data);
//			_unitOfWork.SaveChanges();
//			data.Id = id();

//			return data;
//		}

//		[TestMethod]
//		public void Test_ClientRepository_Count()
//		{
//			var all = _clientRepository.GetAll();

//			var count = _clientRepository.Count();

//			Assert.AreEqual(all.Length, count);
//		}

//		[TestMethod]
//		public void Test_ClientRepository_GetRange()
//		{
//			var count = _clientRepository.Count();

//			var range = _clientRepository.GetRange(count - count / 2, (int)count);

//			Assert.AreEqual(range.Length, count / 2);
//		}

//		[TestMethod]
//		public void Test_ClientRepository_Add_GetByUserId_GetById_Delete()
//		{
//			var client = CreateTestClient();

//			var byId = _clientRepository.GetById(client.Id);

//			Assert.IsNotNull(byId);

//			client.ShouldBeEquivalentTo(byId);

//			var byUserId = _clientRepository.GetByUserId(client.UserId);

//			Assert.IsNotNull(byUserId);

//			client.ShouldBeEquivalentTo(byUserId);
//		}

//		[TestMethod]
//		public void Test_ClientRepository_Delete()
//		{
//			var client = CreateTestClient();

//			_clientRepository.Delete(client.Id);
//			_unitOfWork.SaveChanges();

//			var byId = _clientRepository.GetById(client.Id);

//			Assert.IsNull(byId);
//		}

//		[TestMethod]
//		public void Test_ClientRepository_Update()
//		{
//			var client = CreateTestClient();
//			var newData = _fixture.Create<ClientData>();
//			newData.Id = client.Id;
//			newData.UserId = client.UserId;
//			newData.TransitId = client.TransitId;
//			newData.CopyTo(client);

//			_clientRepository.Update(client.Id, client);
//			_unitOfWork.SaveChanges();

//			var byId = _clientRepository.GetById(client.Id);

//			client.ShouldBeEquivalentTo(byId);
//		}
//	}
//}
