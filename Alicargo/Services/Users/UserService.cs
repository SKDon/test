using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
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
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}

		public UserData[] List(RoleType role)
		{
			return _userRepository.GetByRole(role);			
		}

		public UserModel Get(RoleType role, long id)
		{
			var data = _userRepository.GetByRole(role).First(x => x.EntityId == id);

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

		public void Update(UserModel model)
		{
			switch (model.RoleType)
			{
				case RoleType.Admin:
					_userRepository.UpdateAdmin(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword);
					break;

				case RoleType.Broker:
					_userRepository.UpdateBroker(model.Id, model.Name, model.Authentication.Login, model.Email, model.Authentication.NewPassword);
					break;

				case RoleType.Forwarder:
					_userRepository.UpdateForwarder(model.Id, model.Name, model.Authentication.Login, model.Email, model.Authentication.NewPassword);
					break;

				default:
					throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			}

			_unitOfWork.SaveChanges();
		}

		public void Add(UserModel model)
		{
			var twoLetterISOLanguageName = CultureContext.Current.GetTwoLetterISOLanguageName();

			switch (model.RoleType)
			{
				case RoleType.Admin:
					_userRepository.AddAdmin(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword, twoLetterISOLanguageName);
					break;

				case RoleType.Broker:
					_userRepository.AddBroker(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword, twoLetterISOLanguageName);
					break;

				case RoleType.Forwarder:
					_userRepository.AddForwarder(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword, twoLetterISOLanguageName);
					break;

				default:
					throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			}

			_unitOfWork.SaveChanges();
		}
	}
}