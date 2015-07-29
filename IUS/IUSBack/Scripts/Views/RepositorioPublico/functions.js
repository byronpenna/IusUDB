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
    function icoVistaLista(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_entrarCarpetaPublica", frm, function (data) {
            console.log("la data devuelta por el servidor", data);
            var div = "";
            if (data.estado) {
                if (data.carpetas !== undefined && data.carpetas !== null) {
                    $.each(data.carpetas, function (i, carpeta) {
                        div += getDivListaCarpeta(carpeta);
                    })
                }
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        div += getDivListaArchivos(archivo);
                    })
                }
                seccion.empty().append(div);
            }
        })
    }
    // ver cuadricula 
        function getDivCuadricula(archivoPublico){

        }    
        function verCuadricula() {
            cambiarVistas("cuadricula");
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