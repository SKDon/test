$(function() {

	var $a = Alicargo;
	var $u = $a.Urls;

	$a.Application = (function($apl) {
		var gridSelector = "#application-grid";

		$apl.GetGrid = function() {
			var grid = $(gridSelector).data("kendoGrid");
			$apl.GetGrid = function() { return grid; };
			return $apl.GetGrid();
		};

		$apl.UpdateGrid = function() {
			var grid = $apl.GetGrid();
			grid.dataSource.read();
			grid.refresh();
		};

		function getDataSource() {
			var schema = {
				model: {
					id: "Id",
					fields: {
						// должны быть перечислены все группируемые поля
						"TransitCost": { type: "number", editable: true }, // поле не группируемое, но задается тип
						"AirWaybill": { type: "string", editable: false },
						"State": { editable: true },
						"ClientNic": { type: "string", editable: false },
						"ClientLegalEntity": { type: "string", editable: false }
					}
				},
				data: "Data",
				total: "Total",
				groups: "Groups"
			};

			var dataSource = {
				transport: {
					read: {
						dataType: "json",
						url: $u.Forwarder_Applications_List,
						type: "POST",
						cache: false
					}
				},
				schema: schema,
				pageSize: $a.SelectedPageSize(gridSelector),
				serverPaging: true,
				serverGrouping: true,
				error: $a.ShowError,
				group: { field: "AirWaybill", dir: "desc" },
				groupable: true
			};

			return dataSource;
		}

		function post(url, data) {
			$.post(url, data).fail($a.ShowError).success(function() {
				$apl.GetGrid().refresh();
			});
		}

		function getSettings() {
			var settings = {
				dataSource: getDataSource(),
				filterable: false,
				sortable: false,
				groupable: false,
				columns: $a.Application.GetColumns(),
				editable: true,
				edit: function(e) {
					var canEdit = e.container.find("input[name='TransitReference']").length != 0;

					if (!canEdit) {
						canEdit = e.container.find("input[name='DateOfCargoReceiptLocalString']").length != 0;
					}

					if (!canEdit && e.model.CanSetTransitCost) {
						canEdit = e.container.find("input[name='ForwarderTransitCost']").length != 0;
					}

					if (!canEdit && e.model.CanSetState) {
						canEdit = e.container.find("input[data-text-field='StateName']").length != 0;
					}

					if (!canEdit) {
						$a.Application.GetGrid().closeCell();
					}

				},
				save: function(e) {
					if (e.values.ForwarderTransitCost !== undefined) {
						post($u.ApplicationUpdate_SetTransitCost, {
							id: e.model.Id,
							transitCost: e.values.ForwarderTransitCost
						});
					}
					if (e.values.TransitReference !== undefined) {
						$.post($u.ApplicationUpdate_SetTransitReference, {
							id: e.model.Id,
							TransitReference: e.values.TransitReference
						}).done($apl.UpdateGrid).fail($a.ShowError);
					}
				}
			};

			return settings;
		}

		$a.CreateGrid(gridSelector, getSettings());

		return $apl;
	})($a.Application || {});
});