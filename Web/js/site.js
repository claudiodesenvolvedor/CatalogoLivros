function jqAjax(httpMethod, url, data, settings) {
    var settingsDefaults = {
        type: (!!httpMethod) ? httpMethod : "GET",
        url: url,
        headers: {},
        dataType: "json",
        contentType: "application/json; charset=UTF-8",
        data: data,
        success: function (result, status, xhr) { },
        error: function (xhr, status, error) { }
    };
    if (!!settings)
        $.extend(settingsDefaults, settings);
    return $.ajax(settingsDefaults);
}