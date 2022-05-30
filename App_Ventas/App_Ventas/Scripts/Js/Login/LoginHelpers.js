$(window).on("load", function () {
    $('#preloader').fadeOut(500);
    $('#main-wrapper').addClass('show');

});

(function (window, $) {
    'use strict';
    var login = (function () {
        function run() {
            var token = getCookie("IP-CyberToken");
            if (token != null)
                Login_UsuarioLogeado(token);
            else
               $('#Login_BoxSession').show();
        }

        $('#Login_Ingresar').click(function () { Login_Ingresar() });

        function Login_Ingresar() {
            $('#Alert_Error').hide(); 
            if ($("#frm_Login").valid()) {
                Login_DisableLogin(); 
                setTimeout(function () {
                    var RememberMe = 0; 
                    if ($('#RememberMe').is(':checked'))
                        RememberMe = 1; 
                var item =
                    {
                        COD_USUARIO: $("#UserName").val(),
                        CLAVE_USUARIO: $("#Password").val(),

                    };
                var url = baseUrl + 'Login/Login/Login_Validar?RememberMe=' + RememberMe;
                var auditoria = SICA.Ajax(url, item, false);
                if (auditoria != null && auditoria != "") {
                    debugger;
                    if (auditoria.EJECUCION_PROCEDIMIENTO) {
                        if (!auditoria.RECHAZAR) {
                            Login_EnabledLogin();
                            document.cookie = "IP-CyberToken=" + auditoria.OBJETO;
                            window.localStorage.setItem("access_token", auditoria.OBJETO);
                            window.sessionStorage.setItem("access_token", auditoria.OBJETO);
                            Login_UsuarioLogeado(auditoria.OBJETO);
                        } else {
                            Login_EnabledLogin(); 
                            Login_AlertMessageError(auditoria.MENSAJE_SALIDA);
                        }
                    } else {
                        Login_EnabledLogin(); 
                        Login_AlertMessageError(auditoria.MENSAJE_SALIDA);
                    }
                    }
                }, 200);
            }
        }
        
        function Login_UsuarioLogeado(token) {
            $('#Login_BoxSession').hide();
            var item =
                 {
                     TOKEN: token,
                 };
            var url = baseUrl + 'Login/Login/Usuario';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {
                debugger;
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {

                    } else {
                        Login_EnabledLogin();
                        Login_AlertMessageError(auditoria.MENSAJE_SALIDA);
                    }
                } else {
                    Login_EnabledLogin();
                    Login_AlertMessageError(auditoria.MENSAJE_SALIDA);
                }
            }
            
        }

        function Login_DisableLogin() {
            $('#Login_Ingresar').html('<span class="spinner-grow spinner-grow-sm me-1" role="status" aria-hidden="true"></span> Procesando...');
            $('#frm_Login').addClass('disabled_content');        
        }

        function Login_EnabledLogin() {
            $('#Login_Ingresar').html('Ingresar');
            $('#frm_Login').removeClass('disabled_content');
        }

        function Login_AlertMessageError(mensaje) {
            $('#Alert_Error').show();
            $('#Msg_error').text(mensaje);
        }

        function getCookie(name) {
            var dc = document.cookie; 
            var prefix = name + "="; 
            var begin = dc.indexOf("; " + prefix); 
            if (begin == -1) { begin = dc.indexOf(prefix);
                if (begin != 0) return null; }
            else { begin += 2;
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) { end = dc.length; }
            } // because unescape has been deprecated, replaced with decodeURI // return unescape(dc.substring(begin + prefix.length, end)); 
            return decodeURI(dc.substring(begin + prefix.length, end));
        }


        return {
            init: function () {
                run();
            },
            //firmar: function () {
            //    FirmarDocumento();
            //},
            //setupInit: function (container, url) {
            //    setup(container, url);
            //},
            //firmarfin: function () {
            //    FirmaFinishProcesoFirma();
            //}
        }
    });

    window.Login = login;
})(window, jQuery);