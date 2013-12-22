using System.IO;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users
{
	internal sealed class SenderService : ISenderService
	{
		private readonly ISenderRepository _senders;
		private readonly IUserRepository _users;

		public SenderService(ISenderRepository senders, IUserRepository users)
		{
			_senders = senders;
			_users = users;
		}

		public SenderModel Get(long id)
		{
			var data = _senders.Get(id);

			return new SenderModel
			{
				Name = data.Name,
				Authentication = new AuthenticationModel(data.Login),
				Email = data.Email,
				TariffOfTapePerBox = data.TariffOfTapePerBox
			};
		}

		public long Add(SenderModel model)
		{
			if (model.TariffOfTapePerBox < 0)
			{
				throw new InvalidDataException("TariffOfTapePerBox can't be less than 0.");
			}

			var id = _senders.Add(new SenderData
			{
				Email = model.Email,
				Login = model.Authentication.Login,
				Name = model.Name,
				TariffOfTapePerBox = model.TariffOfTapePerBox,
				Language = TwoLetterISOLanguageName.English
			}, model.Authentication.NewPassword);

			return id;
		}

		public void Update(long id, SenderModel model)
		{
			if (model.TariffOfTapePerBox < 0)
			{
				throw new InvalidDataException("TariffOfTapePerBox can't be less than 0.");
			}

			var data = _senders.Get(id);

			data.Name = model.Name;
			data.Login = model.Authentication.Login;
			data.Email = model.Email;
			data.TariffOfTapePerBox = model.TariffOfTapePerBox;

			_senders.Update(id, data);

			if (!string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
			{
				var userId = _senders.GetUserId(id);

				_users.SetPassword(userId, model.Authentication.NewPassword);
			}
		}
	}
}