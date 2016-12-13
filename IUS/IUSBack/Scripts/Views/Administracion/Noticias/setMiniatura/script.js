$(document).ready(function () {
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
                frm.imgAlto = frm.imgAlto / $(".imgThumbnail").height();
                frm.imgAncho = frm.imgAncho / $(".imgThumbnail").width();
                frm.x = frm.x / $(".imgThumbnail").width();
                frm.y = frm.y / $(".imgThumbnail").height();
                formulario = $(this);
                data = getObjFormData(files, frm);
                e.preventDefault();
                //jcrop_api.destroy();
                getImageFromInputFile($("#flMiniatura")[0].files[0], function (imagen) {
                    console.log(imagen.width, imagen.height);
                    //imagen.width == imagen.height ||
                    if ((frm.imgAlto > 0 && frm.imgAncho > 0 && frm.imgAncho > 0)) {
                        frmMiniatura(data, formulario.attr("action"),imagen);
                    } else {
                        printMessage($(".divResultado"), "Por favor seleccione un area de recorte", false);
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
                $(".divAyudaRecorte").hide();
                console.log("asdqw");
                var cambiar = false;
                if ($(this).val() == "") { // no selecciono imagen
                    boton.prop("disabled", true);
                    $(this).prev().empty().append("Seleccionar imagen");
                    e.preventDefault();
                } else {
                    cambiar = true;
                    //****************************************
                    
                    $(this).prev().empty().append("Cambiar imagen");
                    $(".divLoadingPhoto").empty().append("<img class='imgLoading' src='" + IMG_GENERALES + "ajax-loader.gif" + "'>");
                    boton.prop("disabled", false);
                }
                var targetImg = $(".imgThumbnail");
                if (cambiar) {
                    getImageFromInputFileEvent(e, function (images) {
                        $(".divLoadingPhoto").empty();
                        if (images !== undefined && images != null) {
                            targetImg.attr("src", images.src);

                            printMessage($(".divAyudaRecorte"), "Seleccione un area de recorte",true,7000);
                            console.log("MIRA QUE SON 7 segundos");


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
                    targetImg.attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/noBanerMiniatura.png");
                    targetImg.attr("style", "");
                    $(".divLoadingPhoto").empty();
                }
                
            });
            
})