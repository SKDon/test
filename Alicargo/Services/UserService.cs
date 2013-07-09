using System;
using System.Linq;
using System.Threading;
using Alicargo.Core.Enums;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	public sealed class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}

		public UserModel[] List(RoleType role)
		{
			var dictionary = _userRepository.GetByRole(role);

			return dictionary.Select(x => new UserModel
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				RoleType = role,
				Authentication = new AuthenticationModel
				{
					Login = x.Login
				}
			}).ToArray();
		}

		public UserModel Get(RoleType role, long id)
		{
			var data = _userRepository.GetByRole(role).First(x => x.Id == id);

			return new UserModel
			{
				Id = data.Id,
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

				case RoleType.Sender:
					_userRepository.UpdateSender(model.Id, model.Name, model.Authentication.Login, model.Email, model.Authentication.NewPassword);
					break;

				case RoleType.Brocker:
					_userRepository.UpdateBrocker(model.Id, model.Name, model.Authentication.Login, model.Email, model.Authentication.NewPassword);
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
			var twoLetterISOLanguageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

			switch (model.RoleType)
			{
				case RoleType.Admin:
					_userRepository.AddAdmin(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword, twoLetterISOLanguageName);
					break;

				case RoleType.Sender:
					_userRepository.AddSender(model.Id, model.Name, model.Authentication.Login, model.Email,
						model.Authentication.NewPassword, twoLetterISOLanguageName);
					break;

				case RoleType.Brocker:
					_userRepository.AddBrocker(model.Id, model.Name, model.Authentication.Login, model.Email,
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