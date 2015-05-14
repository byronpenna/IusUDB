$(document).ready(function () {
    //nicEditors.findEditor('editor').getContent();
    
    //plugins 
        // rich text 
            bkLib.onDomLoaded(function() {
                txtAreaEditor = new nicEditor().panelInstance('editor');
                html = "<img src='http://www.matrallune.com/images/imagen_corporativa.jpg' alt=' align='none' class='activeRichImage'>"
                nicEditors.findEditor('editor').setContent(html);
            })
    // eventos
        // keypress
            $(document).on("blur", ".txtTamañoImagen", function (e) {
                // cambiar el tamaño de la imagn
                $(".activeRichImage").css("width", $(this).val() + "%");
            })
        // click
            $(document).on("click", ".ckTamanio", function () {
                if ($(this).is(':checked')) {
                    div = $(this).parents(".divTamanioPersonalizado");
                    txt = div.find(".txtTamañoImagen");
                    txt.prop("disabled", false);
                    $(".activeRichImage").css("width", txt.val()+"%");
                } else {
                    div.find(".txtTamañoImagen").prop("disabled", true);
                    $(".activeRichImage").css("width", "");
                }
            })
            $(document).on("click", ".nicEdit-main", function () {
                $(".divTamanioPersonalizado").addClass("hidden");
            })
            $(document).on("click", ".nicEdit-main img", function (e) {
                //ancho = $(this).css("width");
                $(".activeRichImage").removeClass("activeRichImage");
                $(this).addClass("activeRichImage");
                if ($(this).attr("style") === undefined || $(this).attr("style") == "") {
                    mostrarTamanioPersonalizado();
                } else {
                    //var width = (100 * parseFloat(ancho) / parseFloat($(this).parent().css('width')));
                    var width   = getWidthPercent($(this));
                    width       = width.toFixed(2);
                    console.log(width);
                    editarTamanioPersonalizado(width);
                }
                
                e.stopPropagation();
            })
})