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
				url: $u.Country_List,
				type: "POST",
				cache: false
			}
		},
		schema: schema,
		pageSize: $a.SelectedPageSize("#country-grid"),
		serverPaging: true,
		error: $a.ShowError
	};

	var settings = {
		dataSource: dataSource,
		columns: [
			{ field: "Name", title: $l.Pages_Country }, {
				command: [{
					name: "custom-edit",
					text: "",
					click: function(e) {
						e.preventDefault();
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						var url = $u.Country_Edit + "/" + data.Id;

						$a.LoadPage(url);
					}
				}], title: "&nbsp;", width: $a.DefaultGridButtonWidth
			}]
	};

	$a.CreateGrid("#country-grid", settings);
});