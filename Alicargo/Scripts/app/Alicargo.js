var Alicargo = (function($a) {
	$a.MaxPageSize = 10000;
	$a.DefaultPageSize = $a.MaxPageSize;
	$a.DefaultPageSizes = { refresh: true, pageSizes: [10, 20, 50, 100, $a.MaxPageSize] };

	$a.CreateGrid = function(selector, settings) {
		$(selector).kendoGrid(settings);

		var pageSizes = [
			{ text: "10", value: 10 },
			{ text: "20", value: 20 },
			{ text: "50", value: 50 },
			{ text: "100", value: 100 },
			{ text: "All", value: $a.MaxPageSize }
		];
		
		var dataSource = new kendo.data.DataSource({ data: pageSizes });		

		$('.k-pager-sizes select[data-role="dropdownlist"]', selector)
			.data('kendoDropDownList')
			.setDataSource(dataSource);
	};

	$a.ShowError = function() { alert($a.Localization.Pages_AnError); };
	$a.DefaultGridButtonWidth = "29px";
	$a.Confirm = function(message) { return window.confirm(message); };

	return $a;
}(Alicargo || {}));