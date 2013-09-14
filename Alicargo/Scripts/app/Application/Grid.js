$(function () {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;
	var $r = $a.Roles;
	var $s = $a.States;

	$a.Application = (function ($apl) {
		$apl.GetGrid = function () {
			var grid = $("#application-grid").data("kendoGrid");
			$apl.GetGrid = function () { return grid; };
			return $apl.GetGrid();
		};

		$apl.UpdateGrid = function () {
			var grid = $apl.GetGrid();
			grid.dataSource.read();
			grid.refresh();
		};

		return $apl;
	})($a.Application || {});

	var schema = {
		model: { id: "Id" },
		data: "Data",
		total: "Total",
		groups: "Groups"
	};

	var dataSource = {
		transport: {
			read: {
				dataType: "json",
				url: $u.ApplicationList_List,
				type: "POST"
			}
		},
		schema: schema,
		pageSize: 20,
		serverPaging: true,
		editable: true,
		serverGrouping: true,
		error: $a.ShowError
	};

	if (!$r.IsClient) {
		$.extend(dataSource, { group: { field: "AirWaybill", dir: "asc" } });
	}	

	var detailExpand = function (e) {
		var data = $a.Application.GetGrid().dataItem(e.masterRow);

		$(".close-application", e.detailRow).click(function () {
			if (window.confirm($l.Pages_StateSetConfirm)) {
				var url = $u.ApplicationUpdate_Close;
				$.post(url, { id: data.Id }).done($a.Application.UpdateGrid).fail($a.ShowError);
			}
		});

		$(".delete-application", e.detailRow).click(function () {
			if (window.confirm($l.Pages_DeleteConfirm)) {
				var url = $u.Application_Delete;
				$.post(url, { id: data.Id }).done($a.Application.UpdateGrid).fail($a.ShowError);
			}
		});

		$(".AirWaybill-select", e.detailRow)
			.val(data.AirWaybillId)
			.change(function () {
				if (window.confirm($l.Pages_AirWaybillSetConfirm)) {
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
	var detailCollapse = function (e) {
		$(".close-application", e.detailRow).unbind("click");
		$(".delete-application", e.detailRow).unbind("click");
		$(".AirWaybill-select", e.detailRow).unbind("change");
	};

	var columns = $a.Application.GetColumns();

	var settings = {
		dataSource: dataSource,
		filterable: false,
		sortable: false,
		groupable: true,
		pageable: { refresh: true, pageSizes: [10, 20, 50, 100] },
		resizable: true,
		detailTemplate: kendo.template($("#application-grid-details").html()),
		detailExpand: detailExpand,
		detailCollapse: detailCollapse,
		columns: columns
	};

	if (!$r.IsClient && !$r.IsBroker)
		$.extend(settings, {
			editable: true,
			edit: function (e) {
				if (e.container.find("input[name='TransitReference']").length) return;
				if (e.container.find("input[name='DateOfCargoReceiptLocalString']").length) return;
				var stateName = e.container.find("input[data-text-field='StateName']");
				if (!stateName.length || !e.model.CanSetState) $a.Application.GetGrid().closeCell();
			},
			save: function (e) {
				if (e.values.TransitReference !== undefined) {
					$.post($u.ApplicationUpdate_SetTransitReference, {
						id: e.model.Id,
						TransitReference: e.values.TransitReference
					}).fail($a.ShowError);
				}
				if (e.values.DateOfCargoReceiptLocalString !== undefined) {
					$.post($u.ApplicationUpdate_SetDateOfCargoReceipt, {
						id: e.model.Id,
						dateOfCargoReceipt: kendo.toString(e.values.DateOfCargoReceiptLocalString, "d")
					}).fail($a.ShowError);
				}
			}
		});

	$("#application-grid").kendoGrid(settings);
});