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
				type: "POST",
				cache: false
			}
		},
		schema: schema,
		pageSize: $a.DefaultPageSize,
		serverPaging: true,
		editable: true,
		error: $a.ShowError
	};

	var columns = $a.Awb.AddColumns();

	$a.CreateGrid("#AirWaybill-grid", {
		resizable: true,
		dataSource: dataSource,
		pageable: $a.DefaultPageSizes,
		detailTemplate: kendo.template($("#AirWaybill-grid-details").html()),
		columns: columns
	});
});
