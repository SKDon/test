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
		pageable: $a.DefaultPageSizes,
		editable: false,
		columns: [
			{ field: "LegalEntity", title: $l.Entities_LegalEntity },
			{ field: "Nic", title: $l.Entities_Nic },
			{ field: "Email", title: $l.Entities_Email },
			{
				command: [{
					name: "custom-edit",
					text: "",
					click: function(e) {
						e.preventDefault();
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						window.location = $u.Client_Edit + "/" + data.Id;
					}
				}],
				title: "&nbsp;",
				width: Alicargo.DefaultGridButtonWidth
			}, {
				command: [{
					name: "custom-authenticate",
					text: "",
					click: function(e) {
						e.preventDefault();
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						if ($a.Confirm("Авторизаваться под клиентом " + data.Nic + "?")) {
							window.location = $u.Authentication_LoginAsUser + "/" + data.UserId;
						}
					}
				}],
				title: "&nbsp;",
				width: Alicargo.DefaultGridButtonWidth
			}]
	});
});