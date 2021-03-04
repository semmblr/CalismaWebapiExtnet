var template = '<span style="color:{0};">{1}</span>';

var change = function (value) {
    return Ext.String.format(template, (value > 0) ? "green" : "red", value);
};

var pctChange = function (value) {
    return Ext.String.format(template, (value > 0) ? "green" : "red", value + "%");
};