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

