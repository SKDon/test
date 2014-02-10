$(function() {
	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

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
				url: $u.Client_List,
				type: "POST",
				cache: false
			}
		},
		schema: schema,
		pageSize: $a.SelectedPageSize("#client-grid"),
		serverPaging: true,
		editable: true,
		error: Alicargo.ShowError
	};

	$a.CreateGrid("#client-grid", {
		dataSource: dataSource,
		filterable: false,
		sortable: false,
		editable: false,
		columns: [{
				command: [{
					name: "custom-authenticate",
					text: "",
					click: function(e) {
						e.preventDefault();
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						if ($a.Confirm("Авторизаваться под клиентом " + data.Nic + "?")) {
							var url = $u.Authentication_LoginAsClient + "/" + data.ClientId;

							$a.LoadPage(url);
						}
					}
				}],
				title: "&nbsp;",
				width: Alicargo.DefaultGridButtonWidth
			}, {
				command: [{
					name: "custom-edit",
					text: "",
					click: function(e) {
						e.preventDefault();
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						var url = $u.Client_Edit + "/" + data.ClientId;

						$a.LoadPage(url);
					}
				}],
				title: "&nbsp;",
				width: Alicargo.DefaultGridButtonWidth
			},
			{ field: "Nic", title: $l.Entities_Nic },
			{ field: "LegalEntity", title: $l.Entities_LegalEntity }]
	});
});