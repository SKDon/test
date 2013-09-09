var Alicargo = (function($a) {

	$a.GetGrid = function() {
		var grid = $("#AirWaybill-grid").data("kendoGrid");
		$a.GetGrid = function () { return grid; };
		
		return $a.GetGrid();
	};

	$(function () {
		var schema = {
			model: {
				id: "Id"
			},
			data: "Data",
			total: "Total"
		};

		var dataSource = {
			transport: {
				read: {
					dataType: "json",
					url: window.Alicargo.Urls.AirWaybill_List,
					type: "POST"
				}
			},
			schema: schema,
			pageSize: 20,
			serverPaging: true,
			editable: true,
			error: $a.ShowError
		};

		var columns = $a.AddColumns();

		$("#AirWaybill-grid").kendoGrid({
			resizable: true,
			dataSource: dataSource,
			pageable: { refresh: true, pageSizes: [10, 20, 50, 100] },
			detailTemplate: kendo.template($("#AirWaybill-grid-details").html()),
			columns: columns
		});
	});

	return $a;
}(Alicargo || {}));