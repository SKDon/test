$(function() {

	var $a = Alicargo;
	var $urls = $a.Urls;
	var $l = $a.Localization;

	var gridHolder = $("#calculation-grid");
	var dataSource = {
		transport: {
			read: {
				dataType: "json",
				url: $urls.Calculation_List,
				type: "POST"
			}
		},
		schema: { model: { id: "Id" } },
		pageSize: 20,
		serverPaging: true,
		error: $a.ShowError
	};

	var grid = gridHolder.kendoGrid({
		columns: [{ field: "AwbDisplay" }],
		pageable: true,
		editable: false,
		dataSource: dataSource,
		detailInit: detailInit,
		detailTemplate: kendo.template($("#calculation-grid-details-template").html()),
	}).data("kendoGrid");

	gridHolder.children(".k-grid-header").hide();

	function detailInit(d) {
		var detailRow = d.detailRow;
		var dataItem = grid.dataItem(detailRow);

		detailRow.find(".calculation-rows").kendoGrid({
			dataSource: {
				data: dataItem.Rows,
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
					{ field: "TotalCost", aggregate: "sum" }]
			},
			scrollable: false,
			resizable: true,
			columns: [
				{ field: "ClientNic", title: $l.Entities_Nic },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Count", title: $l.Entities_Count, footerTemplate: "#= sum #" },
				{ field: "Weigth", title: $l.Entities_Weigth, footerTemplate: "#= sum #" },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= Value.toString() + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= sum #" },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, footerTemplate: "#= sum #" },
				{ field: "ScotchCost", title: $l.Entities_ScotchCost, footerTemplate: "#= sum #" },
				{ field: "FactureCost", title: $l.Entities_FactureCost, footerTemplate: "#= sum #" },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost, footerTemplate: "#= sum #" },
				{ field: "TransitCost", title: $l.Entities_TransitCost, footerTemplate: "#= sum #" },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= InsuranceCost.toString() + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= sum #" },
				{ field: "TotalCost", title: $l.Entities_Total, footerTemplate: "#= sum #" },
				{
					command: [{
						name: "calculate",
						text: $l.Pages_Calculate,
						click: function(e) {
							var tr = $(e.target).closest("tr");
							var data = this.dataItem(tr);
							alert("implement " + data.ApplicationId);
						}
					}],
					title: "&nbsp;"
				}
			]
		});
	}
});