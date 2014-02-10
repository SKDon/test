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

			function getUrl(data) {
				switch (role) {
				case "Sender":
					return $u.Sender_Edit + "/" + data.EntityId;
				case "Forwarder":
					return $u.Forwarder_Edit + "/" + data.EntityId;
				case "Carrier":
					return $u.Carrier_Edit + "/" + data.EntityId;
				default:
					return $u.User_Edit + "/" + role + "/" + data.EntityId;
				}
			}

			function onEdit(e) {
				var tr = $(e.target).closest("tr");
				var data = this.dataItem(tr);
				var url = getUrl(data);

				$a.LoadPage(url);
			}

			function onAuthenticate(e) {
				e.preventDefault();
				var tr = $(e.target).closest("tr");
				var data = this.dataItem(tr);
				if ($a.Confirm("Авторизаваться под пользователем " + data.Name + "?")) {
					var url = $u.Authentication_LoginAsUser + "/" + data.UserId;

					$a.LoadPage(url);
				}
			}

			$("#user-grid").kendoGrid({
				dataSource: dataSource,
				pageable: false,
				columns: [{
						command: [{
							name: "custom-authenticate",
							text: "",
							click: onAuthenticate
						}],
						title: "&nbsp;",
						width: Alicargo.DefaultGridButtonWidth
					}, {
						command: [{
							name: "custom-edit",
							text: "",
							click: onEdit
						}],
						title: "&nbsp;",
						width: $a.DefaultGridButtonWidth
					},
					{ field: "Name", title: $l.Pages_Name }]
			});
		};

		return $usr;
	})($a.Users || {});
});