var Alicargo = (function ($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;
	var $r = $a.Roles;

	$a.Calculation = (function ($c) {
		$c.DataSource = function () {
			var editTariffPerKg = $r.IsAdmin;
			var editSenderRate = $r.IsAdmin;
			var editScotchCost = $r.IsAdmin || $r.IsSender;
			var editFactureCost = $r.IsAdmin || $r.IsSender;
			var editWithdrawCost = $r.IsAdmin || $r.IsSender;
			var editTransitCost = $r.IsAdmin || $r.IsForwarder;

			return {
				schema: {
					total: "Total",
					groups:function (response) {
						$c.CalculationInfo = response.Info;
						return response.Groups;
					},
					model: {
						id: "ApplicationId",
						fields: {
							"ClientNic": { type: "string", editable: false },
							"DisplayNumber": { type: "string", editable: false },
							"Factory": { type: "string", editable: false },
							"Mark": { type: "string", editable: false },
							"Count": { type: "number", editable: false },
							"Weigth": { type: "number", editable: false },
							"Invoice": { type: "string", editable: false },
							"Value": { type: "number", editable: false },
							"TariffPerKg": { type: "number", editable: editTariffPerKg },
							"SenderRate": { type: "number", editable: editSenderRate },
							"TotalTariffCost": { type: "number", editable: false },
							"TotalSenderRate": { type: "number", editable: false },
							"ScotchCost": { type: "number", editable: editScotchCost },
							"FactureCost": { type: "number", editable: editFactureCost },
							"WithdrawCost": { type: "number", editable: editWithdrawCost },
							"TransitCost": { type: "number", editable: editTransitCost },
							"InsuranceCost": { type: "number", editable: false },
							"ForwarderCost": { type: "number", editable: false },
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
				pageSize: 5,
				serverPaging: true,
				serverGrouping: true,
				serverAggregates: false,
				error: $a.ShowError,
				group: {
					field: "AirWaybillId", dir: "asc", aggregates: [
						{ field: "Count", aggregate: "sum" },
						{ field: "Weigth", aggregate: "sum" },
						{ field: "Value", aggregate: "sum" },
						{ field: "TotalTariffCost", aggregate: "sum" },
						{ field: "TotalSenderRate", aggregate: "sum" },
						{ field: "TransitCost", aggregate: "sum" },
						{ field: "ScotchCost", aggregate: "sum" },
						{ field: "ForwarderCost", aggregate: "sum" },
						{ field: "InsuranceCost", aggregate: "sum" },
						{ field: "Profit", aggregate: "sum" }]
				}
			};
		};

		$c.Columns = function () {
			var groupFooterTemplate = "#= kendo.toString(sum, 'n2') #";
			var n2Format = "{0:n2}";
			var c = [
				{ field: "ClientNic", title: $l.Pages_Client },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Count", title: $l.Entities_Count, groupFooterTemplate: "#= sum #" },
				{ field: "Weigth", title: $l.Entities_Weigth, groupFooterTemplate: groupFooterTemplate, format: n2Format },
				{ field: "SenderRate", title: $l.Entities_SenderRate, format: n2Format },
				{ field: "TotalSenderRate", title: $l.Entities_TotalSenderRate, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(TotalSenderRate, 'n2') #</b>" },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", groupFooterTemplate: groupFooterTemplate },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg, format: n2Format },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(TotalTariffCost, 'n2') #</b>" },
				{ field: "ScotchCost", groupFooterTemplate: groupFooterTemplate, title: $l.Entities_ScotchCost },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= InsuranceCost != null ? kendo.toString(InsuranceCost, 'n2') : 0 + CurrencyType[ValueCurrencyId] #", groupFooterTemplate: groupFooterTemplate },
				{ field: "FactureCost", title: $l.Entities_FactureCost },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost },
				{ field: "TransitCost", title: $l.Entities_TransitCost, groupFooterTemplate: "#= kendo.toString(sum, 'n0') #" },				
				{ field: "ForwarderCost", title: $l.Entities_ForwarderCost, groupFooterTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "Profit", title: $l.Entities_Total, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(Profit, 'n2') #</b>" }
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

			$c.Columns = function () { return c; };

			return c;
		};		

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});