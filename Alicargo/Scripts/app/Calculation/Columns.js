var Alicargo = (function($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;
	var $r = $a.Roles;

	$a.Calculation = (function($c) {
		$c.DataSource = function() {
			var editTariffPerKg = $r.IsAdmin;
			var editSenderRate = $r.IsAdmin;
			var editScotchCost = $r.IsAdmin || $r.IsSender;
			var editFactureCost = $r.IsAdmin || $r.IsSender;
			var editWithdrawCost = $r.IsAdmin || $r.IsSender;
			var editTransitCost = $r.IsAdmin || $r.IsForwarder;

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
						type: "POST",
						cache: false
					}
				},
				pageSize: 5,
				serverPaging: true,
				serverGrouping: true,
				serverAggregates: false,
				error: $a.ShowError,
				group: {
					field: "AirWaybillId",
					dir: "asc",
					aggregates: [
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

		$c.Columns = function() {
			var groupFooterTemplate = "#= kendo.toString(sum, 'n2') #";
			var n2Format = "{0:n2}";
			var c = [
				{ field: "ClientNic", title: $l.Pages_Client },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Count", title: $l.Entities_Count, groupFooterTemplate: "#= sum #", attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "Weigth", title: $l.Entities_Weigth, groupFooterTemplate: groupFooterTemplate, format: n2Format, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "SenderRate", title: $l.Entities_SenderRate, format: n2Format, attributes: { "class": "text-right" } },
				{ field: "TotalSenderRate", title: $l.Entities_TotalSenderRate, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(TotalSenderRate, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", groupFooterTemplate: groupFooterTemplate, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg, format: n2Format, attributes: { "class": "text-right" } },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(TotalTariffCost, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "ScotchCost", groupFooterTemplate: groupFooterTemplate, title: $l.Entities_ScotchCost, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= kendo.toString(InsuranceCost, 'n2') + CurrencyType[ValueCurrencyId] #", groupFooterTemplate: groupFooterTemplate, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "FactureCost", title: $l.Entities_FactureCost, attributes: { "class": "text-right" } },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost, attributes: { "class": "text-right" } },
				{ field: "TransitCost", title: $l.Entities_TransitCost, groupFooterTemplate: "#= kendo.toString(sum, 'n0') #", attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "ForwarderCost", title: $l.Entities_ForwarderCost, groupFooterTemplate: "#= kendo.toString(sum, 'n0') #", attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } },
				{ field: "Profit", title: $l.Entities_Total, groupFooterTemplate: groupFooterTemplate, template: "<b>#= kendo.toString(Profit, 'n2') #</b>", headerAttributes: { "class": "text-bold" }, attributes: { "class": "text-right" }, footerAttributes: { "class": "text-right" } }
			];


			if ($r.IsAdmin) {
				c.push({
					command: [{
						name: "custom-gear",
						text: "&nbsp;",
						title: $l.Pages_Calculate,
						click: function (e) {
							e.preventDefault();
							var button = $(e.target).closest("a")[0];
							var appId = $.data(button, "ApplicationId");
							var awbId = $.data(button, "AirWaybillId");
							$c.Post($u.Calculation_Calculate, { id: appId, awbId: awbId }, awbId);
						}
					}],
					title: "&nbsp;",
					width: $a.DefaultGridButtonWidth
				});
			}

			$c.Columns = function() { return c; };

			return c;
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});