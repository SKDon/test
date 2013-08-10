using System.Collections.Generic;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Contracts;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationGrouper
	{
		ApplicationGroup[] Group(ApplicationListItem[] models, IReadOnlyCollection<Order> groups, IDictionary<long, ReferenceData> references);
	}
}