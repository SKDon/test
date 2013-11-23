$(function() {
	var $a = Alicargo;

	$a.States = (function($states) {

		var $u = $a.Urls;

		$(function() {
			var stateId = $("#Id").val();

			$("#Language").change(function(e) {
				e.preventDefault();
				var lang = $(e.target).val();
				var url = $u.State_Edit + "/" + stateId + "?lang=" + lang;
				
				$a.LoadPage(url);
			});

		});

		return $states;
	})($a.States || {});
});