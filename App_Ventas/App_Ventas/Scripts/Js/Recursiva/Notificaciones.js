$(function () {
    Notificaciones_Listar(); 
    setInterval(Notificaciones_Listar, 2000);
});

function Notificaciones_Listar() {
    fetch(baseUrl + 'Administracion/Notificacion/Notificacion_Listar', {
        method: "POST",
        headers: {
            "Content-type": "application/json; charset=utf-8",
            "dataType": "json"
        }
    })
        .then(response => response.json())
        .then(response => { Notificaciones_Cargar(response) })
        .catch(err => console.log(err));
}

function Notificaciones_Cargar(auditoria) {
    var Alerta = $('#AlertNotific'); 
    var _Html = "";
    var ViewNotificaciones = $('#List_Notific'); 
    var OnlyFive = false;
    var ListaNoti = new Array; 
    var _btnViewall = "<a class=\"all-notification\" onclick=\"ViewAllNotificaciones()\" href=\"javascript:void(0);\" >"
    _btnViewall += "Ver todas las notificaciones <i class=\"ti-arrow-right\" ></i>"
    _btnViewall += "</a>";
    if (auditoria.EJECUCION_PROCEDIMIENTO) {
        if (!auditoria.RECHAZAR) {
            if (auditoria.OBJETO.length > 0) {
                if (!$(Alerta).hasClass('pulse-css'))
                    $(Alerta).addClass('pulse-css');
                if (auditoria.OBJETO.length > 5) {
                    OnlyFive = true;
                    ListaNoti = auditoria.OBJETO.slice(0, 5);
                } else {
                    ListaNoti = auditoria.OBJETO;
                }
                $.each(ListaNoti, function (i, v) {
                    _Html += "<ul class=\"list-unstyled\">";
                    _Html += "<li class=\"media dropdown-item\">";
                    _Html += `<span class="${v.COLOR}"><i class="${v.IMAGE}"></i></span>`;
                    _Html += `<div class=\"media-body\"><a onclick=\"ViewAllNotificaciones()\" href=\"javascript:void(0);\"><p> ${v.MENSAJE}`;
                    _Html += "</p></a></div>";
                    _Html += `<span class="notify-time">${v.HORA}</span> </li>`;
                });
                if (OnlyFive)
                    _Html += _btnViewall;
                $(ViewNotificaciones).html(_Html);
            } else {
                $(Alerta).removeClass('pulse-css'); 
                _Html += "<div class=\"card-body row justify-content-center\">";
                _Html += "<img class=\"mb-4\" src='" + baseUrl + "assets/images/cofe.png' height=\"133\"; />";
                _Html += "<h4 class=\"text-center\">Sin notificaciones</h4>";
                _Html += "</div>";
                $(ViewNotificaciones).html(_Html);
            }
        }
    } else {
        //console.log(auditoria.MENSAJE_SALIDA, "Ocurrio un Error");
    }
}

function ViewAllNotificaciones() {
    var item =
    {
    };
    InvocarVista(baseUrl + 'Administracion/Notificacion/Index', item);
}

function Notificaciones_Estado() {
    fetch(baseUrl + 'Administracion/Notificacion/Notificacion_Estado', {
        method: "POST",
        headers: {
            "Content-type": "application/json; charset=utf-8",
            "dataType": "json"
        }
    })
    .then(response => response.json())
    //.then(response => { Notificaciones_Cargar(response) })
    .catch(err => console.log(err));

}