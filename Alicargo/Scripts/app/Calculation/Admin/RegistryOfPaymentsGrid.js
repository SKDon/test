$(function() {
	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

	var schema = {
		model: {
			id: "Timestamp"
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
			{ field: "ClientNic", title: $l.Entities_Nic, width: '150px' },
			{ field: "Timestamp", title: $l.Entities_Date, width: '70px' },
			{ field: "EventType", title: $l.Pages_Event, width: '130px' },
			{ field: "Money", title: $l.Entities_Sum, width: '100px', format: '{0:n2}' },
			{ field: "Balance", title: $l.Entities_Balance, width: '100px', format: '{0:n2}' },
			{
				field: "Comment", title: $l.Entities_Comment, attributes: {
					"style": "white-space: normal;"
				}
			}]
	});
});