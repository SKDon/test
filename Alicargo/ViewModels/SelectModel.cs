using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alicargo.ViewModels
{
	[Obsolete]
	public sealed class SelectModel
	{
		[Required]
		public long Id { get; set; }

		public IDictionary<long, string> List { get; set; }

		public string Name { get; set; }
	}

	public sealed class SelectModel<T>
	{
		private T _selected;

		[Required]
		public T Selected
		{
			get
			{
				if (Equals(_selected, null))
				{
					_selected = List.First().Key;
				}
				return _selected;
			}
			set { _selected = value; }
		}

		public IDictionary<T, string> List { get; set; }
	}
}