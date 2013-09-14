var Alicargo = (function ($a) {

	function addEditButton(columns, editUrl) {
		columns.push({
			command: [{
				name: "custom-edit",
				text: "",
				click: function (e) {
					var tr = $(e.target).closest("tr");
					var data = this.dataItem(tr);
					var url = editUrl + "/" + data.Id;
					window.location = url;
				}
			}],
			title: "&nbsp;",
			width: "53px"
		});
	}

	function addDeleteButton(columns) {
		columns.push({
			command: [{
				name: "custom-delete",
				text: "",
				click: function (e) {
					var updateGrid = function () {
						var grid = $a.GetGrid();
						grid.dataSource.read();
						grid.refresh();
					};
					if (window.confirm(window.Alicargo.Localization.Pages_DeleteConfirm)) {
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						var url = window.Alicargo.Urls.AirWaybill_Delete;
						$.post(url, { id: data.Id }).done(updateGrid).fail(error);
					}
				}
			}],
			title: "&nbsp;",
			width: "53px"
		});
	}

	function addAdminColumns(columns) {
		columns.push({
			field: "FlightCost",
			title: Alicargo.Localization.Entities_FlightCost, format: "{0:n0}"
		}, {
			field: "BrokerCost",
			title: Alicargo.Localization.Entities_BrokerCost, format: "{0:n0}"
		}, {
			field: "CustomCost",
			title: Alicargo.Localization.Entities_CustomCost, format: "{0:n0}"
		}, {
			field: "AdditionalCost",
			title: Alicargo.Localization.Entities_AdditionalCost, format: "{0:n0}"
		});

		addEditButton(columns, Alicargo.Urls.AirWaybill_Edit);
		addDeleteButton(columns);
	}

	function addSenderColumns(columns) {
		columns.push({
			field: "FlightCost",
			title: Alicargo.Localization.Entities_FlightCost, format: "{0:n0}"
		});

		addEditButton(columns, Alicargo.Urls.SenderAwb_Edit);
		addDeleteButton(columns);
	}

	function addBrokerColumns(columns) {
		columns.push({
			field: "BrokerCost",
			title: Alicargo.Localization.Entities_BrokerCost, format: "{0:n0}"
		}, {
			field: "CustomCost",
			title: Alicargo.Localization.Entities_CustomCost, format: "{0:n0}"
		});

		addEditButton(columns, Alicargo.Urls.Broker_AWB);
	}

	$a.AddColumns = function () {
		var columns = [
			{ field: "CreationTimestampLocalString", title: Alicargo.Localization.Entities_CreationTimestamp },
			{ field: "State", title: Alicargo.Localization.Entities_StateName, template: "#= !!State && !!State.StateName ? State.StateName : '' #" },
			{ field: "Bill", title: Alicargo.Localization.Entities_AWB },
			{ field: "DepartureAirport", title: Alicargo.Localization.Entities_DepartureAirport },
			{ field: "ArrivalAirport", title: Alicargo.Localization.Entities_ArrivalAirport },
			{ field: "DateOfDepartureLocalString", title: Alicargo.Localization.Entities_DateOfDeparture },
			{ field: "DateOfArrivalLocalString", title: Alicargo.Localization.Entities_DateOfArrival },
			{ field: "TotalCount", title: Alicargo.Localization.Entities_TotalCount },
			{ field: "TotalWeight", title: Alicargo.Localization.Entities_TotalWeight },
			{ field: "StateChangeTimestampLocalString", title: Alicargo.Localization.Entities_StateChangeTimestamp },
			{ field: "GTD", title: Alicargo.Localization.Entities_GTD }
		];

		if (Alicargo.Roles.IsAdmin) {
			addAdminColumns(columns);
		} else if (Alicargo.Roles.IsSender) {
			addSenderColumns(columns);
		} else if (Alicargo.Roles.IsBroker) {
			addBrokerColumns(columns);
		}

		return columns;
	};

	return $a;
}(Alicargo || {}));