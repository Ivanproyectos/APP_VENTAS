
var Tamanio_Valido = 314572800; // 300MB -- Cantidad en MB
var MiSistema =  $("#inputHddNombre_Sistema").val() == "" ? "":  "/" +$("#inputHddNombre_Sistema").val();

/////////// para los menus active open; 
$('.quixnav .metismenu ul a ').click(function () {
    var _this = $(this).parent().children(); 
    $('.quixnav .metismenu ul a ').removeClass('quixnav_a_active quixnav_a_active_before');
    $(this).addClass('quixnav_a_active quixnav_a_active_before');
    $(this).addClass('quixnav_a_active quixnav_a_active_before');

    var _liPadre = $(this).parent().parent().parent();
    var _aPadre = _liPadre.children();
    var _TextPadre = _aPadre[0].innerText;
    var _TextHijo = $(this).text(); 

    $('#Text_padre').text(_TextPadre);
    $('#Text_Hijo').text(_TextHijo);
    //_liPadre.children();
});

/////////// para los menus sin grupo; 
$('.quixnav .metismenu li .not-before').click(function () {
    var _this = $(this);
    var _TextHijo = _this[0].innerText;
    $('#Text_padre').text('Home');
    $('#Text_Hijo').text(_TextHijo);

});


function blockUI_(message) {
    message == "" ? "Procesando..." : message; 
    jQuery.blockUI({
        message: "<div class=\"css_center_block\">  <div class=\"Loader_block\"> "
                        + "<div class=\"spinner2\">"
                            + " <div class=\"circle1\"></div>"
                            + " <div class=\"circle2\"></div>"
                        + " </div>"
                    + " </div> <p style=\"color:white;\">" + message + "</p>  </div> ",
    css: { width: "20px", left: "45%", top: "40%", background: "none" }
    });
}