var Alicargo = (function($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;

	$a.Calculation = (function($c) {
		$c.DataSource = function() {
			return {
				schema: {
					total: "Total",
					groups: function(response) {
						$c.CalculationInfo = response.Info;
						return response.Groups;
					},
					model: {
						id: "ApplicationId",
						fields: {
							"DisplayNumber": { type: "string", editable: false },
							"Factory": { type: "string", editable: false },
							"Mark": { type: "string", editable: false },
							"Class": { type: "string", editable: true },
							"Count": { type: "number", editable: true },
							"Weight": { type: "number", editable: true },
							"Invoice": { type: "string", editable: false },
							"Value": { type: "number", editable: false },
							"TariffPerKg": { type: "number", editable: true },
							"SenderRate": { type: "number", editable: true },
							"TotalTariffCost": { type: "number", editable: false },
							"TotalSenderRate": { type: "number", editable: false },
							"ScotchCost": { type: "number", editable: true },
							"FactureCost": { type: "number", editable: true },
							"FactureCostEx": { type: "number", editable: true },
							"PickupCost": { type: "number", editable: true },
							"TransitCost": { type: "number", editable: true },
							"InsuranceCost": { type: "number", editable: true },
							"Profit": { type: "number", editable: false }
						}
					}
				},
				transport: {
					read: {
						dataType: "json",
						url: $u.Calculation_List,
						type: "POST",
						cache: false
					}
				},
				pageSize: $a.SelectedPageSize("#calculation-grid"),
				serverPaging: true,
				serverGrouping: true,
				serverAggregates: true,
				error: $a.ShowError,
				group: {
					field: "AirWaybillId",
					dir: "asc",
					aggregates: [
						{ field: "Count", aggregate: "sum" },
						{ field: "Weight", aggregate: "sum" },
						{ field: "Value", aggregate: "sum" },
						{ field: "TotalTariffCost", aggregate: "sum" },
						{ field: "TotalSenderRate", aggregate: "sum" },
						{ field: "TransitCost", aggregate: "sum" },
						{ field: "ScotchCost", aggregate: "sum" },
						{ field: "InsuranceCost", aggregate: "sum" },
						{ field: "Profit", aggregate: "sum" }]
				}
			};
		};

		$c.Columns = function() {
			var groupFooterTemplate = "#= kendo.toString(sum, 'n2') #";
			var n2Format = "{0:n2}";
			var textRight = { "class": "text-right" };
			var textBold = { "class": "text-bold" };

			var classType = {
				"null": "",
				"1": $l.Enums_Econom,
				"2": $l.Enums_Comfort,
				"3": $l.Enums_Lux
			};

			function classEditor(container, options) {
				$('<input required data-text-field="ClassName" data-value-field="ClassId" data-bind="value: ClassType"/>')
					.appendTo(container)
					.kendoDropDownList({
						autoBind: false,
						value: options.model.ClassId,
						dataSource: {
							type: "json",
							data: [
								{ ClassName: classType["null"], ClassId: "" },
								{ ClassName: classType["1"], ClassId: 1 },
								{ ClassName: classType["2"], ClassId: 2 },
								{ ClassName: classType["3"], ClassId: 3 }
							]
						}
					});
			}

			function onCalculate(e) {
				e.preventDefault();
				if ($a.Confirm($l.Pages_ConfrimCalculation)) {
					var button = $(e.target).closest("a")[0];
					var appId = $.data(button, "ApplicationId");
					var awbId = $.data(button, "AirWaybillId");
					$c.Post($u.Calculation_Calculate, { id: appId, awbId: awbId }, awbId);
				}
			}

			function onCancelCalculate(e) {
				e.preventDefault();
				if ($a.Confirm($l.Pages_ConfirmCancelCalculation)) {
					var button = $(e.target).closest("a")[0];
					var appId = $.data(button, "ApplicationId");
					var awbId = $.data(button, "AirWaybillId");
					$c.Post($u.Calculation_RemoveCalculatation, { id: appId, awbId: awbId }, awbId);
				}
			}

			function numberEditor(container, options) {
				$('<input name="' + options.field + '"/>')
					.appendTo(container)
					.kendoNumericTextBox({
						format: n2Format,
						decimals: 2,
						step: 0.5
					});
			}

			var c = [
				{
					field: "AirWaybillId",
					hidden: true,
					groupHeaderTemplate: function(data) {
						return $l.Entities_AWB + ": "
							+ "<a href='" + $u.AdminAwb_Edit + "/" + data.value.id + "'>"
							+ data.value.text + "</a>";
					}
				},
				{
					field: "ClientNic",
					title: $l.Pages_Client
				},
				{
					field: "DisplayNumber",
					title: $l.Entities_DisplayNumber,
					template: "<a href='" + $u.Application_Edit + "/#=ApplicationId#'>#= DisplayNumber #</a>"
				},
				{
					field: "Factory",
					title: $l.Entities_FactoryName
				},
				{
					field: "Mark",
					title: $l.Entities_Mark
				},
				{
					field: "ClassId",
					title: $l.Entities_Class,
					editor: classEditor,
					template: function(model) { return classType[model.ClassId]; }
				},
				{
					field: "Count",
					title: $l.Entities_Count,
					groupFooterTemplate: "#= sum #",
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "Weight",
					title: $l.Entities_Weight,
					groupFooterTemplate: groupFooterTemplate,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "SenderRate",
					title: $l.Entities_SenderRate,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight
				},
				{
					field: "TotalSenderRate",
					title: $l.Entities_TotalSenderRate,
					groupFooterTemplate: groupFooterTemplate,
					template: "<b>#= kendo.toString(TotalSenderRate, 'n2') #</b>",
					headerAttributes: textBold,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "Invoice",
					title: $l.Entities_Invoice
				},
				{
					field: "Value",
					title: $l.Entities_Value,
					template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #",
					groupFooterTemplate: groupFooterTemplate,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "TariffPerKg",
					title: $l.Entities_TariffPerKg,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight
				},
				{
					field: "TotalTariffCost",
					title: $l.Entities_TotalTariffCost,
					groupFooterTemplate: groupFooterTemplate,
					template: "<b>#= kendo.toString(TotalTariffCost, 'n2') #</b>",
					headerAttributes: textBold,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "ScotchCost",
					groupFooterTemplate: groupFooterTemplate,
					title: $l.Entities_ScotchCost,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "InsuranceCost",
					title: $l.Entities_Insurance,
					groupFooterTemplate: groupFooterTemplate,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "FactureCost",
					title: $l.Entities_FactureCost,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight
				},
				{
					field: "FactureCostEx",
					title: $l.Entities_FactureCostEx,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight
				},
				{
					field: "PickupCost",
					title: $l.Entities_PickupCost,
					format: n2Format,
					editor: numberEditor,
					attributes: textRight
				},
				{
					field: "TransitCost",
					title: $l.Entities_TransitCost,
					format: n2Format,
					editor: numberEditor,
					groupFooterTemplate: "#= kendo.toString(sum, 'n0') #",
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					field: "Profit",
					title: $l.Entities_Total,
					groupFooterTemplate: groupFooterTemplate,
					template: "<b>#= kendo.toString(Profit, 'n2') #</b>",
					headerAttributes: textBold,
					attributes: textRight,
					footerAttributes: textRight
				},
				{
					attributes: { "class": "cell-button" },
					command: [{
							name: "custom-gear",
							text: "&nbsp;",
							click: onCalculate
						}, {
							name: "custom-cancel",
							text: "&nbsp;",
							click: onCancelCalculate
						}],
					title: "&nbsp;",
					width: $a.DefaultGridButtonWidth
				}
			];

			$c.Columns = function() { return c; };

			return c;
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});