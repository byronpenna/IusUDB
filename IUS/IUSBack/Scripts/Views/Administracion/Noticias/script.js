$(document).ready(function () {
    //nicEditors.findEditor('editor').getContent();
    
    //plugins 
        // chosen 
            $(".cbCategorias").chosen();
        // rich text 
            bkLib.onDomLoaded(function () {
                //nicEditors.allTextAreas()
                txtAreaEditor = new nicEditor({ fullPanel: true }).panelInstance('editor');
                //txtAreaEditor = new nicEditor({ maxHeight: 400 }).panelInstance('editor');
                //html = "<img src='http://www.matrallune.com/images/imagen_corporativa.jpg' alt=' align='none' class='activeRichImage'>"
                //nicEditors.findEditor('editor').setContent(html);
            })
            $(document).on("click", ".nicEdit-main", function () {
                cleanActive();
                //console.log("Vamos a remover");
            })
            //*********************************************************
            $(".prd").draggable({
                start: function () {
                    console.log("start");
                },
                drag: function () {
                    console.log("drag");
                },
                stop: function () {
                    console.log("stop");
                }
            });
            //*********************************************************
            $(document).on("click", ".nicEdit-main img", function (e) {
                cleanActive();
                var img = $(this);//.find("img");
                e.stopPropagation();
                $(".activeRichImage").removeClass("activeRichImage");
                //if (img.length) {
                    var padre = img.parent();
                    padre.attr("contenteditable", "false");
                    //padre.css("display", "block");
                    //var x = img.wrap("<div></div>");
                    img.css("display", "block");
                    img.before("<div class='areaImgRich' style='width:" + img.width() + "px;height:" + img.height() + "px;'>\
                        <div class='pointRichImage plu'></div>\
                        <div class='pointRichImage prd'></div>\
                        <div class='pointRichImage prdi'></div>\
                    </div>");
                    img.addClass("activeRichImage");
                //}
                    $(".prdi").draggable({
                        start: function (e) {
                            //e.preventDefault();
                            console.log("start");
                            console.log("la posicion inicial es: ", $(this).position());
                        },
                        drag: function () {
                            e.preventDefault();
                            console.log("drag");
                        },
                        stop: function () {
                            e.preventDefault();
                            console.log("stop");
                            var moveX = $(this).position().left;
                            var zona = $(".nicEdit-main").width() - $(".nicEdit-main").position().left;
                            var imagenActiva = $(".activeRichImage");
                            console.log("move x es ", moveX);
                            console.log("zona ", zona);
                            
                            //if(moveX - $(".nicEdit-main").position().left > 0 && moveX - $(".nicEdit-main").position().left < zona){
                            if (moveX - $(".nicEdit-main").position().left > 0 && moveX < zona) {
                                console.log("Rango");
                                var porcentaje = moveX / zona * 100;
                                console.log("El porcentaje es: ", porcentaje);
                                $(".activeRichImage").css("width", porcentaje + "%");
                                imagenActiva.click();
                            }else{
                                console.log("fuera de Rango");
                            }
                            console.log("la posicion final es: ", $(".nicEdit-main").position());
                        }
                    });
            })
            /*var nicExampleOptions = {
                buttons: {
                    'example': { name: __('Example'), type: 'nicEditorExampleButton' }
                }
            };

            var nicEditorExampleButton = nicEditorButton.extend({
                init: function () {
                    // your init code
                },
                mouseClick: function () {
                    alert('hallo!'); // Your code here
                }
            });*/

            //nicEditors.registerPlugin(nicPlugin, nicExampleOptions);
    // eventos
        // change
            $(document).on("change", ".flSubirImagenes", function (e) {
                var files = e.target.files;
                console.log("files", files);
                e.preventDefault();
                $("#list").empty();
                $.each(files, function (i, file) {
                    var reader = new FileReader();
                    reader.onload = (function (theFile) {
                        return function (e) {
                            strImage = "<img style='margin-top:5%;' class='thumbPost fullSize pointer' id='" + i + "' src='" + e.target.result + "'/>";
                            $(".divImagesPost").append(strImage);
                        };
                    })(file)
                    reader.readAsDataURL(file);
                })
            })
        // blur
            $(document).on("blur", ".txtTamañoImagen", function (e) {
                // cambiar el tamaño de la imagn
                $(".activeRichImage").css("width", $(this).val() + "%");
            })
        // submit 
            $(document).on("submit", ".frmNoticia", function (e) {
                var formulario = $(this);
                e.preventDefault();
                //--------------
                // capturando formulario
                    formulario = serializeToJson(formulario.serializeArray());
                    formulario.contenido = nicEditors.findEditor('editor').getContent();
                    formulario.tags = $(".txtEtiquetas").val();
                var val = validarInsert(formulario);
                console.log("Val es", val);
                $(".divResultado").addClass("hidden");
                if (val.estado) {
                    $("#div_carga").fadeIn(400, function () {
                        if ($("#btnSubmitNoticia").attr("action") == 1) {
                            //console.log("Formulario a agregar",formulario);
                            frmNoticia(formulario);
                            //console.log("Ingresaras");
                        } else {
                            updatePost(formulario);
                        }
                    });
                } else {
                    var errores;
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        var divResultado = $(".frmNoticia").find("." + i).parents(".contenedorControl").find(".divResultado")
                        if (val.length > 0) {
                            console.log("entro");
                            divResultado.removeClass("hidden");
                            $.each(val, function (i, val) {
                                errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                            })
                            divResultado.empty().append(errores);
                        }
                    })
                    /*
                    var div = "";
                    $.each(val.general, function (i, val) {
                        div += "<div class='row marginNull'>";
                        div += getSpanMessageError(val);
                        div += "</div>";
                    })
                    printMessageDiv($(".divMensajesGenerales"), div);
                    */
                }
                
            })
        // click
            /*$(document).on("click", ".ckTamanio", function () {
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
            })*/
            /*$(document).on("click", ".nicEdit-main img", function (e) {
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
            })*/
})