$(function() {
	var window = $("<div></div>");

	$("#files-button")
		.bind("click", function(e) {
			e.preventDefault();
			debugger;
			var url = $(this).attr("href");

			var kendoWindow = window.data("kendoWindow");
			kendoWindow.refresh({
				url: url
			});
			kendoWindow.open();
		});


	window.kendoWindow({
		width: "615px",
	});
});