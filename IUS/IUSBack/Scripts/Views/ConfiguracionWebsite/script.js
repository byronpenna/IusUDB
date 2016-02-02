$(document).ready(function () {
    // plugins 
        // tabs 
            $('#horizontalTab').responsiveTabs();
        
    // eventos 
        // chage 
            $(document).on("change", ".flImagen", function (e) {
                var divLoading = $(".divImgSlideLoading"); var boton = $(".btnSubir");
                var targetImg   = $(".imgSlide");
                var cambiar     = false;
                if ($(this).val() == "") {
                    boton.prop("disabled", true);
                } else {
                    cambiar = true;
                    divLoading.empty().append("<img class='imgLoading' src='" + IMG_GENERALES + "ajax-loader.gif" + "'>");
                    boton.prop("disabled", false);
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
                            jcrop_api = $.Jcrop('.imgSlide', {
                                //setSelect: [0, 0, 1745, 643],
                                onSelect: storeCoords,
                                onChange: storeCoords,
                                allowSelect: true,
                                aspectRatio: 300 / 100,
                                allowResize: false,
                                onRelease: releaseCheck
                            });
                            jcrop_api.enable();
                            console.log("Mirar que pasa aqui");
                            inicialFoto();
                        }
                    })
                } else {
                    //
                    if (jcrop_api != null) {
                        jcrop_api.destroy();
                    }
                    targetImg.attr("src", IMG_GENERALES + "noimage.png");
                    targetImg.attr("style", "");
                }
            })
        // keydown
            $(document).on("keydown", ".txtValores", function (e) {
                var charcode = e.which;
                switch (charcode) {
                    case 13: {
                        $(".btnAddValor").click();
                        break;
                    }
                }
            })
        // submit
            $(document).on("submit", "#frmInstitucional", function (e) {
                e.preventDefault();
                frm = serializeToJson($(this).serializeArray());
                console.log("Formulario a enviar es: ", frm);
                frmInstitucional(frm);
            });
            $(document).on("submit", "#frm", function (e) { // imagen
                var files = $("#file1")[0].files
                //try{
                    //e.preventDefault();
                    var frm = getFrmSlide();
                    data = getObjFormData(files,frm);
                    //console.log("El valor del frm es", frm);
                    e.preventDefault();
                    $("#div_carga").show();
                    section = $(this);
                    getImageFromInputFile($("#file1")[0].files[0], function (imagen) {
                        formularioSubir(data, section.attr("action"), section, imagen);
                    });
                    
                //} catch (error) {
                    /*console.log("error get data", error);
                    alert(error.message);*/
                //}
                
            });
        // click
            $(document).on("click", ".btnEliminarImage", function () {
                var x = confirm("¿Esta seguro que desea eliminar esta imagen?")
                if (x) {
                    btnEliminarImage($(this).parents(".divImgIndividual"))
                }
            });
            $(document).on("click", ".btnDeshabilitarSliderImage", function () {
                var x = confirm("Esta seguro que desea cambiar estado de imagen");
                if (x) {
                    btnDeshabilitarSliderImage($(this).parents(".divImgIndividual"), $(this));
                }
            })
            $(document).on("click", ".iconQuitarValor", function () {
                var x = confirm("¿Esta seguro de quitar valor?");
                tr = $(this).parents("tr");
                if (x) {
                    iconQuitarValor(tr);
                }
                
            });
            $(document).on("click", ".btnAddValor", function () {
                frmSection = $(this).parents(".agregarValorSection");
                frm = serializeSection(frmSection);
                console.log("formulario a agregar", frm);
                var seccion = $(".divMensajesAdd");
                var val = validacionAddValor(frm);
                if (val.estado) {
                    btnAddValor(frm,seccion);
                } else {
                    printAllMessageWithOutTable(seccion, val.campos);
                }
                
            })
});