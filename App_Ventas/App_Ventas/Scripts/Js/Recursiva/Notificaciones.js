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
                //if (OnlyFive)
                _Html += _btnViewall;
                $(ViewNotificaciones).html(_Html);
            } else {
                $(Alerta).removeClass('pulse-css'); 
                _Html += "<div class=\"card-body row justify-content-center\">";
                _Html += "<img class=\"mb-4\" src='" + baseUrl + "assets/images/cofe.png' height=\"133\"; />";
                _Html += "<h4 class=\"text-center\">Sin notificaciones</h4>";
                _Html += "</div>";
                _Html += _btnViewall;
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
    .catch(err => console.log(err));

}

function Notificaciones_LeidasListar() {
    var ViewNotificacionesLeidas = $('#ListNoti_Leidas'); 
    var _Html = "";
    var item = {
        ESTADO :1, 
        FECHA_INICIO: $('#Noti_Fecha').val().split('-')[0],
        FECHA_FIN: $('#Noti_Fecha').val().split('-')[1],
}; 
    fetch(baseUrl + 'Administracion/Notificacion/Notificacion_Listar', {
        method: "POST",
        headers: {
            "Content-type": "application/json; charset=utf-8",
            "dataType": "json"
        },
        body:JSON.stringify(item)
    })
        .then(auditoria => auditoria.json())
        .then(auditoria => {
            if(auditoria != null){
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        if (auditoria.OBJETO.length > 0) {    
                            $.each(auditoria.OBJETO, function (i, v) {
                                _Html += "<a href=\"javascript:void()\" class=\"list-group-item list-group-item-action flex-column align-items-start\">";
                                _Html += "<div class=\"d-flex w-100 justify-content-between\">";
                                _Html += `<h5 class="mb-3">${v.FECHA_REGISTRO}</h5>`;
                                _Html += `<small class="text-muted">${v.HORA}</small></div>`;
                                _Html += `<div class="d-flex w-100 justify-content-start">`;
                                _Html += `<span class="${v.COLOR}  pill-image"><i class="${v.IMAGE}"></i></span>`;
                                _Html += `<p class="mb-1">${v.MENSAJE}.</p></div> </a>`;
                            });
                            $(ViewNotificacionesLeidas).html(_Html);
                        } else {
                            _Html += "<h4 class=\"text-center\">Sin notificaciones</h4>";
                            $(ViewNotificacionesLeidas).html(_Html);
                        }
                    }
                } 
        }
    })
    .catch(err => console.log(err));
}