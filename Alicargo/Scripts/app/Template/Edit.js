$(function() {
	var $a = Alicargo;
	var $u = $a.Urls;

	var id = $("#EventType").val();

	$("#Language").change(function(e) {
		e.preventDefault();
		var lang = $(e.target).val();
		var url = $u.EmailTemplate_Edit + "/" + id + "?lang=" + lang;

		$a.LoadPage(url);
	});
});