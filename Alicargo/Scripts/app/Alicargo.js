var Alicargo = (function($a) {
	$a.MaxPageSize = 10000;
	$a.DefaultPageSize = 20;

	$a.SelectedPageSize = function(selector, value) {
		var key = selector + '-page-size';

		if (!!value) {
			$.cookie(key, value);
			return value;
		}

		value = $.cookie(key);

		if (!!!value) {
			value = $a.DefaultPageSize;
		}

		return value;
	};

	function getGridHeight(selector) {
		var height = $(window).height();
		var grid = $(selector);
		var top = grid.position().top;
		var marginAndBorder = 22;

		return height - top - marginAndBorder;
	}

	$a.CreateGrid = function(selector, settings) {
		settings = $.extend(settings, {
			resizable: true,
			pageable: { refresh: true, pageSizes: [10, 20, 50, 100, $a.MaxPageSize] },
			height: getGridHeight(selector)
		});

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
			.change(function(e) {
				var val = $(e.target).val();
				$a.SelectedPageSize(selector, val);
			})
			.data('kendoDropDownList')
			.setDataSource(dataSource);
	};

	$a.ShowError = function() {
		$a.ShowMessage($a.Localization.Pages_AnError);
	};


	$a.ShowMessage = function(message) {
		alert(message);
	};

	$a.DefaultGridButtonWidth = "29px";
	$a.Confirm = function(message) {
		return window.confirm(message);
	};

	$a.LoadPage = function(url) {
		window.location.href = url;
	};

	return $a;
}(Alicargo || {}));