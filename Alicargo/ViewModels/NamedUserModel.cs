//using System;
//using System.ComponentModel.DataAnnotations;
//using Alicargo.Core.Localization;
//using Alicargo.Core.Models;
//using Alicargo.Core.Repositories;
//using Resources;

//namespace Alicargo.ViewModels
//{
//	public sealed class NamedUserModel
//	{
//		public long? Id { get; set; }

//		[Required]
//		[DisplayNameLocalized(typeof(Pages), "Name")]
//		public string Name { get; set; }

//		public UserModel User { get; set; }

//		public void Fill(INamedUserHolder entity, Func<string, string, User> getUser = null)
//		{
//			entity.Name = Name;

//			if (getUser != null)
//				entity.User = getUser(User.Login, User.NewPassword);
//		}		
//	}
//}