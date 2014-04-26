var Alicargo = (function($a) {
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
							"TotalTariffCost": { type: "number", editable: true },
							"TotalSenderRate": { type: "number", editable: false },
							"ScotchCost": { type: "number", editable: true },
							"FactureCost": { type: "number", editable: true },
							"FactureCostEx": { type: "number", editable: true },
							"PickupCost": { type: "number", editable: true },
							"TransitCost": { type: "number", editable: true },
							"InsuranceCost": { type: "number", editable: true },
							"Profit": { type: "number", editable: true }
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

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});