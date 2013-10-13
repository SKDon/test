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

		$c.Post = post;

		function findGroup(data, awbId) {
			for (var i in data) {
				if (data[i].AirWaybillId == awbId) {
					return data[i];
				}
			}
			return null;
		}

		function rebindData(oldData, data) {
			var aggregates = data.Groups[0].aggregates;
			for (var i in aggregates) {
				oldData.aggregates[i].sum = aggregates[i].sum;
			}

			var items = data.Groups[0].items;
			for (var itemIndex in items) {
				oldData.items[itemIndex].TotalTariffCost = items[itemIndex].TotalTariffCost;
				oldData.items[itemIndex].TotalSenderRate = items[itemIndex].TotalSenderRate;
				oldData.items[itemIndex].Profit = items[itemIndex].Profit;
				oldData.items[itemIndex].IsCalculated = items[itemIndex].IsCalculated;
			}

			for (var infoIndex in $c.CalculationInfo) {
				if ($c.CalculationInfo[infoIndex].AirWaybillId == data.Info[0].AirWaybillId) {
					$.extend($c.CalculationInfo[infoIndex], data.Info[0]);
					break;
				}
			}
		}

		function updateMailGrid(awbId) {
			$.post($u.Calculation_Row, { id: awbId }).success(function (data) {
				var grid = $c.GetMainGrid();
				var oldData = findGroup(grid.dataSource.data(), awbId);

				rebindData(oldData, data);
				var scrollTop = $(window).scrollTop();
				grid.refresh();
				$(window).scrollTop(scrollTop);

			}).fail($a.ShowError);
		};

		function initAdditionalCost(row, data) {
			row.find(".additional-cost input").kendoNumericTextBox({
				decimals: 2,
				spinners: false,
				change: function () {
					post($u.Calculation_SetAdditionalCost, {
						awbId: data.AirWaybillId,
						additionalCost: this.value()
					}, data.AirWaybillId);
				}
			});
		}

		$(function () {
			function dataBound() {
				var detailsTemplate = kendo.template($("#calculation-grid-details-template").html());

				function getDetails(awbId) {
					var awbDetails = null;
					for (var infoIndex in $c.CalculationInfo) {
						var info = $c.CalculationInfo[infoIndex];
						if (awbId == info.AirWaybillId) {
							awbDetails = info;
							break;
						}
					}
					return awbDetails;
				}

				$("tr a.k-grid-custom-gear").each(function () {
					var dataItem = $c.GetMainGrid().dataItem($(this).closest("tr"));
					if (dataItem.IsCalculated) {
						$(this).remove();
					}
				});

				$("tr.k-group-footer").each(function (i) {
					var awbId = $c.CalculationInfo[i].AirWaybillId;

					var awbDetails = getDetails(awbId);

					var detailsHtml = $(detailsTemplate(awbDetails));

					initAdditionalCost(detailsHtml, awbDetails);

					$(this).after(detailsHtml);
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
				if (e.values.SenderRate !== undefined) {
					post($u.Calculation_SetSenderRate, {
						id: e.model.ApplicationId,
						senderRate: e.values.SenderRate
					}, awbId);
				}
			}

			function edit(e) {
				if (e.model.IsCalculated) {
					$c.GetMainGrid().closeCell();
				}
			}

			var gridHolder = $("#calculation-grid");
			gridHolder.kendoGrid({
				columns: $c.Columns(),
				pageable: { refresh: true, pageSizes: [5, 20, 50, 100] },
				dataSource: $c.DataSource(),
				editable: true,
				resizable: true,
				save: save,
				dataBound: dataBound,
				edit: edit
			});
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});