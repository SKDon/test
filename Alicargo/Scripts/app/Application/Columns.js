$(function () {

	var $a = Alicargo;
	var $u = $a.Urls;
	var $r = $a.Roles;
	var $l = $a.Localization;

	$a.Application = (function ($apl) {

		function dateEditor(container, options) {
			$('<input name="' + options.field + '" data-text-field="' + options.field
				+ '" data-value-field="' + options.field + '" data-bind="value:'
				+ options.field + '" />')
				.appendTo(container)
				.kendoDatePicker();
		}

		function stateDropDownEditor(container, options) {
			var applicationId = options.model.Id;
			$('<input required data-text-field="StateName" data-value-field="StateId" data-bind="value:' + options.field + '"/>')
				.appendTo(container)
				.kendoDropDownList({
					autoBind: false,
					select: function (e) {
						if (window.confirm($l.Pages_StateSetConfirm)) {
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

		var groupHeaderTemplateAwb = "#= Alicargo.Localization.Entities_AWB + ':' + !!value ? value : Alicargo.Localization.Pages_NoAirWaybill #";
		var adminColumns = [
			{ field: "CreationTimestampLocalString", title: $l.Entities_CreationTimestamp, groupable: false, width: "90px" },
			{ field: "State", title: $l.Entities_StateName, groupable: true, width: "150px", editor: stateDropDownEditor, template: "#= State.StateName #" },
			{ field: "DateOfCargoReceiptLocalString", title: $l.Entities_DateOfCargoReceipt, groupable: false, editor: dateEditor, width: "90px", template: "#= DateOfCargoReceiptLocalString == null ? '' : kendo.toString(DateOfCargoReceiptLocalString, 'd') #" },
			{ field: "DateInStockLocalString", title: $l.Entities_DateInStock, groupable: false, width: "90px" },
			{ field: "ClientNic", title: $l.Entities_Nic, groupable: true, width: "100px" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, groupable: false, width: "60px" },
			{ field: "CountryName", title: $l.Entities_Country, groupable: false, width: "70px" },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: false, width: "100px" },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: false, width: "100px" },
			{ field: "Count", title: $l.Entities_Count, groupable: false, width: "35px", groupFooterTemplate: "#= Count.sum #", format: "{0:n0}" },
			{ field: "Weigth", title: $l.Entities_Weigth, groupable: false, width: "46px", groupFooterTemplate: "#= kendo.toString(Weigth.sum, 'n2') #", format: "{0:n2}" },
			{ field: "Volume", title: $l.Entities_Volume, groupable: false, width: "46px", format: "{0:n2}" },
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false, width: "150px" },
			{ field: "ValueString", title: $l.Entities_Value, groupable: false, width: "70px" },
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false, width: "75px" },
			{ field: "TransitCity", title: $l.Entities_City, groupable: false, width: "100px" },
			{ field: "TransitCarrierName", title: $l.Entities_CarrierName, groupable: false, width: "100px" },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{ field: "AirWaybill", title: $l.Entities_AirWayBill, groupable: true, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		var clientColumns = [
			{ field: "CreationTimestampLocalString", title: $l.Entities_CreationTimestamp, groupable: false, width: "70px" },
			{ field: "State", title: $l.Entities_StateName, groupable: true, template: "#= State.StateName #" },
			{ field: "DateOfCargoReceiptLocalString", title: $l.Entities_DateOfCargoReceipt, groupable: false, width: "70px" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: false },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: false },
			{ field: "Count", title: $l.Entities_Count, groupable: false, width: "35px", format: "{0:n0}" },
			{ field: "Weigth", title: $l.Entities_Weigth, groupable: false, width: "46x", format: "{0:n2}" },
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false },
			{ field: "ValueString", title: $l.Entities_Value, groupable: false, width: "70px" },
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false },
			{ field: "TransitCarrierName", title: $l.Entities_CarrierName, groupable: false },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{ field: "AirWaybill", title: $l.Entities_AirWayBill, groupable: true, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		var forwarderColumns = [
			{ field: "State", title: $l.Entities_StateName, groupable: true, editor: stateDropDownEditor, template: "#= State.StateName #", width: "150px" },
			{ field: "ClientLegalEntity", title: $l.Entities_LegalEntity, groupable: true, width: "150px" },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			{ field: "Count", title: $l.Entities_Count, groupable: false, width: "35px", groupFooterTemplate: "#= Count.sum #", format: "{0:n0}" },
			{ field: "Weigth", title: $l.Entities_Weigth, groupable: false, width: "46px", groupFooterTemplate: "#= kendo.toString(Weigth.sum, 'n2') #", format: "{0:n2}" },
			{ field: "TransitCarrierName", title: $l.Entities_CarrierName, groupable: false, width: "100px" },
			{ field: "TransitMethodOfTransitString", title: $l.Entities_MethodOfTransit, groupable: false, width: "75px" },
			{ field: "TransitCity", title: $l.Entities_City, groupable: false, width: "100px" },
			{ field: "TransitRecipientName", title: $l.Entities_RecipientName, groupable: false, width: "100px" },
			{ field: "TransitAddress", title: $l.Entities_Address, groupable: false, width: "100px" },
			{ field: "TransitPhone", title: $l.Entities_Phone, groupable: false, width: "100px" },
			{ field: "TransitWarehouseWorkingTime", title: $l.Entities_WarehouseWorkingTime, groupable: false, width: "90px" },
			{ field: "TransitReference", title: $l.Entities_TransitReference, groupable: false, width: "150px" },
			{ field: "TransitCost", title: $l.Entities_TransitCost, groupable: false, width: "150px" },
			{ field: "AirWaybill", title: $l.Entities_AirWayBill, groupable: true, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		var senderColumns = [
			{ field: "ClientNic", title: $l.Entities_Nic, groupable: true },
			{ field: "DisplayNumber", title: $l.Entities_DisplayNumber, width: "70px", groupable: false },
			{ field: "CountryName", title: $l.Entities_Country, groupable: false, width: "70px" },
			{ field: "FactoryName", title: $l.Entities_FactoryName, groupable: false },
			{ field: "MarkName", title: $l.Entities_Mark, groupable: false },
			{ field: "Count", title: $l.Entities_Count, groupable: false, width: "35px", groupFooterTemplate: "#= Count.sum #", format: "{0:n0}" },
			{ field: "Weigth", title: $l.Entities_Weigth, groupable: false, width: "46px", groupFooterTemplate: "#= kendo.toString(Weigth.sum, 'n2') #", format: "{0:n2}" },
			{ field: "Volume", title: $l.Entities_Volume, groupable: false, width: "46px", format: "{0:n2}" },
			{ field: "Invoice", title: $l.Entities_Invoice, groupable: false },
			{ field: "ValueString", title: $l.Entities_Value, groupable: false, width: "70px" },
			{ field: "State", title: $l.Entities_StateName, groupable: true, editor: stateDropDownEditor, template: "#= State.StateName #" },
			{ field: "StateChangeTimestampLocalString", title: $l.Entities_StateChangeTimestamp, groupable: false },
			{ field: "AirWaybill", title: $l.Entities_AirWayBill, groupable: true, width: "150px", groupHeaderTemplate: groupHeaderTemplateAwb }];

		$apl.GetColumns = function () {
			if ($r.IsClient) {
				return clientColumns;
			} else if ($r.IsForwarder) {
				return forwarderColumns;
			} else if ($r.IsSender) {
				return senderColumns;
			} else if ($r.IsAdmin) {
				return adminColumns;
			}
			return null;
		};
		
		return $apl;
	})($a.Application || {});
});