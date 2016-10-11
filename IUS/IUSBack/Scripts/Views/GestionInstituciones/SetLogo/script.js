$(document).ready(function () {
    // eventos
        /*// change
            $(document).on("change", "#flMiniatura", function () {
                if ($(this).val() == "") {
                    $(".btnSubir").prop("disabled", true);
                } else {
                    $(".btnSubir").prop("disabled", false);
                }
            })*/
        // plugins 
            var jcrop_api = null;
        // change 
                $(document).on("change", ".flFoto", function (e) {
                    // vars 
                    var divLoading  = $(".divLoadingPhoto"), boton = $(".btnSubir"), cambiar = false;
                    var targetImg   = $(".imgThumbnail");
                    console.log("cambio foto");
                    // do it 
                    if ($(this).val() == "") {
                        boton.prop("disabled", true);
                    } else {
                        divLoading.empty().append("<img class='imgLoading' src='" + IMG_GENERALES + "ajax-loader.gif" + "'>");
                        boton.prop("disabled", false);
                        cambiar = true;
                    }
                    if (cambiar) {
                        getImageFromInputFileEvent(e, function (images) {
                            divLoading.empty();
                            if (images !== undefined && images != null) {
                                targetImg.attr("src", images.src);
                                targetImg.attr("style", "");
                                if (jcrop_api != null) {
                                    jcrop_api.destroy();
                                }
                                jcrop_api = $.Jcrop(".imgThumbnail", {
                                    onSelect: storeCoords,
                                    onChange: storeCoords,
                                    aspectRatio: 7 / 9
                                });
                            }
                        })
                    } else {
                        if (jcrop_api != null) {
                            jcrop_api.destroy();
                        }
                        targetImg.attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                        targetImg.attr("style", "");
                        $(".divLoadingPhoto").empty();
                    }
                })
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                frm = serializeToJson($(this).serializeArray());
                formulario = $(this);
                frm = setFrmCoords(frm, $(".imgThumbnail"));
                data = getObjFormData(files, frm);
                e.preventDefault();
                console.log("frm enviado es", frm);
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    if (imagen.width == imagen.height || (frm.imgAlto > 0 && frm.imgAncho > 0 && frm.imgAncho > 0)) {
                        frmMiniatura(data, formulario.attr("action"), imagen, jcrop_api);
                    } else {
                        printMessage($(".divResultado"), "La imagen debe ser cuadrada", false);
                    }
                });
            })
        // click
            
})