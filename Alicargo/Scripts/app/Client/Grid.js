$(function () {
	var schema = {
		model: {
			id: "Id"
		},
		data: "Data",
		total: "Total"
	};

	var error = function () { alert(Alicargo.Localization.Pages_AnError); };

	var dataSource = {
		transport: {
			read: {
				dataType: "json",
				url: Alicargo.Urls.Client_List,
				type: "POST"
			}
		},
		schema: schema,
		pageSize: 20,
		serverPaging: true,
		editable: true,
		error: error
	};

	$("#client-grid").kendoGrid({
		dataSource: dataSource,
		filterable: false,
		sortable: false,
		pageable: true,
		editable: false,
		columns: [
			{ field: "LegalEntity", title: Alicargo.Localization.Entities_LegalEntity },
			{ field: "Nic", title: Alicargo.Localization.Entities_Nic },
		{ field: "Email", title: Alicargo.Localization.Entities_Email },
			{
				command: [{
					name: "custom-edit",
					text: "",
					click: function (e) {
						var tr = $(e.target).closest("tr");
						var data = this.dataItem(tr);
						var url = Alicargo.Urls.Client_Edit + "/" + data.Id;
						window.location = url;
					}
				}], title: "&nbsp;", width: "53px"
			}]
	});
});