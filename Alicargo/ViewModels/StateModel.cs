﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels
{
	[Obsolete] // todo: 3. obsolete?
	public sealed class StateModel
	{
		public StateModel() { }

		public StateModel(StateData state, Dictionary<long, string> states = null)
		{
			Name = state.Name;
			RussianName = state.Localization[TwoLetterISOLanguageName.Russian];
			ItalianName = state.Localization[TwoLetterISOLanguageName.Italian];

			States = states;
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "StateRussianName")]
		public string RussianName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "StateItalianName")]
		public string ItalianName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "StateNextName")]
		public long? NextId { get; set; }

		public Dictionary<long, string> States { get; set; }
	}
}