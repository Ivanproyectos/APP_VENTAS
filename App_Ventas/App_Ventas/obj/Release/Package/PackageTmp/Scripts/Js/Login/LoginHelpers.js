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

            $('#Login_Ingresar').click(function () { Login_Validar() });
            $('#Login_SignOff').click(function () { SignOff() });
            $(window).keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    Login_Validar();
                }
            });
            
        }

        function Login_Validar() {
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
            var item =
                 {
                     TOKEN: token,
                 };
            var url = baseUrl + 'Login/Login/Usuario';
            var auditoria = SICA.Ajax(url, item, false);
            if (auditoria != null && auditoria != "") {        
                if (auditoria.EJECUCION_PROCEDIMIENTO) {
                    if (!auditoria.RECHAZAR) {
                        $('#Login_BoxSession').hide();
                        $('#Login_nameUser').text(auditoria.OBJETO.NOMBRES_APE)
                        var count_permisos = auditoria.OBJETO.Lista_Sucursales.length;           
                        if (count_permisos > 0) {
                            //if (count_permisos == 1) {
                            //    Login_Ingresar(auditoria.OBJETO.Lista_Sucursales[0].ID_USUARIO_PERFIL_HASH); 
                            //} else if (count_permisos > 1) {
                                var Html = Login_RowsPermisos(auditoria.OBJETO.Lista_Sucursales);
                                var tabla = $('#Login_TablePermisos');
                                tabla[0].innerHTML = "";
                                tabla[0].innerHTML = Html;
                                $('#Login_ListPermisos').show();
                                $('[data-bs-toggle="tooltip"]').tooltip();
                            //}
                        } else {
                            $('#Login_MsgSinPermisos').show();
                        }
                        $('#Login_BoxUsuLogeo').show();
                    } else {
                        Login_AlertMessageError(auditoria.MENSAJE_SALIDA);
                    }
                } else {
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

        function Login_RowsPermisos(Lista) {
            var html = "<a href=\"javascript:void()\" class=\"list-group-item list-group-item-action disabled\">Sucursales Asignados </a>";
            $.each(Lista, function (i, v) {
                var idgrilla = i + 1;
                var ID_USUARIO_PERFIL_HASH = "'" + v.ID_USUARIO_PERFIL_HASH + "'"
                html += "<a href=\"javascript:void()\" onclick=\"Login_Ingresar(" + ID_USUARIO_PERFIL_HASH + ")\" data-placement=\"left\" "
                         + "class=\"list-group-item list-group-item-action \" title=\"Vamos para alla!\" data-bs-toggle=\"tooltip\" style=\"cursor:pointer\"><i class=\"bi bi-geo-alt-fill\"></i> " + v.DESC_SUCURSAL +" </a>";
            });
            return html; 
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

        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/";
        }

        var eliminarCookie = function (key) {
            return document.cookie = key + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        }

        function SignOff() {
            eliminarCookie("IP-CyberToken"); 
            window.location.reload()
        }

        function Login_Ingresar(ID_PERMISO) {
            window.location.href = baseUrl + "Home/index?Pf=" + ID_PERMISO;
        }


        return {
            init: function () {
                run();
            },
            LogIn: function (id) {
                Login_Ingresar(id);
            },
        }
    });

    window.Login = login;
})(window, jQuery);