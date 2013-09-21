var Alicargo = (function ($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;
	var $r = $a.Roles;

	$a.Calculation = (function ($c) {
		$c.DataSource = function () {
			var editTariffPerKg = $r.IsAdmin;
			var editScotchCost = $r.IsAdmin || $r.IsSender;
			var editFactureCost = $r.IsAdmin || $r.IsSender;
			var editWithdrawCost = $r.IsAdmin || $r.IsSender;
			var editTransitCost = $r.IsAdmin || $r.IsForwarder;

			return {
				schema: {
					total: "Total",
					data: "Data",
					model: {
						id: "ApplicationId",
						fields: {
							"ClientNic": { type: "string", editable: false },
							"AwbDisplay": { type: "string", editable: false },
							"DisplayNumber": { type: "string", editable: false },
							"Factory": { type: "string", editable: false },
							"Mark": { type: "string", editable: false },
							"Count": { type: "number", editable: false },
							"Weigth": { type: "number", editable: false },
							"Invoice": { type: "string", editable: false },
							"Value": { type: "number", editable: false },
							"TariffPerKg": { type: "number", editable: editTariffPerKg },
							"TotalTariffCost": { type: "number", editable: false },
							"ScotchCost": { type: "number", editable: editScotchCost },
							"FactureCost": { type: "number", editable: editFactureCost },
							"WithdrawCost": { type: "number", editable: editWithdrawCost },
							"TransitCost": { type: "number", editable: editTransitCost },
							"InsuranceCost": { type: "number", editable: false },
							"Profit": { type: "number", editable: false }
						}
					}
				},
				transport: {
					read: {
						dataType: "json",
						url: $u.Calculation_List,
						type: "POST"
					}
				},
				pageSize: 3,
				serverPaging: true,
				serverGrouping: false,
				error: $a.ShowError,
				group: { field: "AwbDisplay", dir: "asc" },
				aggregate: [
					{ field: "Count", aggregate: "sum" },
					{ field: "Weigth", aggregate: "sum" },
					{ field: "Value", aggregate: "sum" },
					{ field: "TotalTariffCost", aggregate: "sum" },
					{ field: "ScotchCost", aggregate: "sum" },
					{ field: "FactureCost", aggregate: "sum" },
					{ field: "WithdrawCost", aggregate: "sum" },
					{ field: "TransitCost", aggregate: "sum" },
					{ field: "InsuranceCost", aggregate: "sum" },
					{ field: "Profit", aggregate: "sum" }]
			};
		};

		$c.Columns = function () {
			var c = [
				{ field: "AwbDisplay", title: $l.Entities_AirWayBill, groupable: true, hidden: true },
				{ field: "ClientNic", title: $l.Pages_Client },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Count", title: $l.Entities_Count, footerTemplate: "#= sum #" },
				{ field: "Weigth", title: $l.Entities_Weigth, footerTemplate: "#= kendo.toString(sum, 'n2') #", format: "{0:n2}" },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg, format: "{0:n2}" },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "ScotchCost", title: $l.Entities_ScotchCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "FactureCost", title: $l.Entities_FactureCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "TransitCost", title: $l.Entities_TransitCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= InsuranceCost != null ? kendo.toString(InsuranceCost, 'n2') : 0 + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "Profit", title: $l.Entities_Profit, footerTemplate: "#= kendo.toString(sum, 'n2') #" }
			];
			if ($r.IsAdmin) {
				c.push({
					command: [{
						name: "calculate",
						text: $l.Pages_Calculate,
						click: function (e) {
							var tr = $(e.target).closest("tr");
							var trData = this.dataItem(tr);
							alert("implement " + trData.ApplicationId);
						}
					}],
					title: "&nbsp;"
				});
			}

			return c;
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});