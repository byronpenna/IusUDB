// acciones script
    function btnEditarArchivo(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_renameFile", frm, function (data) {
            console.log(data);
            if (data.estado) {

            }
        });
    }
    function spIrBuscar(frm) {
        console.log(frm);
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getPublicoByRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                window.location = RAIZ + "RepositorioPublico/index/" + data.carpetaPublica._idCarpetaPublica;
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