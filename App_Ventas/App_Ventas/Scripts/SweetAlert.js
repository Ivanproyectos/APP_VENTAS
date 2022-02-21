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




})(jQuery);