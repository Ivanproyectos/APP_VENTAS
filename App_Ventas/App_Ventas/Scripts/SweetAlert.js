(function ($) {
    //"use strict"

    /*******************
    Sweet-alert JS
    *******************/

    jConfirm = function (message, title, callback) {
        swal.fire({
            title: title,
            html: message,
            icon: "warning",
            showCancelButton: !0,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Aceptar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: !1,
            closeOnCancel: !1
        }).then((result) => {
            if (result.isConfirmed) {
          callback(true); 
        } else {
            callback(false); 
        }
      }); 
    }

jOkas = function (message, title){
    Swal.fire({
        title: title,
        html : message,
        icon:'success',
        confirmButtonText: 'Aceptar'
    }
  ); 
}

jError = function (message, title){
    Swal.fire({
        title: title,
        text : message,
        icon:'error',
        confirmButtonText: 'Aceptar'
    }
  ); 
}


jInfo = function (message, title){
    Swal.fire({
        title: title,
        text : message,
        icon:'info',
        confirmButtonText: 'Aceptar'
    }
  ); 
}

jSweetModal = function (message, title){
    Swal.fire({
        title: title,
        html : message,
       // icon:icon,
        cancelButtonText: "Cerrar",
        showCancelButton: true,
        showConfirmButton: false,
    }
  ); 
}





jWarning = function (message, title){
    toastr.warning(message, title, {
        positionClass: "toast-top-right",
        timeOut: 5e3,
        closeButton: !0,
        debug: !1,
        newestOnTop: !0,
        progressBar: !0,
        preventDuplicates: !0,
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut",
        tapToDismiss: !1
    })
}

jDanger = function (message, title){
    toastr.error(message, title, {
        positionClass: "toast-top-right",
        timeOut: 5e3,
        closeButton: !0,
        debug: !1,
        newestOnTop: !0,
        progressBar: !0,
        preventDuplicates: !0,
        onclick: null,
        showDuration: "300",
        hideDuration: "1000",
        extendedTimeOut: "1000",
        showEasing: "swing",
        hideEasing: "linear",
        showMethod: "fadeIn",
        hideMethod: "fadeOut",
        tapToDismiss: !1
    })
}

//jPrueba = function (message, title){
//    Swal.fire({
//        title: "Devolver Producto",
//        html: _html,
//        input: 'text',
//        inputPlaceholder: 'Ingrese Cantidad a devolver',
//        inputValue: _CANTIDAD,
//        icon:"warning",
//        inputAttributes: {
//            autocapitalize: 'off'
//        },
//        showCancelButton: true,
//        confirmButtonText: 'Cancelar',
//        confirmButtonText: 'Devolver',
//        showLoaderOnConfirm: true,
//        inputValidator: (value) => {
//            if(_CANTIDAD < value){
//                return 'La cantidad a devolver no puede ser mayor a la cantidad vendida'
//    }
//                  return !value && 'Este campo es obligatiorio'
//},
//preConfirm: (value) => {
//    var item = {
//        ID_VENTA_DETALLE: _ID_VENTA_DETALLE,
//        USU_MODIFICACION: $('#input_hdcodusuario').val(),
//        CANTIDAD : value
//    };
// 
//var auditoria = SICA.Ajax(url, item, false); 
//if (auditoria != null && auditoria != "") {
//    if (auditoria.EJECUCION_PROCEDIMIENTO) {
//        if (!auditoria.RECHAZAR) {
//            return auditoria
//        }else{
//            throw new Error(auditoria.MENSAJE_SALIDA )
//        }
//    }
//}else{
//    return auditoria
//}
//},
//allowOutsideClick: () => !Swal.isLoading()
//}).then((result) => {
//    
//if (result.isConfirmed) {
//    jOkas("Producto devuelto con exito!", "Proceso");
//    Ventas_ConfigurarGrilla();
//    Ventas_Detalle_CargarGrilla($('#hfd_ID_VENTA').val());
//}
//})

//}
    
})(jQuery);