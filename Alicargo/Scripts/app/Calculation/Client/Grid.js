var Alicargo = (function ($a) {
	$a.Calculation = (function ($c) {

		$(function () {
			$("#calculation-grid").kendoGrid({
				columns: $c.Columns(),
				pageable: { refresh: true, pageSizes: $a.DefaultPageSizes },
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