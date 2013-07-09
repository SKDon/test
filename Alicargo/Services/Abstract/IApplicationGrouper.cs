using System.Collections.Generic;
using Alicargo.Core.Helpers;
using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationGrouper
	{
		ApplicationGroup[] Group(ApplicationModel[] models, IReadOnlyCollection<Order> groups);
	}
}