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

// Código de disparo de Alert BootStrap
const alertPlaceholder = document.getElementById('liveAlertPlaceholder');
const appendAlert = (message, type) => {
    const wrapper = document.createElement('div');
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible" role="alert">`,
        `   <div>${message}</div>`,
        '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
        '</div>'
    ].join('');

    alertPlaceholder.append(wrapper);
}

//const alertTrigger = document.getElementById('liveAlertBtn')
//if (alertTrigger) {
//    alertTrigger.addEventListener('click', () => {
//        appendAlert('Teste de Constante: you triggered this alert message!', 'success')
//    })
//}








