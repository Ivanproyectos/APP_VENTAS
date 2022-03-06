
jQuery(document).ready(function () {
    //var waitimageUrl = baseUrl + 'assets/images/loading.gif';
    var options = {
        AjaxWait: {
            AjaxWaitMessage: "<div class=\"css_center_block\">  <div class=\"Loader_block\"> "
                   + "<div class=\"spinner2\">"
                     +  " <div class=\"circle1\"></div>"
                      + " <div class=\"circle2\"></div>"
                  + " </div>"
              + " </div> <p style=\"color:white;\">Procesando...</p>  </div> ",
            AjaxWaitMessageCss: { width: "20px", left: "45%", top: "40%", background: "none" }
        },
        AjaxErrorMessage: "<h6>Error! Por favor contacte con el Administrador del sistema!</h6>"
    };
    var AjaxGlobalHandler = {
        Initiate: function (options) {
            try {
                // Ajax events fire in following order
                jQuery(document).ajaxStart(function () {
                    jQuery.blockUI({
                        message: options.AjaxWait.AjaxWaitMessage,
                       // baseZ: 2000
                         css: options.AjaxWait.AjaxWaitMessageCss
                    });
                }).ajaxSend(function (e, xhr, opts) {
                }).ajaxError(function (e, xhr, opts) {
                    if (500 == xhr.status) {
                        document.location.replace("");
                        return;
                    }
                    jQuery.unblockUI();  
                    //            $.colorbox({ html: options.AjaxErrorMessage });
                }).ajaxSuccess(function (e, xhr, opts) {
                }).ajaxComplete(function (e, xhr, opts) {
                }).ajaxStop(function () {
                    jQuery.unblockUI();
                });
            } catch (e) {
                jQuery.unblockUI();
            }
        }
    };

    AjaxGlobalHandler.Initiate(options);
    var loc = document.location.href.split(/[\?#]/).shift().replace(/\/$/, '');
    jQuery("a").each(function () {
        if (this.href.toLowerCase() == loc.toLowerCase()) jQuery(this).addClass("ui-state-highlight");
    });
});


