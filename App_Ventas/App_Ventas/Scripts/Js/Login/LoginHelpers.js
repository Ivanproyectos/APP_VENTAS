$(window).on("load", function () {
    $('#preloader').fadeOut(500);
    $('#main-wrapper').addClass('show');

});

(function (window, $) {
    'use strict';
    var login = (function () {
        function run() {
            $('#Login_BoxSession').show();
        }

        $('#Login_Ingresar').click(function () { Login_Ingresar() });

        function Login_Ingresar() {
            if ($("#frm_Login").valid()) {
            }
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