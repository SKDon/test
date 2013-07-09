$.validator.methods.number = function (value, element) {
	return this.optional(element) ||
		!isNaN(Globalize.parseFloat(value));
};

$.validator.methods.date = function (value, element) {
	return this.optional(element) ||
		!isNaN(Globalize.parseDate(value));
};