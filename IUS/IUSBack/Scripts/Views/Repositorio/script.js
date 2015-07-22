﻿$(document).ready(function () {
    // eventos 
    /*
    $(window).bind("popstate", function (e) {
        console.log("set back");
    })*/
        loadPublicFiles();            
        // change 
            $(document).on("change", ".rdBusqueda", function () {
                if ($(this).val() == 0) {
                    $(".btnBusqueda").addClass("hidden");
                    $(".divBusquedaArchivos").removeClass("input-group");
                    buscarEnCarpeta($(".txtBusqueda").val());
                } else {
                    $(".divBusquedaArchivos").addClass("input-group");
                    $(".btnBusqueda").removeClass("hidden");
                    $(".folders .folder").removeClass("hidden");
                }
            })
        // keyup 
            $(document).on("keyup", ".txtNombreFileCompartir", function (e) {
                if ($(this).val() == "") {
                    $(".divCarpetasPublicasCompartir .divCarpetaPublica").removeClass("hidden");
                } else {
                    $(".divCarpetasPublicasCompartir .divCarpetaPublica").addClass("hidden");
                    var folders = $(".divCarpetaPublica .tituloCarpetaPublica:containsi(" + $(this).val() + ")");
                    folders = folders.parents(".divCarpetaPublica");
                    folders.removeClass("hidden");
                }
            })
        // keydown
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
            
            $(document).on("keydown", ".txtNombreCarpeta", function (e) {
                switch (e.which) {
                    case 13: {
                        var folder = $(this).parents(".folder");
                        if ($(this).hasClass("txtNombreArchivo"))
                        {
                            editarArchivo(folder);
                        } else {
                            editarFolder(folder);
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
            // vista
                $(document).on("click", ".iconoVistaCuadricula", function (e) {
                    e.preventDefault();
                    verCuadricula($(this));
                    /*var seccion = $(this).parents(".accionesDiv");
                    $(".listView").addClass("hidden");
                    $(".cuadriculaView").removeClass("hidden");
                    
                    seccion.find(".icoVistaLista").removeClass("activeVista");
                    $(this).addClass("activeVista");
                    var frm = {
                        idCarpeta: $(".txtHdIdCarpetaPadre").val()
                    }
                    var seccionModificar = $(".cuadriculaView");
                    iconoVistaCuadricula(frm, seccionModificar);*/

                })
                $(document).on("click", ".icoVistaLista", function (e) {
                    e.preventDefault();
                    var seccion = $(this).parents(".accionesDiv");
                    $(".cuadriculaView").addClass("hidden");
                    $(".listView").removeClass("hidden");
                    seccion.find(".iconoVistaCuadricula").removeClass("activeVista");
                    $(this).addClass("activeVista");
                    var frm = {
                        idCarpeta: $(".txtHdIdCarpetaPadre").val()
                    }
                    var seccionModificar = $(".listView");
                    icoVistaLista(frm, seccionModificar);
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
                        folder = $(this).parents(".folder");
                        initShareFile(folder);

                    })
                    $(document).on("click", ".btnCompartir", function () {
                        seccion = $(this).parents(".shareSection");
                        frm = serializeSection(seccion);
                        console.log(frm);
                        if (frm.txtHdCarpetaPadrePublica != "-1") {
                            btnCompartir(frm, seccion);
                        } else {
                            alert("No se puede compartir en este directorio");
                        }
                        
                    })
            // repositorio privado        
                $(document).on("click", ".carpetaDetalle", function (e) {
                    window.location = RAIZ + "Repositorio/index/" + $(this).find(".txtHdIdCarpeta").val();
                })
                $(document).on("click", ".cuadritoCarpeta", function () {
                    /*frm = { idCarpeta: $(this).parents(".folder").find(".txtHdIdCarpeta").val() }
                    cuadritoCarpeta(frm);*/
                    console.log("Cuadrito carpeta click");
                    var estado = $(this).attr("id");
                    if (estado != '0') {
                        window.location = RAIZ + "Repositorio/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
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
                    var frm = {
                        txtBusqueda: $(".txtBusqueda").val()
                    }
                    var seccion = $(".cuadriculaView");
                    btnBusqueda(frm, seccion);
                })
                // eliminar archivos 
                    $(document).on("click", ".icoEliminarArchivo", function () {
                        var x = confirm("Esta seguro que desea eliminar este archivo");
                        if (x) {
                            seccion = $(this).parents(".folder");
                            frm = { idArchivo: seccion.find(".txtHdIdArchivo").val() }
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
                    
                        btnGuardarCarpeta(frm,seccion);
                    });
                    $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                        div = $(this).parents(".folder");
                        div.remove();
                    })
                // eliminar carpeta
                    $(document).on("click", ".icoEliminarCarpeta", function () {
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
                    
                        btnEditarCarpeta(frm, folder);
                    });
                    $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                        seccion = $(this).parents(".detalleCarpeta");
                        btnCancelarEdicionCarpeta(seccion);
                    });
                // cambiar nombre carpeta
                    $(document).on("click", ".sinRedirect", function (e) {
                        e.stopPropagation();
                    })
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