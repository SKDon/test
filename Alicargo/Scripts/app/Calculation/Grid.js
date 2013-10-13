var Alicargo = (function ($a) {

	var $u = $a.Urls;

	$a.Calculation = (function ($c) {

		$c.GetMainGrid = function () {
			var grid = $("#calculation-grid").data("kendoGrid");
			$c.GetMainGrid = function () { return grid; };
			return grid;
		};

		function post(url, data, awbId) {
			var scrollTop = $(window).scrollTop();
			$.post(url, data).success(function (awbData) {
				updateMailGrid(awbId, awbData);
				$(window).scrollTop(scrollTop);
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

		function updateMailGrid(awbId, awbData) {
			var grid = $c.GetMainGrid();
			var oldData = findGroup(grid.dataSource.data(), awbId);
			rebindData(oldData, awbData);
			grid.refresh();
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
					var button = $(this);
					var dataItem = $c.GetMainGrid().dataItem(button.closest("tr"));
					$.data(button[0], "ApplicationId", dataItem.ApplicationId);
					$.data(button[0], "AirWaybillId", dataItem.AirWaybillId);
					if (dataItem.IsCalculated) {
						button.remove();
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
				var applicationId = e.model.ApplicationId;
				
				if (e.values.TariffPerKg !== undefined) {
					post($u.Calculation_SetTariffPerKg, {
						id: applicationId,
						awbId: awbId,
						tariffPerKg: e.values.TariffPerKg
					}, awbId);
				}
				if (e.values.ScotchCost !== undefined) {
					post($u.Calculation_SetScotchCostEdited, {
						id: applicationId,
						awbId: awbId,
						scotchCost: e.values.ScotchCost
					}, awbId);
				}
				if (e.values.FactureCost !== undefined) {
					post($u.Calculation_SetFactureCostEdited, {
						id: applicationId,
						awbId: awbId,
						factureCost: e.values.FactureCost
					}, awbId);
				}
				if (e.values.WithdrawCost !== undefined) {
					post($u.Calculation_SetWithdrawCostEdited, {
						id: applicationId,
						awbId: awbId,
						withdrawCost: e.values.WithdrawCost
					}, awbId);
				}
				if (e.values.TransitCost !== undefined) {
					post($u.Calculation_SetTransitCost, {
						id: applicationId,
						awbId: awbId,
						transitCost: e.values.TransitCost
					}, awbId);
				}
				if (e.values.SenderRate !== undefined) {
					post($u.Calculation_SetSenderRate, {
						id: applicationId,
						awbId: awbId,
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