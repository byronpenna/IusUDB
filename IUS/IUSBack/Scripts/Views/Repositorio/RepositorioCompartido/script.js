$(document).ready(function () {
    // vista compartida
        $(document).on("click", ".icoCuadriculaUsuario", function () {

        
        })
        $(document).on("click", ".icoVistaCompartida", function () {
            var idUsuario = parseInt($(".txtUsuarioSeleccionado").val()), idVista = parseInt($(this).attr("id"));
            $(".icoVistaCompartida").removeClass("activeVista");
            $(this).addClass("activeVista");
            if ( (idUsuario == -1 && idVista == -1) || (idUsuario == -1 && idVista == 1) )  {
                // cuadricula raiz
                var frm = {};
                icoCompartidoBack(frm, idVista);
            } else if ((idUsuario != -1 && idVista == -1) || (idUsuario != -1 && idVista == 1) ) {
                // cuadricula usuario
                var shareSection    = $(this).parents(".shareSection");
                var seccion         = shareSection.find(".seccionCompartida");
                var frm = {
                    idUserFile:     seccion.find(".txtUsuarioSeleccionado").val(),
                    nombreCarpeta:  shareSection.find(".hUsuarioCarpeta").text(),
                    idVista:        idVista
                }
                console.log("Frm aqui es: ", frm);
                var seccion = targetSeccionCompartida; 
                divCarpetaUsuarioCompartido(frm, seccion);
            }
        })
    // tmp 
        $(document).on("keyup", ".txtBusquedaUsuarioDiv", function (e) {
            console.log("uhui");
            // variables
                var folders = $(".seccionCompartida .divCarpetaPublica");
            // do it 
                var charCode = e.which;
                if (charCode == 27) { // tecla esc cancela todo
                    $(this).val("");
                }
                var txt = $(this).val();
                if (txt == "") {
                    folders.removeClass("hidden");
                } else {
                    folders.addClass("hidden");
                    var foldersMostrados = folders.find(".tituloCarpetaPublica:containsi(" + txt + ")");
                    foldersMostrados = foldersMostrados.parents(".divCarpetaPublica");
                    foldersMostrados.removeClass("hidden");
                }
        })
    // plugins
        $(".cbUsuarios").chosen({ no_results_text: "No existe ese usuario", width: '100%' });
    //eventos
        // doble click
            $(document).on("dblclick", ".divCarpetaUsuarioCompartido", function (e) {
                var frm = {
                    idUserFile: $(this).find(".txtHdIdUsuario").val(),
                    nombreCarpeta: $(this).find(".tituloCarpetaPublica").text(),
                    idVista: $(this).parents(".seccionCompartida").find(".txtHdIdVista").val()
                }
                var seccion = targetSeccionCompartida; //$(this).parents(".seccionCompartida");
                $(".txtUsuarioSeleccionado").val(frm.idUserFile);

                divCarpetaUsuarioCompartido(frm,seccion);
            });
        // click
                $(document).on("click", ".carpetaDetalle", function (e) {
                    var nVista = getNumVistaActual();
                    console.log(nVista);
                    window.location = RAIZ + "RepositorioCompartido/index/" + $(this).find(".txtHdIdCarpeta").val() + "/" + nVista;
                })
                $(document).on("click", ".btnDejarDeCompartirTodo", function (e) {
                    var x           = confirm("¿Esta seguro que desea dejar de compartir ?");
                    var divUsuario = $(this).parents(".divCarpetaUsuarioCompartido ");
                    e.preventDefault();
                    if (x) {
                        frm = {
                            idUsuarioCompartido: divUsuario.find(".txtHdIdUsuario").val()
                        }
                        console.log("formulario a enviar",frm);
                        btnDejarDeCompartirTodo(frm);
                    }
                })
                $(document).on("keyup", ".txtBusquedaUsuario", function (e) {
                    var charCode = e.which;
                    if (charCode == 27) {
                        $(this).val("");
                    }
                    txtBusquedaUsuario($(this).val());
                })
            // busqueda 
                $(document).on("click", ".btnBusqueda", function () {
                    var vistaCuadricula = $(".iconoVistaCuadricula").hasClass("activeVista");
                    var vistaLista      = $(".icoVistaLista").hasClass("activeVista");
                    var target = "";
                    if (!$(this).hasClass("btnBuscando")) {
                        if (vistaCuadricula) {
                            var seccion = $(".cuadriculaView");
                            target = "cuadricula";
                        } else {
                            var seccion = $(".listView");
                            target = "lista";
                        }
                        var frm = {
                            txtBusqueda: $(".txtBusqueda").val()
                        }
                        btnBusqueda(frm, seccion, target);
                        $(this).addClass("btnBuscando");
                        $(this).empty().append("<i class='fa fa-times'></i>");
                    } else {
                        if (vistaCuadricula) {
                            var frm = {
                                idCarpeta: $(".txtHdIdCarpetaPadre").val()
                            }
                            var seccion = $(".cuadriculaView");
                            vista(frm, seccion,"cuadricula");
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        } else if (vistaLista) {
                            var frm = {
                                idCarpeta: $(".txtHdIdCarpetaPadre").val()
                            }
                            var seccionModificar = $(".listView");
                            vista(frm, seccionModificar,"lista");
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        }
                    }
                })
                $(document).on("click", ".icoCancelShare", function (e) {
                    e.preventDefault();
                    $(".nombreFileCompartir").empty();
                    $(".txtHdIdArchivoCompartir").val(-1);
                })
                $(document).on("click", ".icoDejarDeCompartir", function (e) {
                    e.preventDefault();
                    var seccion = $(this).parents(".divCarpetaPublica");
                    var frm = {
                        idArchivo: seccion.find(".txtHdIdArchivoCompartido").val()
                    }
                    var x = confirm("¿Esta seguro que desea dejar de compartir archivo?");
                
                    if (x) {
                        icoDejarDeCompartir(frm, seccion);
                    }
                })
                $(document).on("click", ".icoCompartidoBack", function () {
                    var frm = {};
                    //var idVista = $(".txtHdIdVista").val();
                    var idVista = $(".shareSection").find(".activeVista").attr("id");
                    console.log("VISTA AQUI ", idVista);
                    icoCompartidoBack(frm,idVista);
                });
                $(document).on("click", ".btnShareArchivo", function (e) {
                    var frm = {
                        idArchivo: $(".txtHdIdArchivoCompartir").val(),
                        idUsuario: $(".cbUsuarios").val(),
                        nombreCarpeta: $(".cbUsuarios option:selected").text()
                    }
                
                    if (frm.idUsuario != -1) {
                        if (frm.idArchivo != -1) {
                            btnShareArchivo(frm)
                        }
                        else {
                            printMessage($(".divMessageCompartir"), "Por favor seleccione un archivo para compartir", false);
                        }
                    } else {
                        printMessage($(".divMessageCompartir"), "Por favor seleccione un usuario para compartir", false);
                    }
                
                })
                $(document).on("click", ".icoCompartirFile", function (e) {
                    e.preventDefault();
                    //var folder = $(this).parents(".folder");
                    var folder = $(this).parents(".folderUni");
                    icoCompartirFile(folder);
                })
                $(document).on("click", ".cuadritoCarpeta", function () {
                var estado = $(this).attr("id");
                if (estado != '0') {
                    window.location = RAIZ + "RepositorioCompartido/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                }
            });
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                
                e.cancelBubble = true;
                var folder = $(this).parents(".folder");
                printMessage(folder.find(".divMensajeCarpeta"), "En esta pestaña no se permite editar el nombre de los archivos y carpetas", false)
            })
        
})