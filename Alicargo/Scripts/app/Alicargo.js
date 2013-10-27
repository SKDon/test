var Alicargo = (function($a) {
	$a.ShowError = function() { alert($a.Localization.Pages_AnError); };
	$a.DefaultGridButtonWidth = "29px";
	$a.Confirm = function(message) { return window.confirm(message); };
	return $a;
}(Alicargo || {}));