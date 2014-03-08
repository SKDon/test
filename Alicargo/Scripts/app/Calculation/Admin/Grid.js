var Alicargo = (function($a) {

	var $u = $a.Urls;

	var select = $("#client-balance-select");
	var updateBalanceButtons = function() {
		var decrease = $("#client-balance-decrease-link");
		var increase = $("#client-balance-increase-link");
		var urls = select.val().split(";");
		decrease.attr("href", urls[0]);
		increase.attr("href", urls[1]);
	};
	select.change(updateBalanceButtons);
	updateBalanceButtons();

	$a.Calculation = (function($c) {

		$c.GetMainGrid = function() {
			var grid = $("#calculation-grid").data("kendoGrid");
			$c.GetMainGrid = function() { return grid; };
			return grid;
		};

		function rebindData(oldData, data) {
			var aggregates = data.Groups[0].aggregates;
			for (var i in aggregates) {
				oldData.aggregates[i].sum = aggregates[i].sum;
			}

			var items = data.Groups[0].items;
			for (var itemIndex in items) {
				$.extend(oldData.items[itemIndex], items[itemIndex]);
			}

			for (var infoIndex in $c.CalculationInfo) {
				if ($c.CalculationInfo[infoIndex].AirWaybillId == data.Info[0].AirWaybillId) {
					$.extend($c.CalculationInfo[infoIndex], data.Info[0]);
					break;
				}
			}
		}

		function post(url, data, awbId) {
			var scrollTop = $(".k-grid-content").scrollTop();
			$.post(url, data).success(function(awbData) {
				updateMailGrid(awbId, awbData);
				$("#total-balance").text(awbData.TotalBalance);
				$(".k-grid-content").scrollTop(scrollTop);
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
				change: function() {
					post($u.Calculation_SetAdditionalCost, {
						awbId: data.AirWaybillId,
						additionalCost: this.value()
					}, data.AirWaybillId);
				}
			});
		}

		$(function() {
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

				$("tr a.k-grid-custom-gear").each(function() {
					var button = $(this);
					var dataItem = $c.GetMainGrid().dataItem(button.closest("tr"));
					$.data(button[0], "ApplicationId", dataItem.ApplicationId);
					$.data(button[0], "AirWaybillId", dataItem.AirWaybillId);
					if (dataItem.IsCalculated) {
						button.remove();
					}
				});

				$("tr a.k-grid-custom-cancel").each(function() {
					var button = $(this);
					var dataItem = $c.GetMainGrid().dataItem(button.closest("tr"));
					$.data(button[0], "ApplicationId", dataItem.ApplicationId);
					$.data(button[0], "AirWaybillId", dataItem.AirWaybillId);
					if (!dataItem.IsCalculated) {
						button.remove();
					}
				});

				$("tr.k-group-footer").each(function(i) {
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
				
				function numberConverter(value) {
					if (!!!value) {
						return null;
					}
					
					// todo: fix hack
					if (Globalize.culture().name == 'en') {
						return value.toFixed(2);
					}
					
					return value.toFixed(2).replace(".", ","); 
				}

				var settings = {
					ClassType: {
						Url: $u.Calculation_SetClass,
						ArgumentName: "classId",
						Convert: function(classType) {
							return classType == null ? null : classType.ClassId;
						}
					},
					TariffPerKg: {
						Url: $u.Calculation_SetTariffPerKg,
						ArgumentName: "tariffPerKg",
						Convert: numberConverter
					},
					ScotchCost: {
						Url: $u.Calculation_SetScotchCostEdited,
						ArgumentName: "scotchCost",
						Convert: numberConverter
					},
					FactureCost: {
						Url: $u.Calculation_SetFactureCostEdited,
						ArgumentName: "factureCost",
						Convert: numberConverter
					},
					FactureCostEx: {
						Url: $u.Calculation_SetFactureCostExEdited,
						ArgumentName: "factureCostEx",
						Convert: numberConverter
					},
					PickupCost: {
						Url: $u.Calculation_SetPickupCostEdited,
						ArgumentName: "pickupCost",
						Convert: numberConverter
					},
					TransitCost: {
						Url: $u.Calculation_SetTransitCostEdited,
						ArgumentName: "transitCost",
						Convert: numberConverter
					},
					SenderRate: {
						Url: $u.Calculation_SetSenderRate,
						ArgumentName: "senderRate",
						Convert: numberConverter
					},
					Count: {
						Url: $u.Calculation_SetCount,
						ArgumentName: "value"
					},
					Weight: {
						Url: $u.Calculation_SetWeight,
						ArgumentName: "value",
						Convert: numberConverter
					},
					InsuranceCost: {
						Url: $u.Calculation_SetInsuranceCost,
						ArgumentName: "value",
						Convert: numberConverter
					},
					TotalTariffCost: {
						Url: $u.Calculation_SetTotalTariffCost,
						ArgumentName: "value",
						Convert: numberConverter
					},
					Profit: {
						Url: $u.Calculation_SetProfit,
						ArgumentName: "value",
						Convert: numberConverter
					}
				};

				var data = {
					id: applicationId,
					awbId: awbId
				};

				for (var fieldName in e.values) {
					if (e.values[fieldName] !== undefined) {
						var setting = settings[fieldName];
						data[setting.ArgumentName] = setting.Convert
							? setting.Convert(e.values[fieldName])
							: e.values[fieldName];
						post(setting.Url, data, awbId);

						return;
					}
				}
			}

			function edit(e) {
				if (e.model.IsCalculated) {
					$c.GetMainGrid().closeCell();
				}
			}

			$a.CreateGrid("#calculation-grid", {
				columns: $c.Columns(),
				dataSource: $c.DataSource(),
				editable: true,
				save: save,
				dataBound: dataBound,
				edit: edit
			});
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});