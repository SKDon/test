$(function () {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $c = $a.Calculation;

	var gridHolder = $("#calculation-grid");
	
	var grid = gridHolder.kendoGrid({
		columns: [{ field: "AwbDisplay", template: "<b>#: AwbDisplay # </b>" }],
		pageable: true,
		editable: false,
		dataSource: {
			transport: {
				read: {
					dataType: "json",
					url: $u.Calculation_List,
					type: "POST"
				}
			},
			schema: { model: { id: "Id" } },
			pageSize: 20,
			serverPaging: true,
			error: $a.ShowError
		},
		detailInit: detailInit,
		dataBound: function () {
			this.expandRow(this.tbody.find("tr.k-master-row"));
		},
		detailTemplate: kendo.template($("#calculation-grid-details-template").html()),
	}).data("kendoGrid");	

	gridHolder.children(".k-grid-header").hide();

	function detailInit(r) {
		var data = grid.dataItem(r.masterRow);

		$c.InitDetails(r.detailRow, data);
	}
});