$(function() {
	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

	var schema = {
		model: {
			id: "ClientId"
		},
		data: "Data",
		total: "Total"
	};

	var dataSource = {
		transport: {
			read: {
				dataType: "json",
				url: $u.RegistryOfPayments_List,
				type: "POST",
				cache: false
			}
		},
		schema: schema,
		pageSize: $a.SelectedPageSize("#payment-grid"),
		serverPaging: false,
		error: Alicargo.ShowError
	};

	$a.CreateGrid("#payment-grid", {
		dataSource: dataSource,
		filterable: false,
		sortable: false,
		editable: false,
		columns: [
			{ field: "ClientNic", title: $l.Entities_Nic },
			{ field: "Timestamp", title: $l.Entities_Date },
			{ field: "EventType", title: $l.Pages_Event },
			{ field: "Money", title: $l.Entities_Sum },
			{ field: "Balance", title: $l.Entities_Balance },
			{ field: "Comment", title: $l.Entities_Comment }]
	});
});