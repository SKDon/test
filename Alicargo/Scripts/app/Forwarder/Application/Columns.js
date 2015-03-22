$(function() {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $l = $a.Localization;

	$a.Application = (function($apl) {

		function stateDropDownEditor(container, options) {
			var applicationId = options.model.Id;
			$('<input required data-text-field="StateName" data-value-field="StateId" data-bind="value:' + options.field + '"/>')
				.appendTo(container)
				.kendoDropDownList({
					autoBind: false,
					select: function(e) {
						if ($a.Confirm($l.Pages_StateSetConfirm)) {
							var dataItem = this.dataItem(e.item.index());
							var url = $u.ApplicationUpdate_SetState;
							$.post(url, { stateId: dataItem.StateId, id: applicationId }).done($apl.UpdateGrid).fail($a.ShowError);
						} else {
							$apl.GetGrid().closeCell();
						}
					},
					dataTextField: "StateName",
					dataValueField: "StateId",
					dataSource: {
						type: "json",
						transport: {
							read: {
								cache: false,
								dataType: "json",
								url: $u.ApplicationUpdate_States + "/" + applicationId,
								type: "POST"
							}
						}
					}
				});
		}

		var n2Format = "{0:n2}";
		var weightField = {
			field: "Weight",
			title: $l.Entities_Weight,
			groupable: false,
			width: "46px",
			groupFooterTemplate: "#= kendo.toString(Weight.sum, 'n2') #",
			format: n2Format
		};
		var n0Format = "{0:n0}";
		var countField = {
			field: "Count",
			title: $l.Entities_Count,
			groupable: false,
			width: "35px",
			groupFooterTemplate: "#= Count.sum #",
			format: n0Format
		};
		var volumeField = {
			field: "Volume",
			title: $l.Entities_Volume,
			groupable: false,
			width: "46px",
			groupFooterTemplate: "#= kendo.toString(Volume.sum, 'n2') #",
			format: n2Format
		};

		function groupHeaderTemplateAwb(data) {
			if (!!data.value) {
				var json = $.parseJSON(data.value);
				return $l.Entities_AWB + ': ' + json.text;
			}

			return $l.Entities_AWB + ': ' + $l.Pages_NoAirWaybill;
		};

		$apl.GetColumns = function() {
			return [
				{ field: "State", title: $l.Entities_StateName, groupable: false, editor: stateDropDownEditor, template: "#= State.StateName #", width: "150px" },
				{ field: "ClientLegalEntity", title: $l.Entities_LegalEntity, groupable: false, width: "150px" },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
				countField,
				weightField,
				volumeField,
				{ field: "CarrierName", title: $l.Entities_Carrier, groupable: false, width: "100px" },
				{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false, width: "75px" },
				{ field: "TransitDeliveryTypeString", title: $l.Entities_DeliveryType, groupable: false, width: "75px" },
				{ field: "TransitCity", title: $l.Entities_City, groupable: false, width: "100px" },
				{ field: "CarrierContact", title: $l.Entities_RecipientName, groupable: false, width: "100px" },
				{ field: "CarrierAddress", title: $l.Entities_Address, groupable: false, width: "100px" },
				{ field: "CarrierPhone", title: $l.Entities_Phone, groupable: false, width: "100px" },
				{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
				{ field: "ForwarderTransitCost", title: $l.Entities_TransitCost, groupable: false, width: "150px" },
				{
					field: "AirWaybill",
					title: $l.Entities_AirWaybill,
					groupable: false,
					width: "150px",
					groupHeaderTemplate: groupHeaderTemplateAwb
				}
			];
		};

		return $apl;
	})($a.Application || {});
});