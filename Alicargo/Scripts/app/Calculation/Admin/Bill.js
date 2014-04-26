var Alicargo = (function($a) {
	var $u = $a.Urls;
	$a.Calculation = (function($c) {

		$c.ShowBillWindow = function(data) {
			$("<div></div>").kendoWindow({
				width: "1000px",
				position: { top: 60, left: 100 },
				title: "Счёт",
				animation: false,
				modal: true,
				content: $u.Admin_Bill_Preview + "?applicationId=" + data.ApplicationId
			}).data("kendoWindow").open();
		};

		return $c;
	})($a.Calculation || {});
	return $a;
})(Alicargo || {});