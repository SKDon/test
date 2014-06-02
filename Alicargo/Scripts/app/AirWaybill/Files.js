$(function() {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

	function initUploader(awbId, type, files) {
		$("#" + type).kendoUpload({
			multiple: true,
			async: {
				saveUrl: $u.AwbFiles_Upload + "/" + awbId + "?type=" + type,
				removeUrl: $u.AwbFiles_Delete,
				removeField: "Id",
				autoUpload: true
			},
			localization: {
				select: $l.Pages_UploadFiles
			},
			files: files,
			success: function(e) {
				if (e.operation == "upload") {
					e.files[0].id = e.response.id;
				}
			},
			remove: function(e) {
				$.each(e.files, function() { this.name = this.id; });
			}
		});
	}

	function initHolder(holder) {
		var type = holder.data("type");
		var awbId = holder.data("awb-id");

		$.getJSON($u.AwbFiles_Files + "/" + awbId + "?type=" + type).done(function(data) {
			initUploader(awbId, type, data);
		}).fail($a.ShowError);
	}

	$(".files-holder").each(function() { initHolder($(this)); });
});