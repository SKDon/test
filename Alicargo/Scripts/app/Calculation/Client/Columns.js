var Alicargo = (function($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;

	$a.Calculation = (function($c) {
		$c.DataSource = function() {
			return {
				schema: {
					total: "Total",
					groups: "Groups",
					model: { id: "ApplicationId" }
				},
				transport: {
					read: {
						dataType: "json",
						url: $u.ClientCalculation_List,
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
						{ field: "TotalTariffCost", aggregate: "sum" },
						{ field: "TransitCost", aggregate: "sum" },
						{ field: "ScotchCost", aggregate: "sum" },
						{ field: "InsuranceCost", aggregate: "sum" },
						{ field: "Profit", aggregate: "sum" }
					]
				}
			};
		};

		$c.Columns = function() {
			var groupFooterTemplate = "#= kendo.toString(sum, 'n2') #";
			var n2Format = "{0:n2}";
			var texRight = { "class": "text-right" };
			var c = [
				{ field: "ClientNic", title: $l.Pages_Client },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "ClassName", title: $l.Entities_Class },
				{ field: "Count", title: $l.Entities_Count, attributes: texRight },
				{ field: "Weigth", title: $l.Entities_Weigth, format: n2Format, attributes: texRight },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", attributes: texRight },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg, format: n2Format, attributes: texRight },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, template: "<b>#= kendo.toString(TotalTariffCost, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: texRight },
				{ field: "ScotchCost", title: $l.Entities_ScotchCost, attributes: texRight },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= kendo.toString(InsuranceCost, 'n2') + CurrencyType[ValueCurrencyId] #", attributes: texRight },
				{ field: "FactureCost", title: $l.Entities_FactureCost, attributes: texRight },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost, attributes: texRight },
				{ field: "TransitCost", title: $l.Entities_TransitCost, attributes: texRight },
				{ field: "Profit", title: $l.Entities_Total, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(Profit, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: texRight, footerAttributes: texRight }
			];

			$c.Columns = function() { return c; };

			return c;
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});