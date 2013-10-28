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
					model: { id: "ApplicationId" }
				},
				transport: {
					read: {
						dataType: "json",
						url: $u.SenderCalculation_List,
						type: "POST",
						cache: false
					}
				},
				pageSize: 20,
				serverPaging: true,
				serverGrouping: true,
				error: $a.ShowError,
				group: {
					field: "AirWaybillId",
					dir: "asc",
					aggregates: [
						{ field: "Count", aggregate: "sum" },
						{ field: "Weigth", aggregate: "sum" },
						{ field: "Value", aggregate: "sum" },
						{ field: "SenderScotchCost", aggregate: "sum" },
						{ field: "TotalSenderRate", aggregate: "sum" },
						{ field: "FactureCost", aggregate: "sum" },
						{ field: "PickupCost", aggregate: "sum" },
						{ field: "Profit", aggregate: "sum" }
					]
				}
			};
		};

		$c.Columns = function() {
			var groupFooterTemplate = "#= kendo.toString(sum, 'n2') #";
			var groupFooterTemplate0 = "#= kendo.toString(sum, 'n0') #";
			var n2Format = "{0:n2}";
			var textRight = { "class": "text-right" };
			var textBold = { "class": "text-bold" };
			var c = [
				{
					field: "AirWaybillId",
					hidden: true,
					groupHeaderTemplate: function(data) {
						return "<a href='" + $u.SenderAwb_Edit + "/" + data.value.id + "'>" + $l.Entities_AWB
							+ ": " + data.value.text + "</a>";
					}
				},
				{ field: "ClientNic", title: $l.Pages_Client },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, template: "<a href='" + $u.SenderApplication_Edit + "/#=ApplicationId#'>#= DisplayNumber #</a>" },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", attributes: textRight, groupFooterTemplate: groupFooterTemplate, footerAttributes: textRight },
				{ field: "Count", title: $l.Entities_Count, attributes: textRight, groupFooterTemplate: groupFooterTemplate0, footerAttributes: textRight },
				{ field: "Weigth", title: $l.Entities_Weigth, format: n2Format, attributes: textRight, groupFooterTemplate: groupFooterTemplate0, footerAttributes: textRight },
				{ field: "SenderRate", title: $l.Entities_SenderRate, format: n2Format, attributes: textRight },
				{ field: "TotalSenderRate", title: $l.Entities_TotalSenderRate, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(TotalSenderRate, 'n2') #</b>", headerAttributes: textBold, attributes: textRight, footerAttributes: textRight },
				{ field: "SenderScotchCost", title: $l.Entities_ScotchCost, attributes: textRight, format: n2Format, groupFooterTemplate: groupFooterTemplate, footerAttributes: textRight },
				{ field: "FactureCost", title: $l.Entities_FactureCost, attributes: textRight, format: n2Format, groupFooterTemplate: groupFooterTemplate, footerAttributes: textRight },
				{ field: "PickupCost", title: $l.Entities_PickupCost, attributes: textRight, format: n2Format, groupFooterTemplate: groupFooterTemplate, footerAttributes: textRight },
				{ field: "Profit", title: $l.Entities_Total, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(Profit, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: textRight, footerAttributes: textRight }
			];

			$c.Columns = function() { return c; };

			return c;
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});