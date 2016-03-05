// genericas 
    // busqueda
        function btnBusqueda(frm, seccion, target) {
            actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_searchArchivoPublicoBack", frm, function (data) {
                console.log(data);
                var div = '';
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        if (target == "lista") {
                            div += getDivListaArchivos(archivo);
                        } else if (target == "cuadricula") {
                            div += getDivCuadriculaArchivo(archivo)
                        }
                    })
                } else {
                    div += "\
                            <div class='divNofoundResults'>\
                                No se han encontrado archivos\
                            </div>\
                            ";
                }
                seccion.empty().append(div);
                $(".encabezadoFicheros").empty().append("Resultados de busqueda");

            });
        }
    // ver lista 
        function getDivListaArchivos(archivoPublico) {
            // definir tipo de archivo 
            var div = "\
                <div class='row folderDetalles carpetaDetalle'>\
                    <div class='col-lg-3'>\
                        " + archivoPublico._nombre + "\
                    </div>\
                    <div class='col-lg-3'>\
                        " + archivoPublico._archivoUsuario._extension._tipoArchivo._tipoArchivo + "\
                    </div>\
                    <div class='col-lg-3'>\
                        " + archivoPublico._archivoUsuario._carpeta._usuario._usuario + "\
                    </div>\
                    <div class='col-lg-3'>\
                        " + archivoPublico.getFechaCreacion + "\
                    </div>\
                </div>";
            return div;
        }
        function getDivListaCarpeta(carpetaPublica) {
            var div = "\
                <div class='row folderDetalles carpetaDetalle'>\
                    <input type='hidden' class='txtHdIdCarpeta' value=" + carpetaPublica._idCarpetaPublica + ">\
                    <div class='col-lg-6'>\
                        " + carpetaPublica._nombre + "\
                    </div>\
                    <div class='col-lg-2'>\
                        Folder\
                    </div>\
                    <div class='col-lg-2'>\
                        \
                    </div>\
                    <div class='col-lg-2'>\
                        "+carpetaPublica.getFechaCreacion+"\
                    </div>\
                </div>\
            ";
            return div;
        }
        function verLista() {
            var frm = {
                idCarpetaPublica: $(".txtHdIdCarpetaPadre").val()
            }
            var seccionModificar = $(".targetListView");
            cambiarVistas("lista");
            icoVista(frm, seccionModificar,"lista");
        }
        
    // ver cuadricula 
        function getDivCuadriculaCarpeta(carpeta,base) {
            var rutaWebsite = '#';
            if(base !== undefined){
                rutaWebsite = base + "Repositorio/AllFiles/" + carpeta._idCarpetaPublica + "/-1";
            }
            var div = "\
            <div class='col-lg-2 folder'>\
                <input type='hidden' class='txtHdIdCarpeta' value='"+carpeta._idCarpetaPublica+"' />\
                <div class='cuadritoIcono cuadritoCarpeta'>\
                    <img class='imgCuadritoIcono' src='"+RAIZ+"Content/themes/iusback_theme/img/general/repositorio/"+carpeta.getIcono+"' />\
                    <div class='btn-group'>\
                        <a href='#' class='ico btn btn-default icoEliminarCarpeta' title='Eliminar'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                        <a href='" + rutaWebsite + "' target='_blank' class='ico btn btn-default' title='Ver website'>\
                            <i class='fa fa-globe'></i>\
                        </a>\
                    </div>\
                    <div class='detalleCarpeta'>\
                        <div class='normalMode sinRedirect'>\
                            <h3 class='ttlNombreCarpeta'>"+carpeta._nombre+"</h3>\
                        </div>\
                        <div class='row marginNull hidden editMode sinRedirect'>\
                            <div class='row marginNull inputNombreCarpeta'>\
                                <input type='text' class='form-control txtNombreCarpeta'>\
                            </div>\
                            <div class='row marginNull'>\
                                <button class='btn btn-xs btnEditarCarpeta'>Actualizar</button>\
                                <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                            </div>\
                        </div>\
                    </div>\
                </div>\
            </div>\
            ";
            return div;
        }    
        function getDivCuadriculaArchivo(archivo, base) {
            var rutaWebsite = "#";
            if (base !== undefined) {
                rutaWebsite = base + "Repositorio/AllFiles/" + archivo._carpetaPublica._idCarpetaPublica + "/-1";
            }
            var div = "\
                <div class='col-lg-2 folder'>\
                    <input type='hidden' class='txtHdIdArchivoPublico' value='"+archivo._idArchivoPublico+"' />\
                    <div class='row divHerramientasIndividual'>\
                        <a href='#' class='ico icoEliminarArchivo' title='Eliminar'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                        <a href='"+rutaWebsite+"' target='_blank' class='ico' title='Ver website'>\
                            <i class='fa fa-globe'></i>\
                        </a>\
                    </div>\
                    <div class='cuadritoIcono '>\
                        <img src='"+RAIZ+"/Content/themes/iusback_theme/img/general/repositorio/"+archivo._archivoUsuario._extension._tipoArchivo._icono+"' />\
                        <div class='detalleCarpeta'>\
                            <div class='normalMode'>\
                                <h3 class='ttlNombreCarpeta'>"+archivo._nombre+"</h3>\
                            </div>\
                            <div class='row marginNull hidden editMode'>\
                                <div class='row marginNull inputNombreCarpeta'>\
                                    <input type='text' class='form-control txtNombreCarpeta'>\
                                </div>\
                                <div class='row marginNull'>\
                                    <button class='btn btn-xs btnEditarArchivo'>Actualizar</button>\
                                    <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div>\
            ";
            return div;
        }
        function verCuadricula(callback) {
            var frm = {
                idCarpetaPublica: $(".txtHdIdCarpetaPadre").val()
            }
            var seccionModificar = $(".cuadriculaView");
            cambiarVistas("cuadricula");
            icoVista(frm, seccionModificar, "cuadricula",callback);
        }
    // gnerales
        function icoVista(frm, seccion,op,callback) {
            actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_entrarCarpetaPublica", frm, function (data) {
                console.log("la data devuelta por el servidor icoVista", data);
                var div = "";
                if (data.estado) {
                    if (data.carpetas !== undefined && data.carpetas !== null) {
                        //console.log()
                        $.each(data.carpetas, function (i, carpeta) {
                            if (op == "lista") {
                                div += getDivListaCarpeta(carpeta);
                            } else if (op == "cuadricula") {
                                div += getDivCuadriculaCarpeta(carpeta,data.base);
                            }
                        })
                    }
                    if (data.archivos !== undefined && data.archivos !== null) {
                        $.each(data.archivos, function (i, archivo) {
                            if (op == "lista") {
                                div += getDivListaArchivos(archivo);
                            } else if (op == "cuadricula") {
                                div += getDivCuadriculaArchivo(archivo,data.base)
                            }
                        })
                    }

                    seccion.empty().append(div);
                    if (callback !== undefined) {
                        callback();
                    }
                }
            })
        }
