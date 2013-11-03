var Alicargo = (function($a) {
	$a.Calculation = (function($c) {

		function dataBound() {
			var detailsTemplate = kendo.template($("#calculation-grid-details-template").html());

			function getDetails(awbId) {
				var awbDetails = null;
				for (var infoIndex in $c.CalculationInfo) {
					var info = $c.CalculationInfo[infoIndex];
					if (awbId == info.AirWaybillId) {
						awbDetails = info;
						break;
					}
				}
				return awbDetails;
			}

			$("tr.k-group-footer").each(function(i) {
				var awbId = $c.CalculationInfo[i].AirWaybillId;

				var awbDetails = getDetails(awbId);

				var detailsHtml = $(detailsTemplate(awbDetails));

				$(this).after(detailsHtml);
			});
		}

		$(function() {
			$a.CreateGrid("#calculation-grid", {
				columns: $c.Columns(),
				pageable: $a.DefaultPageSizes,
				dataSource: $c.DataSource(),
				editable: false,
				groupable: false,
				resizable: true,
				dataBound: dataBound,
			});
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});