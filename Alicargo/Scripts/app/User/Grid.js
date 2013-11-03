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
				pageable: false,
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
									? $u.Sender_Edit + "/" + data.EntityId
									: $u.User_Edit + "/" + role + "/" + data.EntityId;
								window.location = url;
							}
						}], title: "&nbsp;", width: $a.DefaultGridButtonWidth
					}, {
						command: [{
							name: "custom-authenticate",
							text: "",
							click: function(e) {
								e.preventDefault();
								var tr = $(e.target).closest("tr");
								var data = this.dataItem(tr);
								if ($a.Confirm("Авторизаваться под пользователем " + data.Name + "?")) {
									window.location = $u.Authentication_LoginAsUser + "/" + data.UserId;
								}
							}
						}],
						title: "&nbsp;",
						width: Alicargo.DefaultGridButtonWidth
					}]
			});
		};

		return $usr;
	})($a.Users || {});
});