// acciones script
    function btnEditarArchivo(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_renameFile", frm, function (data) {
            console.log(data);
            if (data.estado) {
                seccion.find(".ttlNombreCarpeta").empty().append(data.archivoPublico._nombre);
                btnCancelarEdicionCarpeta(seccion.find(".detalleCarpeta"));
            }
        });
    }
    function spIrBuscar(frm) {
        //console.log(frm);
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getPublicoByRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                if(data.carpetaPublica !== undefined && data.carpetaPublica !== null )
                {
                    window.location = RAIZ + "RepositorioPublico/index/" + data.carpetaPublica._idCarpetaPublica;
                } else {
                    printMessage($(".divMensajesGenerales"), "No se a encontrado ese directorio, por favor digite bien la ruta", false);
                }
            }
        })
    }
    function icoEliminarArchivo(frm, seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_removeShareArchivoPublico", frm, function (data) {
            if (data.estado) {
                seccion.remove();
            }
        })
    }
    function icoEliminarCarpeta(frm, seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_deleteCarpetaPublica", frm, function (data) {
            console.log(data);
            if (data.estado) {
                seccion.remove();
            }
        })
    }
    function btnGuardarCarpeta(frm, seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_insertCarpetaPublica", frm, function (data) {
            console.log("data de carpeta INGRESADA",data);
            if (data.estado) {
                seccion.find(".cuadritoCarpeta").attr("id", 1);
                seccion.find(".imgFolder").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");

                seccion.find(".txtHdIdCarpeta").val(data.carpeta._idCarpetaPublica);
                seccion.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);

                seccion.find(".saveMode").remove();
                seccion.find(".normalMode").removeClass("hidden");
                //
                seccion.find(".icoWebsiteFolder").attr("href", data.base + "Repositorio/AllFiles/" + data.carpeta._idCarpetaPublica + "/-1");
                seccion.find(".icoWebsiteFolder").attr("target", "_blank");
                seccion.find(".divHerramientasIndividual").fadeIn("slow");
                //
                
                x = seccion.find(".cuadritoIconoAdd");
                x.removeClass("cuadritoIconoAdd");
                x.addClass("cuadritoIcono");
            } else {
                var mjs = "";
                if (data.error._mostrar) {
                    mjs = data.error.Message;
                } else {
                    mjs = "Error no controlado";
                }
                printMessage($(".mensajeNewFolder"),mjs , false);
            }
        })
    }
    function btnEditarCarpeta(frm, folder) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_updateCarpetaPublica", frm, function (data) {
            //console.log("Respuesta", data);
            if (data.estado) {
                folder.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);
                btnCancelarEdicionCarpeta(folder.find(".detalleCarpeta"));
            } else {
                alert("Ocurrio un error queriendo renombrar la carpeta");
            }
        })
    }