
var Alicargo = (function ($a) {

	var $u = $a.Urls;

	$a.Calculation = (function ($c) {

		$c.GetMainGrid = function () {
			var grid = $("#calculation-grid").data("kendoGrid");
			$c.GetMainGrid = function () { return grid; };
			return grid;
		};

		function post(url, data, awbId) {
			$.post(url, data).success(function () {
				updateMailGrid(awbId);
			}).fail($a.ShowError);
		}

		function updateMailGrid() {
			// todo: 1. implement
			//$.post($u.Calculation_Row, { id: awbId }).success(function (data) {
			//	var grid = $c.GetMainGrid();
			//	var oldData = grid.dataSource.get(awbId);
			//	$.extend(oldData, data);
			//	grid.refresh();
			//}).fail($a.ShowError);
		};

		function overrideFooter(gridHolder) {
			var grid = gridHolder.data("kendoGrid");

			var collapseGroupBase = grid.collapseGroup;

			grid.collapseGroup = function (group) {
				collapseGroupBase.call(grid, group);

				group.nextAll("tr").each(function () {
					var tr = $(this);
					if (tr.hasClass("k-group-sub-footer")) {
						tr.hide();
						return false;
					}

					return true;
				});

			};
		}

		$(function () {
			function dataBound() {
				var grid = this;

				var detailsTemplate = kendo.template($("#calculation-grid-details-template").html());

				$(".k-group-footer").each(function (i, e) {
					var awbId = grid.dataItem($(e).prevAll(".k-grouping-row:first")).AirWaybillId;

					var awbDetails = null;
					for (var infoIndex in $c.CalculationInfo) {
						var info = $c.CalculationInfo[infoIndex];
						if (awbId == info.AirWaybillId) {
							awbDetails = info;
							break;
						}
					}

					var detailsHtml = detailsTemplate(awbDetails);

					$(this).after("<tr class='k-group-footer k-group-sub-footer'><td class='k-group-cell'>&nbsp;</td>"
						+ "<td colspan='" + $c.Columns().length + "'>"
						+ detailsHtml
						+ "</td></tr>");
				});
			}

			function save(e) {
				var awbId = e.model.AirWaybillId;

				if (e.values.TariffPerKg !== undefined) {
					post($u.Calculation_SetTariffPerKg, {
						id: e.model.ApplicationId,
						tariffPerKg: e.values.TariffPerKg
					}, awbId);
				}
				if (e.values.ScotchCost !== undefined) {
					post($u.Calculation_SetScotchCostEdited, {
						id: e.model.ApplicationId,
						scotchCost: e.values.ScotchCost
					}, awbId);
				}
				if (e.values.FactureCost !== undefined) {
					post($u.Calculation_SetFactureCostEdited, {
						id: e.model.ApplicationId,
						factureCost: e.values.FactureCost
					}, awbId);
				}
				if (e.values.WithdrawCost !== undefined) {
					post($u.Calculation_SetWithdrawCostEdited, {
						id: e.model.ApplicationId,
						withdrawCost: e.values.WithdrawCost
					}, awbId);
				}
				if (e.values.TransitCost !== undefined) {
					post($u.ApplicationUpdate_SetTransitCost, {
						id: e.model.ApplicationId,
						transitCost: e.values.TransitCost
					}, awbId);
				}
			}

			var gridHolder = $("#calculation-grid");
			gridHolder.kendoGrid({
				columns: $c.Columns(),
				pageable: { refresh: true, pageSizes: [1, 3, 5, 10] },
				dataSource: $c.DataSource(),
				editable: true,
				resizable: true,
				save: save,
				dataBound: dataBound
			});

			overrideFooter(gridHolder);
		}).fail($a.ShowError);

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});