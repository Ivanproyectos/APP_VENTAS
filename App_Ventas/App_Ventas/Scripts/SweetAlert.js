(function ($) {
    //"use strict"

    /*******************
    Sweet-alert JS
    *******************/

 

    jConfirm = function (message, title, callback) {
        swal.fire({
            title: title,
            text: message,
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
        text : message,
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


    
})(jQuery);