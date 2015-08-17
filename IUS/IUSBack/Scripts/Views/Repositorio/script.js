$(document).ready(function () {
    // eventos 
    /*
    $(window).bind("popstate", function (e) {
        console.log("set back");
    })*/
        loadPublicFiles();            
        // change 
            
        // keyup 
            $(document).on("keyup", ".txtNombreFileCompartir", function (e) {
                /*if ($(this).val() == "") {
                    $(".divCarpetasPublicasCompartir .divCarpetaPublica").removeClass("hidden");
                } else {
                    $(".divCarpetasPublicasCompartir .divCarpetaPublica").addClass("hidden");
                    var folders = $(".divCarpetaPublica .tituloCarpetaPublica:containsi(" + $(this).val() + ")");
                    folders = folders.parents(".divCarpetaPublica");
                    folders.removeClass("hidden");
                }*/
            })
        // keydown
            
            $(document).on("keydown", ".txtRutaPublica", function (e) {
                switch (e.which) {
                    case 13: {
                        $(".spIrPublico").click();
                        break;
                    }
                }
            })

            $(document).on("keydown", ".txtNombreArchivo", function (e) {
                
                switch (e.which) {
                    case 13: {
                        var folder = $(this).parents(".folder");
                        var frm = {
                            idArchivo:seccion.find(".txtHdIdArchivo").val(),
                            nombreArchivo: seccion.find(".txtNombreCarpeta").val()
                        }
                        btnEditarArchivo(frm,folder);
                        break;
                    }
                }
            })
            $(document).on("keydown", ".txtBusqueda ", function (e) {
                switch (e.which) {
                    case 13: {
                        if ($(".rdBusqueda:checked").val() == 1) {
                            $(".btnBusqueda").click();
                        }
                    }
                }
            })
            $(document).on("keydown", ".txtNombreCarpeta", function (e) {
                switch (e.which) {
                    case 13: {
                        var folder = $(this).parents(".folder");
                        if ($(this).hasClass("txtNombreArchivo"))
                        {
                            //editarArchivo(folder);
                            folder.find(".btnEditarArchivo").click();
                        } else {
                            //editarFolder(folder);
                            folder.find(".btnEditarCarpeta").click();
                        }
                        break;
                    }
                    case 27: {
                        seccion = $(this).parents(".detalleCarpeta");
                        btnCancelarEdicionCarpeta(seccion);
                        break;
                    }
                }
                
                
            });
            $(document).on("keydown", ".txtNombreCarpetaDetalle", function (e) {
                var me = $(this);
                var seccion = $(this).parents(".folderDetalles");
                switch (e.which) {
                    case 13: {
                        if ($(this).hasClass(".txtNombreArchivoDetalle")) {
                            var frm = {
                                txtHdIdCarpeta: seccion.find(".txtHdIdCarpeta").val(),
                                nombre: me.val()
                            }
                            
                            txtNombreCarpetaDetalle(frm, seccion);
                        }
                        else {
                            
                            var frm = {
                                idArchivo: seccion.find(".txtHdIdArchivo").val(),
                                nombreArchivo: seccion.find(".txtNombreArchivoDetalle").val()
                            }
                            console.log("formulario a enviar", frm);
                            txtNombreArchivoDetalle(frm,seccion);
                        }
                        break;
                    }
                }
            })
            $(document).on("keydown", ".txtNombreCarpetaSave", function (e) {
                switch (e.which) {
                    case 13: {
                        $(".btnGuardarCarpeta").click();
                    }
                }
            });
            
        // submit 
            $(document).on("submit", "#frmSubir", function (e) {
                
                files = $("#flArchivos")[0].files;
                formulariohtml = $(this);
                frm = { txtHdIdCarpetaPadre: $(".txtHdIdCarpetaPadre").val() };
                
                e.preventDefault();
                
                if (frm.txtHdIdCarpetaPadre != -1 || frm.txtHdIdCarpetaPadre != '-1') {
                    cn = 0;
                    totalFiles = files.length;
                    $(".imgCargando").find("img").removeClass("hidden");
                    $(".tbArchivos").empty();
                    $(".porcentajeCarga").empty();
                    $.each(files, function (file) {
                        frm.cn = cn;
                        data = getIndividualFormData(files[cn], frm);
                        frmSubir(data, formulariohtml.attr("action"), totalFiles);
                        cn++;
                    });
                    
                } else {
                    alert("No se pueden subir ficheros a este directorio");
                }
                /**/
                
                
            })
        // click 
                $(document).on("click", ".spIrPublico", function () {
                    frm = { txtRuta: $(".txtRutaPublica").val() }
                    console.log("formulario a enviar", frm);
                    spIrPublico(frm);
                })
                $(document).on("click", ".closeModal", function (e) {
                    $(".divUpload").click();
                })
            // vista
                $(document).on("click", ".iconoVistaCuadricula", function (e) {
                    e.preventDefault();
                    if (!isSearch()) {
                        verCuadricula($(this));
                    } else {
                        vistaCuadriculaBusqueda();
                    }
                    
                })
                $(document).on("click", ".icoVistaLista", function (e) {
                    e.preventDefault();
                    if (!isSearch()) {
                        console.log("No buscando");
                        verLista($(this));
                    } else {
                        console.log("buscando");
                        vistaListaBusqueda();
                    }
                    
                })
            // publico 
                $(document).on("click", ".icoPublicoBack", function (e) {
                    frm = { idCarpetaPublica: $(".txtHdCarpetaPadrePublica").val() }
                    icoPublicoBack(frm);
                })
                $(document).on("dblclick", ".divCarpetaPublica", function (e) {
                    frm = { idCarpetaPublica: $(this).find(".txtHdIdCarpetaPublica").val() }
                    //seccion = $(this).parents(".divCarpetasOpciones").find(".txtHdCarpetaPadrePublica").val(frm.idCarpetaPublica);
                    divCarpetaPublica(frm);
                })
            // repositorio compartido
                    $(document).on("click", ".icoCompartirFile", function (e) {
                        e.preventDefault();
                        folder = $(this).parents(".folderUni");
                        initShareFile(folder);

                    })
                    $(document).on("click", ".btnCompartir", function () {
                        seccion = $(this).parents(".shareSection");
                        frm = serializeSection(seccion);
                        console.log(frm);
                        if (frm.txtHdCarpetaPadrePublica != "-1") {
                            if ($(".txtHdIdArchivoCompartir").val() == -1) {
                                printMessage($(".divMensajeRepoPublico"), "Seleccione un archivo a compartir", false);
                            }
                            btnCompartir(frm, seccion);
                        } else {
                            //alert("No se puede compartir en este directorio");
                            printMessage($(".divMensajeRepoPublico"), "No se puede compartir en este directorio", false);
                            //$(".divMensajeRepoPublico").empty().append("<span class='failMessage'>No se puede compartir en este directorio</span>");
                        }
                        
                    })
            // repositorio privado      
                    
                $(document).on("click", ".icoCancelShare", function (e) {
                    e.preventDefault();
                    $(".nombreFileCompartir").empty();
                    $(".txtNombreFileCompartir").val("");
                    $(".txtHdIdArchivoCompartir").val(-1);

                })
                $(document).on("click", ".carpetaDetalle", function (e) {
                    var nVista = getNumVistaActual();
                    window.location = RAIZ + "Repositorio/index/" + $(this).find(".txtHdIdCarpeta").val() + "/" + nVista;

                })
                $(document).on("click", ".cuadritoCarpeta", function () {
                    /*frm = { idCarpeta: $(this).parents(".folder").find(".txtHdIdCarpeta").val() }
                    cuadritoCarpeta(frm);*/
                    console.log("Cuadrito carpeta click");
                    var estado = $(this).attr("id");
                    var nVista = getNumVistaActual();
                    //console.log("vista actual es:", nVista);
                    if (estado != '0') {
                        window.location = RAIZ + "Repositorio/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val()+"/"+nVista;
                    }
                    //console.log("vas a redireccionar");
                });
                $(document).on("click", ".icoOpenLocation", function (e) {
                    e.preventDefault();
                    console.log("Iras a abrir")
                    var seccion = $(this).parents(".folder");
                    window.location = RAIZ + "Repositorio/index/" + seccion.find(".txtHdIdCarpetaContenedora").val();
                });
                
                $(document).on("click", ".btnBusqueda", function () {
                    var vistaCuadricula = $(".iconoVistaCuadricula").hasClass("activeVista");
                    var vistaLista      = $(".icoVistaLista").hasClass("activeVista");
                    var target          = "";
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
                        btnBusqueda(frm, seccion,target);
                        $(this).addClass("btnBuscando");
                        $(this).empty().append("<i class='fa fa-times'></i>");
                    } else {
                        if (vistaCuadricula) {
                            var frm = {
                                idCarpeta: $(".txtHdIdCarpetaPadre").val()
                            }
                            var seccion = $(".cuadriculaView");
                            iconoVistaCuadricula(frm, seccion);
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        } else if (vistaLista) {
                            var frm = {
                                idCarpeta: $(".txtHdIdCarpetaPadre").val()
                            }
                            var seccionModificar = $(".listView");
                            icoVistaLista(frm, seccionModificar);
                            $(this).removeClass("btnBuscando");
                            $(this).empty().append("<i class='fa fa-search'></i>");
                        }
                    }
                    
                })
                // eliminar archivos 
                    $(document).on("click", ".icoEliminarArchivo", function (e) {
                        e.preventDefault();
                        var x = confirm("Esta seguro que desea eliminar este archivo");
                        var classPadre = "";
                        if (x) {
                            
                            var vista = getVistaActual();
                            switch (vista) {
                                case "cuadricula": {
                                    classPadre = ".folder";
                                    break;
                                }
                                case "lista": {
                                    classPadre = ".folderDetalles";
                                    break;
                                }
                            }
                            seccion = $(this).parents(classPadre);
                            frm = { idArchivo: seccion.find(".txtHdIdArchivo").val() }
                            console.log("formulario es:", frm);
                            icoEliminarArchivo(frm,seccion);
                        }
                    })
                // cambiar nombre archivo 
                    $(document).on("click", ".btnEditarArchivo", function (e) {
                        var seccion = $(this).parents(".folder");
                        var frm = {
                            idArchivo:      seccion.find(".txtHdIdArchivo").val(),
                            nombreArchivo:  seccion.find(".txtNombreCarpeta").val()
                        }
                        btnEditarArchivo(frm,seccion);
                    })
                // herramientas carpetas
                    $(document).on("click", ".icoNuevaCarpeta", function (e) {
                        e.preventDefault();
                        if ($(".cuadriculaView").hasClass("hidden")) {
                            verCuadricula($(".iconoVistaCuadricula"), function () {
                                div = getDivNewFolder();
                                $(".cuadriculaView").prepend(div);
                            });
                        } else {
                            div = getDivNewFolder();
                            $(".cuadriculaView").prepend(div);
                        }
                    })
                    $(document).on("click", ".icoSubirFichero", function (e) {
                        e.preventDefault();
                        $(".divUpload").fadeIn(400, function () {
                        
                        });
                    })
                // subir archivos 
                    $(document).on("click", ".divUpload", function (e) {
                        if ($(".txtHdEstadoUpload").val() == 1 || $(".txtHdEstadoUpload").val() == 1) {
                            window.location = RAIZ + "Repositorio/index/" + $(".txtHdIdCarpetaPadre").val();
                        }
                        $(this).fadeOut();
                    })
                    $(document).on("click", ".contenedorUpload", function (e) {
                        e.stopPropagation();
                    });
                // guardar carpeta
                    $(document).on("click", ".btnGuardarCarpeta", function (e) {
                        seccion = $(this).parents(".folder");
                        frm = {
                                idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                                nombre: seccion.find(".txtNombreCarpetaSave").val()
                        }
                        if (frm.nombre != "") {
                            btnGuardarCarpeta(frm, seccion);
                        } else {
                            printMessage($(".mensajeNewFolder"), "No puede dejar vacio nombre de carpeta", false);
                        }
                        
                    });
                    /*
                    $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                        div = $(this).parents(".folder");
                        div.remove();
                    })*/
                // eliminar carpeta
                    $(document).on("click", ".icoEliminarCarpeta", function (e) {
                        e.preventDefault();
                        seccion = $(this).parents(".folder");
                        frm = { idCarpeta: seccion.find(".txtHdIdCarpeta").val() }
                        var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                        if (x) {
                            icoEliminarCarpeta(frm, seccion);
                        }
                    
                    });
                // actualizar carpeta
                    $(document).on("click", ".btnEditarCarpeta", function () {
                        folder = $(this).parents(".folder");
                        frm = {
                            txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                            nombre: folder.find(".txtNombreCarpeta").val()
                        }
                        console.log("formulario es: ", frm);
                        if (frm.nombre != "") {
                            btnEditarCarpeta(frm, folder);
                        } else {
                            printMessage(folder.find(".mensajeFolder"), "La carpeta no puede ser vacia", false);
                        }
                    });
                    $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                        seccion = $(this).parents(".detalleCarpeta");
                        btnCancelarEdicionCarpeta(seccion);
                    });
                
        // doble click
                $(document).on("dblclick", ".spanNombreCarpeta", function (e) {
                    var seccion = $(this).parents(".folderDetalles");
                    seccion.find(".txtNombreCarpetaDetalle").val(seccion.find(".spanNombreCarpeta").text());
                    controlesEdit(true, seccion);
                })
                $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                    e.cancelBubble = true;
                    seccion = $(this).parents(".detalleCarpeta");
                    nombre = $(this).text();
                    ttlNombreCarpeta(seccion, nombre);
                })
})