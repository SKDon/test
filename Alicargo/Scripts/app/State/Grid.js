$(function() {
	var $a = Alicargo;

	$a.States = (function($states) {

		var $u = $a.Urls;
		var $l = $a.Localization;

		var dataSource = {
			transport: {
				read: {
					dataType: "json",
					url: $u.State_List,
					type: "POST",
					cache: false
				}
			},
			schema: { model: { id: "Id" } },
			error: $a.ShowError
		};

		$("#state-grid").kendoGrid({
			dataSource: dataSource,
			pageable: false,
			columns: [
				{ field: "Name", title: $l.Pages_Name },
				{
					command: [{
						name: "custom-edit",
						text: "",
						click: function(e) {
							e.preventDefault();
							var tr = $(e.target).closest("tr");
							var data = this.dataItem(tr);
							var url = $u.State_Edit + "/" + data.Id;
							
							$a.LoadPage(url);
						}
					}], title: "&nbsp;", width: $a.DefaultGridButtonWidth
				}]
		});

		return $states;
	})($a.States || {});
});