var Alicargo = (function($a) {
	$a.Calculation = (function($c) {

		$(function() {
			$a.CreateGrid("#calculation-grid", {
				columns: $c.Columns(),
				pageable: $a.DefaultPageSizes,
				dataSource: $c.DataSource(),
				editable: false,
				groupable: false,
				resizable: true
			});
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});