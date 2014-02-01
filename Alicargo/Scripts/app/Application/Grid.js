$(function() {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;
	var $r = $a.Roles;
	var $s = $a.States;

	if ($r.IsSender || $r.IsAdmin) {
		var select = $("#selected-client-id");
		var updateLink = function() {
			var link = $("#apply-link");
			link.attr("href", select.val());
		};
		select.change(updateLink);
		updateLink();
	}

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
						url: $u.ApplicationList_List,
						type: "POST",
						cache: false
					}
				},
				schema: schema,
				pageSize: $a.SelectedPageSize(gridSelector),
				serverPaging: true,
				serverGrouping: true,
				error: $a.ShowError,
				group: { field: "AirWaybill", dir: "desc" }
			};

			if (!$r.IsClient) {
				$.extend(dataSource, { groupable: true });
			}

			return dataSource;
		}

		function getSettings() {
			var settings = {
				dataSource: getDataSource(),
				filterable: false,
				sortable: false,
				groupable: false,
				columns: $a.Application.GetColumns()
			};

			if ($r.IsAdmin) {
				$.extend(settings, { groupable: true });
			}

			if (!$r.IsForwarder && !$r.IsCarrier) {
				var detailExpand = function(e) {
					var data = $a.Application.GetGrid().dataItem(e.masterRow);

					$(".close-application", e.detailRow).click(function() {
						if ($a.Confirm($l.Pages_StateSetConfirm)) {
							var url = $u.ApplicationUpdate_Close;
							$.post(url, { id: data.Id }).done($a.Application.UpdateGrid).fail($a.ShowError);
						}
					});

					$(".delete-application", e.detailRow).click(function() {
						if ($a.Confirm($l.Pages_DeleteConfirm)) {
							var url = $u.Application_Delete;
							$.post(url, { id: data.Id }).done($a.Application.UpdateGrid).fail($a.ShowError);
						}
					});

					$(".AirWaybill-select", e.detailRow)
						.val(data.AirWaybillId)
						.change(function() {
							if ($a.Confirm($l.Pages_AirWaybillSetConfirm)) {
								var url = $u.AirWaybill_SetAirWaybill;
								$.post(url, { AirWaybillId: $(this).val(), applicationId: data.Id }).done($a.Application.UpdateGrid).fail($a.ShowError);
							}
						});

					if (data.AirWaybillId && data.StateId != $s.CargoIsFlewStateId) {
						$(".AirWaybill-holder", e.detailRow).hide();
					} else {
						$(".AirWaybill-holder", e.detailRow).show();
					}
				};

				var detailCollapse = function(e) {
					$(".close-application", e.detailRow).unbind("click");
					$(".delete-application", e.detailRow).unbind("click");
					$(".AirWaybill-select", e.detailRow).unbind("change");
				};

				$.extend(settings, {
					detailTemplate: kendo.template($("#application-grid-details").html()),
					detailExpand: detailExpand,
					detailCollapse: detailCollapse
				});
			}

			if ($r.IsClient) {
				$.extend(settings, {
					dataBound: function() { this.expandRow(this.tbody.find("tr.k-master-row")); }
				});
			}

			function post(url, data) {
				$.post(url, data).fail($a.ShowError).success(function() {
					$apl.GetGrid().refresh();
				});
			}

			if (!$r.IsClient && !$r.IsBroker)
				$.extend(settings, {
					editable: true,
					edit: function(e) {
						var canEdit = !$r.IsCarrier && e.container.find("input[name='TransitReference']").length != 0;

						if (!canEdit) {
							canEdit = e.container.find("input[name='DateOfCargoReceiptLocalString']").length != 0;
						}

						if (!canEdit) {
							if (e.model.CanSetTransitCost && $r.IsForwarder) {
								canEdit = e.container.find("input[name='ForwarderTransitCost']").length != 0;
							}
						}

						if (!canEdit && e.model.CanSetState) {
							canEdit = e.container.find("input[data-text-field='StateName']").length != 0;
						}

						if (!canEdit) {
							$a.Application.GetGrid().closeCell();
						}

					},
					save: function(e) {
						if ($r.IsForwarder && e.values.ForwarderTransitCost !== undefined) {
							post($u.ApplicationUpdate_SetTransitCost, {
								id: e.model.Id,
								transitCost: e.values.ForwarderTransitCost
							});
						}
						if (($r.IsForwarder || $r.IsAdmin) && e.values.TransitReference !== undefined) {
							$.post($u.ApplicationUpdate_SetTransitReference, {
								id: e.model.Id,
								TransitReference: e.values.TransitReference
							}).done($apl.UpdateGrid).fail($a.ShowError);
						}
						if ($r.IsAdmin && e.values.DateOfCargoReceiptLocalString !== undefined) {
							post($u.ApplicationUpdate_SetDateOfCargoReceipt, {
								id: e.model.Id,
								dateOfCargoReceipt: kendo.toString(e.values.DateOfCargoReceiptLocalString, "d")
							});
						}
					}
				});

			return settings;
		}

		$a.CreateGrid(gridSelector, getSettings());

		return $apl;
	})($a.Application || {});
});