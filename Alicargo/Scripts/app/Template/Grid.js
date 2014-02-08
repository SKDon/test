$(function() {
	var $a = Alicargo;

	$a.Templates = (function($t) {

		var $u = $a.Urls;
		var $l = $a.Localization;

		var dataSource = {
			transport: {
				read: {
					dataType: "json",
					url: $u.EmailTemplate_List,
					type: "POST",
					cache: false
				}
			},
			schema: { model: { id: "Id" } },
			error: $a.ShowError
		};

		$("#templates-grid").kendoGrid({
			dataSource: dataSource,
			pageable: false,
			columns: [
				{
					command: [{
						name: "custom-edit",
						text: "",
						click: function(e) {
							e.preventDefault();
							var tr = $(e.target).closest("tr");
							var data = this.dataItem(tr);
							var url = $u.EmailTemplate_Edit + "/" + data.Id;

							$a.LoadPage(url);
						}
					}],
					title: "&nbsp;",
					width: $a.DefaultGridButtonWidth
				}, { field: "Name", title: $l.Pages_Event }]
		});

		return $t;
	})($a.Templates || {});
});