var Alicargo = (function ($a) {

	$a.ShowError = function () {
		alert(window.Alicargo.Localization.Pages_AnError);
	};

	//var grids = {};
	
	//$a.GetGrid = function (name) {
	//	if (grids[name]) {
	//		return grids[name];
	//	}
		
	//	var grid = $(name).data("kendoGrid");

	//	grids[name] = grid;

	//	return grid;
	//};

	return $a;
}(Alicargo || {}));