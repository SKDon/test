namespace Alicargo.Core.Contracts.State
{
	public interface IStateConfig
	{
		long DefaultStateId { get; }

		/// <summary>
		/// Груз на складе - 6
		/// Можно назначать, если указаны количество и вес
		/// </summary>
		long CargoInStockStateId { get; }

		/// <summary>
		/// Груз получен - 11
		/// </summary>
		long CargoReceivedStateId { get; }

		int CargoReceivedDaysToShow { get; }

		/// <summary>
		/// На транзите - 12
		/// </summary>
		long CargoOnTransitStateId { get; }

		#region AWB

		// todo: add an AWB flag to the States table (264)
		long[] AwbStates { get; }

		/// <summary>
		/// Груз вылетел - 7
		/// Первый статус в накладной
		/// </summary>
		long CargoIsFlewStateId
		{
			get;
		}

		/// <summary>
		/// Груз на таможне - 8
		/// Можно назначать, если указано ГТД
		/// </summary>
		long CargoAtCustomsStateId
		{
			get;
		}

		/// <summary>
		/// Груз выпущен - 9
		/// Можно редактировать статус заявки
		/// Можно назначать, если указано ГТД
		/// </summary>
		long CargoIsCustomsClearedStateId
		{
			get;
		}

		#endregion
	}
}