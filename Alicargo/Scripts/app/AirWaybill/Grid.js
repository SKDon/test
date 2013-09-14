$(function () {
	var $a = Alicargo;
	
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
				url: $a.Urls.AirWaybill_List,
				type: "POST"
			}
		},
		schema: schema,
		pageSize: 20,
		serverPaging: true,
		editable: true,
		error: $a.ShowError
	};

	var columns = $a.Awb.AddColumns();

	$("#AirWaybill-grid").kendoGrid({
		resizable: true,
		dataSource: dataSource,
		pageable: { refresh: true, pageSizes: [10, 20, 50, 100] },
		detailTemplate: kendo.template($("#AirWaybill-grid-details").html()),
		columns: columns
	});
});
