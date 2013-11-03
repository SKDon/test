﻿var Alicargo = (function ($a) {
	$a.ShowError = function () { alert($a.Localization.Pages_AnError); };
	$a.DefaultGridButtonWidth = "29px";
	$a.MaxPageSize = 10000;
	$a.DefaultPageSize = $a.MaxPageSize;
	$a.DefaultPageSizes = [10, 20, 50, 100, $a.MaxPageSize];
	$a.Confirm = function (message) { return window.confirm(message); };
	return $a;
}(Alicargo || {}));