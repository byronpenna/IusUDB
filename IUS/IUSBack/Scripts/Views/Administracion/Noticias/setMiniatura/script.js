﻿$(document).ready(function () {
    // plugins
        // jcrop
            
            /* jcrop_api = $.Jcrop('.imgThumbnail', {
                onSelect: storeCoords,
                onChange: storeCoords,
                aspectRatio: 1
            });*/
    // eventos 
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                console.log("files es D: ",files);
                frm = serializeToJson($(this).serializeArray());
                frm.imgAlto = frm.imgAlto / $(".imgThumbnail").width();
                frm.imgAncho = frm.imgAncho / $(".imgThumbnail").height();
                frm.x = frm.x / $(".imgThumbnail").width();
                frm.y = frm.y / $(".imgThumbnail").height();
                formulario = $(this);
                data = getObjFormData(files, frm);
                e.preventDefault();
                //jcrop_api.destroy();
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    console.log(imagen.width, imagen.height);
                    if (imagen.width == imagen.height || (frm.imgAlto > 0 && frm.imgAncho > 0 && frm.imgAncho > 0)) {
                        frmMiniatura(data, formulario.attr("action"),imagen);
                    } else {
                        printMessage($(".divResultado"), "La imagen debe ser cuadrada", false);
                    }
                })
                /*
                var oFReader = new FileReader();
                oFReader.readAsDataURL($("#flMiniatura")[0].files[0]);

                oFReader.onload = function (oFREvent) {
                    //document.getElementById("flMiniatura").src = oFREvent.target.result;
                    console.log($(this));
                    imagen = new Image();
                    imagen.src = $(this)[0].result;
                    console.log(imagen.width);

                };*/
                
                //console.log("la data e enviar es ",data);
                //frmMiniatura(data, $(this).attr("action"));
            })
        // change 
            $(document).on("change", "#flMiniatura", function (e) {
                var boton = $(".botonSubir");
                console.log("asdqw");
                var cambiar = false;
                if ($(this).val() == "") {
                    boton.prop("disabled", true);
                    e.preventDefault();
                } else {
                    cambiar = true;
                    $(".divLoadingPhoto").empty().append("<img class='imgLoading' src='" + IMG_GENERALES + "ajax-loader.gif" + "'>");
                    boton.prop("disabled", false);
                }
                var targetImg = $(".imgThumbnail");
                if (cambiar) {
                    getImageFromInputFileEvent(e, function (images) {
                        $(".divLoadingPhoto").empty();
                        if (images !== undefined && images != null) {
                            targetImg.attr("src", images.src);
                            targetImg.attr("style", "");
                            if (jcrop_api != null) {
                                jcrop_api.destroy();
                            }
                            jcrop_api = $.Jcrop('.imgThumbnail', {
                                onSelect: storeCoords,
                                onChange: storeCoords,
                                aspectRatio: 300 / 100
                            });
                            inicialFoto();
                        }
                    })
                } else {
                    if (jcrop_api != null) {
                        jcrop_api.destroy();
                    }
                    targetImg.attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/noimage.png");
                    targetImg.attr("style", "");
                    $(".divLoadingPhoto").empty();
                }
                
            });
            
})