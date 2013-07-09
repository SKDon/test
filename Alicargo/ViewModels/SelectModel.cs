using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alicargo.ViewModels
{
	public sealed class SelectModel
	{
		[Required]
		public long Id { get; set; }

		public Dictionary<long, string> List { get; set; }

		public string Name { get; set; }
	}
}