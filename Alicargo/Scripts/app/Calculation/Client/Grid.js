var Alicargo = (function ($a) {
	$a.Calculation = (function ($c) {

		$(function () {
			$("#calculation-grid").kendoGrid({
				columns: $c.Columns(),
				pageable: { refresh: true, pageSizes: [10, 20, 50, 100] },
				pageSize: 10,
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