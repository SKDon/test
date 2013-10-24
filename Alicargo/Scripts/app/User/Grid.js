$(function() {
	var $a = Alicargo;

	$a.Users = (function($usr) {

		var $u = $a.Urls;
		var $l = $a.Localization;

		$usr.Init = function(role) {
			var dataSource = {
				transport: {
					read: {
						dataType: "json",
						url: $u.User_List + "/" + role,
						type: "POST",
						cache: false
					}
				},
				schema: { model: { id: "Id" } },
				error: $a.ShowError
			};

			$("#user-grid").kendoGrid({
				dataSource: dataSource,
				columns: [
					{ field: "Name", title: $l.Pages_Name },
					{
						command: [{
							name: "custom-edit",
							text: "",
							click: function(e) {
								var tr = $(e.target).closest("tr");
								var data = this.dataItem(tr);
								var url = role == "Sender"
									? $u.Sender_Edit + "/" + data.Id
									: $u.User_Edit + "/" + role + "/" + data.Id;
								window.location = url;
							}
						}], title: "&nbsp;", width: $a.DefaultGridButtonWidth
					}]
			});
		};

		return $usr;
	})($a.Users || {});
});