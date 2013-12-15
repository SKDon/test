$(function () {
	var $a = Alicargo;

	var schema = {
		model: {
			id: "Id"
		},
		data: "Data",
		total: "Total"
	};
	var gridSelector = "#AirWaybill-grid";
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
		pageSize: $a.SelectedPageSize(gridSelector),
		serverPaging: true,
		editable: true,
		error: $a.ShowError
	};

	var columns = $a.Awb.AddColumns();

	$a.CreateGrid(gridSelector, {
		dataSource: dataSource,		
		detailTemplate: kendo.template($("#AirWaybill-grid-details").html()),
		columns: columns		
	});
});
