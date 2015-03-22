var Alicargo = (function($a) {
	var $u = $a.Urls;
	$a.Calculation = (function($c) {

		$c.ShowBillWindow = function(data) {
			$a.LoadPage($u.Admin_Bill_Preview + "/" + data.ApplicationId);
		};

		return $c;
	})($a.Calculation || {});
	return $a;
})(Alicargo || {});