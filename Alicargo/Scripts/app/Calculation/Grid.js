
var Alicargo = (function ($a) {
	var $u = $a.Urls;

	$a.Calculation = (function ($c) {

		$c.GetMainGrid = function () {
			var grid = $("#calculation-grid").data("kendoGrid");
			$c.GetMainGrid = function () { return grid; };
			return grid;
		};

		$c.ExpandedRow = function (row) {
			if (row !== undefined) {
				$c._ExpandedRow = row;
			}
			return $c._ExpandedRow;
		};

		$(function () {
			var gridHolder = $("#calculation-grid");

			gridHolder.kendoGrid({
				columns: [{ field: "AwbDisplay", template: "<b>#: AwbDisplay # </b>" }],
				pageable: true,
				editable: false,
				dataSource: {
					transport: {
						read: {
							dataType: "json",
							url: $u.Calculation_List,
							type: "POST"
						}
					},
					schema: { model: { id: "AwbId" } },
					pageSize: 20,
					serverPaging: true,
					error: $a.ShowError
				},
				detailInit: function (r) {
					var data = $c.GetMainGrid().dataItem(r.masterRow);
					$c.InitDetails(r.detailRow, data);
				},
				dataBound: function () {
					var row = $c.ExpandedRow();
					if (row !== undefined && row != null) {
						var uid = row.data("uid");
						this.expandRow("tr.k-master-row[data-uid=" + uid + "]");
					}
				},
				detailExpand: function (e) {
					var row = $c.ExpandedRow();
					if (row !== undefined) {
						this.collapseRow(row);
					}
					$c.ExpandedRow(e.masterRow);
				},
				detailTemplate: kendo.template($("#calculation-grid-details-template").html()),
			});

			gridHolder.children(".k-grid-header").hide();
		});

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});