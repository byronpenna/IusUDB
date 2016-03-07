$(document).ready(function () {
    // eventos
        //keydown
            $(document).on("keydown", ".txtNombreCarpetaSave", function (e) {
                var charCode = e.which;
                var folder = $(this).parents(".folder");
                switch (charCode) {
                    case 13: {
                        folder.find(".btnGuardarCarpeta").click();
                        break;
                    }
                }
            })
            $(document).on("keydown", ".txtNombreCarpeta", function (e) {
                var charCode = e.which;
                var folder = $(this).parents(".folder");
                switch (charCode) {
                    case 13: {
                        folder.find(".btnEditarCarpeta").click();
                        break;
                    }
                }
            })
        //click 
            // Busqueda
                $(document).on("click", ".btnBusqueda", function () {
                    var vistaCuadricula = $(".iconoVistaCuadricula").hasClass("activeVista");
                    var vistaLista = $(".icoVistaLista").hasClass("activeVista");
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
                            verCuadricula();
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        } else if (vistaLista) {
                            var frm = {
                                idCarpeta: $(".txtHdIdCarpetaPadre").val()
                            }
                            var seccionModificar = $(".listView");
                            verLista();
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        }
                    }

                })
                $(document).on("click", ".btnEditarArchivo", function (e) {
                    var seccion = $(this).parents(".folder");
                    var frm = {
                        idArchivo: seccion.find(".txtHdIdArchivoPublico").val(),
                        nombreArchivo: seccion.find(".txtNombreCarpeta").val()
                    }
                    //console.log(frm);
                    btnEditarArchivo(frm, seccion);
                });
                $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                    div = $(this).parents(".folder");
                    div.remove();
                })
                /*
                $(document).on("click", ".iconoVista", function (e) {
                    e.preventDefault();
                    if ($(this).hasClass("iconoVistaCuadricula")) {
                        verCuadricula();
                    } else if ($(this).hasClass("icoVistaLista")) {
                        verLista();
                    }  
                })*/
            //
                $(document).on("click", ".icoNuevaCarpeta", function (e) {
                    //console.log("D: D: D: D:")
                    e.preventDefault();
                    div = getDivNewFolder();
                    if ($(".cuadriculaView").hasClass("hidden")) {
                        verCuadricula(function () {
                            $(".cuadriculaView").prepend(div);
                        })
                    } else {
                        $(".cuadriculaView").prepend(div);
                    }
                    
                })
            // eliminar carpeta 
                $(document).on("click", ".icoEliminarArchivo", function (e) {
                    e.preventDefault();
                    var x = confirm("¿Esta seguro que desea dejar de compartir este archivo?");
                    if (x) {
                        var seccion = $(this).parents(".folder");
                        var frm = {
                            idArchivoPublico: $(".txtHdIdArchivoPublico").val()
                        }
                        icoEliminarArchivo(frm,seccion);
                    }
                })
            // entrar a carpeta
                $(document).on("click", ".carpetaDetalle", function (e) {
                    var nVista = getNumVistaActual();
                    window.location = RAIZ + "RepositorioPublico/index/" + $(this).find(".txtHdIdCarpeta").val() + "/" + nVista;
                })
                $(document).on("click", ".cuadritoCarpeta", function (e) {
                    e.cancelBubble = true;
                    var estado = $(this).attr("id");
                    if (estado != '0') {
                        console.log("n vista es",nVista);
                        window.location = RAIZ + "RepositorioPublico/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                    }
                });
            // guardar carpeta
                $(document).on("click", ".btnGuardarCarpeta", function (e) {
                    seccion = $(this).parents(".folder");
                    frm = {
                        idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                        nombre: seccion.find(".txtNombreCarpetaSave").val()
                    }
                    console.log(frm);
                    if (frm.nombre != "") {
                        btnGuardarCarpeta(frm, seccion);
                    } else {
                        printMessage($(".mensajeNewFolder"), "El nombre de la carpeta no puede ser vacio", false);
                    }
                    
                })
            // cambiar nombre a carpeta
                $(document).on("click", ".btnEditarCarpeta", function (e) {
                    folder = $(this).parents(".folder");
                    frm = {
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtNombreCarpeta").val()
                    }
                    btnEditarCarpeta(frm, folder);
                })
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    seccion = $(this).parents(".detalleCarpeta");
                    btnCancelarEdicionCarpeta(seccion);
                })
                $(document).on("click", ".icoEliminarCarpeta", function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    seccion = $(this).parents(".folder");
                    frm = { idCarpeta: seccion.find(".txtHdIdCarpeta").val() }
                    var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                    if (x) {
                        icoEliminarCarpeta(frm, seccion);
                    }

                });
                $(document).on("click", ".sinRedirect", function (e) {
                    e.stopPropagation();
                })
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                e.cancelBubble = true;
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
})