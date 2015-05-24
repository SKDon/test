using System.ComponentModel.DataAnnotations;

namespace Alicargo.ViewModels
{
	public sealed class FeedbackModel
	{
		public string FactoryName { get; set; }

		public string FactoryPhone { get; set; }

		public string FactoryEmail { get; set; }

		public string FactoryContact { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public string UserPhone { get; set; }

		public string UserEmail { get; set; }

		public string UserCallTime { get; set; }

		public string Comment { get; set; }
	}
}