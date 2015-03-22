$(function() {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

	function initUploader(appId, type, files) {
		$("#" + type).kendoUpload({
			multiple: true,
			async: {
				saveUrl: $u.ApplicationFiles_Upload + "/" + appId + "?type=" + type,
				removeUrl: $u.ApplicationFiles_Delete,
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
		var appId = holder.data("app-id");

		$.getJSON($u.ApplicationFiles_Files + "/" + appId + "?type=" + type).done(function(data) {
			initUploader(appId, type, data);
		}).fail($a.ShowError);
	}

	$(".files-holder").each(function() { initHolder($(this)); });
});