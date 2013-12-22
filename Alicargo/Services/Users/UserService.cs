using System;
using System.Linq;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Localization;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users
{
	internal sealed class UserService : IUserService
	{
		private readonly IAdminRepository _admins;
		private readonly IBrokerRepository _brokers;
		private readonly IForwarderRepository _forwarders;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository,
			IAdminRepository admins,
			IForwarderRepository forwarders,
			IBrokerRepository brokers,
			IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_admins = admins;
			_forwarders = forwarders;
			_brokers = brokers;
			_unitOfWork = unitOfWork;
		}

		public UserListItem[] List(RoleType role)
		{
			switch (role)
			{
				case RoleType.Admin:
					return _admins.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.EntityId,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Broker:
					return _brokers.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.Id,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Forwarder:
					return _forwarders.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.EntityId,
							UserId = x.UserId
						}).ToArray();

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}

		public UserModel Get(RoleType role, long id)
		{
			var data = GetByRole(role, id);

			return new UserModel
			{
				Id = data.EntityId,
				Name = data.Name,
				RoleType = role,
				Email = data.Email,
				Authentication = new AuthenticationModel
				{
					Login = data.Login
				}
			};
		}

		private UserData GetByRole(RoleType role, long id)
		{
			switch (role)
			{
				case RoleType.Admin:
					return _admins.GetAll().First(x => x.EntityId == id);

				case RoleType.Broker:
					var broker = _brokers.Get(id);
					return new UserData
					{
						Email = broker.Email,
						TwoLetterISOLanguageName = broker.TwoLetterISOLanguageName,
						Login = broker.Login,
						EntityId = broker.Id,
						Name = broker.Name,
						UserId = broker.UserId
					};

				case RoleType.Forwarder:
					return _forwarders.GetAll().First(x => x.EntityId == id);

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}

		public void Update(UserModel model)
		{
			//switch (model.RoleType)
			//{
			//	case RoleType.Admin:
			//		_admins.Update(model.Id, model.Name, model.Authentication.Login, model.Email);
			//		_userRepository.SetPassword(model.Authentication., model.Authentication.NewPassword);
			//		break;

			//	case RoleType.Broker:
			//		_userRepository.UpdateBroker(model.Id, model.Name, model.Authentication.Login, model.Email,
			//			model.Authentication.NewPassword);
			//		break;

			//	case RoleType.Forwarder:
			//		_userRepository.UpdateForwarder(model.Id, model.Name, model.Authentication.Login, model.Email,
			//			model.Authentication.NewPassword);
			//		break;

			//	default:
			//		throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			//}

			_unitOfWork.SaveChanges();
		}

		public void Add(UserModel model)
		{
			var twoLetterISOLanguageName = CultureContext.Current.GetTwoLetterISOLanguageName();

			//switch (model.RoleType)
			//{
			//	case RoleType.Admin:
			//		_admins.Add(model.Name, model.Authentication.Login, model.Email, twoLetterISOLanguageName);
			//		break;

			//	case RoleType.Broker:
			//		_userRepository.AddBroker(model.Id, model.Name, model.Authentication.Login, model.Email,
			//			model.Authentication.NewPassword, twoLetterISOLanguageName);
			//		break;

			//	case RoleType.Forwarder:
			//		_userRepository.AddForwarder(model.Id, model.Name, model.Authentication.Login, model.Email,
			//			model.Authentication.NewPassword, twoLetterISOLanguageName);
			//		break;

			//	default:
			//		throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			//}

			_unitOfWork.SaveChanges();
		}
	}
}