﻿var Alicargo = (function ($a) {

	function getGrid() {
		var grid = $("#AirWaybill-grid").data("kendoGrid");
		getGrid = function () { return grid; };

		return getGrid();
	};

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
						var grid = getGrid();
						grid.dataSource.read();
						grid.refresh();
					};
					if (window.confirm(window.$a.Localization.Pages_DeleteConfirm)) {
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						var url = window.$a.Urls.AirWaybill_Delete;
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
			title: $a.Localization.Entities_FlightCost, format: "{0:n0}"
		}, {
			field: "BrokerCost",
			title: $a.Localization.Entities_BrokerCost, format: "{0:n0}"
		}, {
			field: "CustomCost",
			title: $a.Localization.Entities_CustomCost, format: "{0:n0}"
		}, {
			field: "AdditionalCost",
			title: $a.Localization.Entities_AdditionalCost, format: "{0:n0}"
		});

		addEditButton(columns, $a.Urls.AirWaybill_Edit);
		addDeleteButton(columns);
	}

	function addSenderColumns(columns) {
		columns.push({
			field: "FlightCost",
			title: $a.Localization.Entities_FlightCost, format: "{0:n0}"
		});

		addEditButton(columns, $a.Urls.SenderAwb_Edit);
		addDeleteButton(columns);
	}

	function addBrokerColumns(columns) {
		columns.push({
			field: "BrokerCost",
			title: $a.Localization.Entities_BrokerCost, format: "{0:n0}"
		}, {
			field: "CustomCost",
			title: $a.Localization.Entities_CustomCost, format: "{0:n0}"
		});

		addEditButton(columns, $a.Urls.Broker_AWB);
	}

	$a.Awb = (function($awb) {
		$awb.AddColumns = function () {
			var columns = [
				{ field: "CreationTimestampLocalString", title: $a.Localization.Entities_CreationTimestamp },
				{ field: "State", title: $a.Localization.Entities_StateName, template: "#= !!State && !!State.StateName ? State.StateName : '' #" },
				{ field: "Bill", title: $a.Localization.Entities_AWB },
				{ field: "DepartureAirport", title: $a.Localization.Entities_DepartureAirport },
				{ field: "ArrivalAirport", title: $a.Localization.Entities_ArrivalAirport },
				{ field: "DateOfDepartureLocalString", title: $a.Localization.Entities_DateOfDeparture },
				{ field: "DateOfArrivalLocalString", title: $a.Localization.Entities_DateOfArrival },
				{ field: "TotalCount", title: $a.Localization.Entities_TotalCount },
				{ field: "TotalWeight", title: $a.Localization.Entities_TotalWeight },
				{ field: "StateChangeTimestampLocalString", title: $a.Localization.Entities_StateChangeTimestamp },
				{ field: "GTD", title: $a.Localization.Entities_GTD }
			];

			if ($a.Roles.IsAdmin) {
				addAdminColumns(columns);
			} else if ($a.Roles.IsSender) {
				addSenderColumns(columns);
			} else if ($a.Roles.IsBroker) {
				addBrokerColumns(columns);
			}

			return columns;
		};
	})($a.Awb || {});	

	return $a;
}(Alicargo || {}));