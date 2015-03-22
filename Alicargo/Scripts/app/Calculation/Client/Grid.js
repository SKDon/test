var Alicargo = (function($a) {
	$a.Calculation = (function($c) {

		$(function() {
			$a.CreateGrid("#calculation-grid", {
				columns: $c.Columns(),
				dataSource: $c.DataSource(),
				editable: false,
				groupable: false
			});
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});