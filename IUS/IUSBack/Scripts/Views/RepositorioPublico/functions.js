// genericas 
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
            <a class='aLista' href='" + RAIZ + '/RepositorioPublico/index/' +carpetaPublica._idCarpetaPublica+ "' >\
                <div class='row folderDetalles carpetaDetalle'>\
                    <div class='col-lg-3'>\
                        " + carpetaPublica._nombre + "\
                    </div>\
                    <div class='col-lg-3'>\
                        Folder\
                    </div>\
                    <div class='col-lg-3'>\
                        \
                    </div>\
                    <div class='col-lg-3'>\
                        "+carpetaPublica.getFechaCreacion+"\
                    </div>\
                </div>\
            </a>\
            ";
            return div;
        }
        function verLista() {
            var frm = {
                idCarpetaPublica: $(".txtHdIdCarpetaPadre").val()
            }
            var seccionModificar = $(".listView");
            cambiarVistas("lista");
            icoVistaLista(frm, seccionModificar);
        }
        
    // ver cuadricula 
        function getDivCuadriculaCarpeta(carpeta) {
            var div = "\
            <div class='col-lg-2 folder'>\
                <input type='hidden' class='txtHdIdCarpeta' value='"+carpeta._idCarpetaPublica+"' />\
                <div class='row divHerramientasIndividual'>\
                    <a href='#' class='ico' title='Descargar'>\
                        <i class='fa fa-download'></i>\
                    </a>\
                    <a href='#' class='ico icoEliminarCarpeta' title='Eliminar'>\
                        <i class='fa fa-trash-o'></i>\
                    </a>\
                </div>\
                <div class='cuadritoIcono cuadritoCarpeta'>\
                    <img src='~/Content/themes/iusback_theme/img/general/repositorio/"+carpeta.getIcono+"' />\
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
        function getDivCuadriculaArchivo(archivo) {
            var div = "\
                <div class='col-lg-2 folder'>\
                    <input type='hidden' class='txtHdIdArchivoPublico' value='"+archivo._idArchivoPublico+"' />\
                    <div class='row divHerramientasIndividual'>\
                        <a href='#' class='ico icoEliminarArchivo' title='Eliminar'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                    </div>\
                    <div class='cuadritoIcono '>\
                        <img src='~/Content/themes/iusback_theme/img/general/repositorio/"+archivo._archivoUsuario._extension._tipoArchivo._icono+"' />\
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
        function verCuadricula() {
            cambiarVistas("cuadricula");
        }
    // gnerales
        function icoVistaLista(frm, seccion,op) {
            actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_entrarCarpetaPublica", frm, function (data) {
                console.log("la data devuelta por el servidor", data);
                var div = "";
                if (data.estado) {
                    if (data.carpetas !== undefined && data.carpetas !== null) {
                        $.each(data.carpetas, function (i, carpeta) {
                            if (op == "lista") {
                                div += getDivListaCarpeta(carpeta);
                            } else if (op == "cuadricula") {
                                div += getDivCuadriculaCarpeta(carpeta);
                            }
                        })
                    }
                    if (data.archivos !== undefined && data.archivos !== null) {
                        $.each(data.archivos, function (i, archivo) {
                            if (op == "lista") {
                                div += getDivListaArchivos(archivo);
                            } else if (op == "cuadricula") {
                                div += getDivCuadriculaArchivo(archivo)
                            }
                        })
                    }
                    seccion.empty().append(div);
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
            console.log(data);
            if (data.estado) {
                seccion.find(".cuadritoCarpeta").attr("id", 1);
                seccion.find(".imgFolder").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");

                seccion.find(".txtHdIdCarpeta").val(data.carpeta.idCarpetaPublica);
                seccion.find(".ttlNombreCarpeta").empty().append(data.carpeta._nombre);

                seccion.find(".saveMode").remove();
                seccion.find(".normalMode").removeClass("hidden");

                x = seccion.find(".cuadritoIconoAdd");
                x.removeClass("cuadritoIconoAdd");
                x.addClass("cuadritoIcono");
            } else {
                alert("Ocurrio un error agregando la carpeta");
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