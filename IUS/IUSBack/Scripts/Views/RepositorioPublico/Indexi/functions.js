function btnBusqueda(frm) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_searchArchivoPublicoBack", frm, function (data) {
        console.log("la data devuelta es ", data);
        if (data.estado) {
            if (data.archivos !== undefined && data.archivos !== null) {
                $.each(data.archivos, function (i, archivo) {

                })
            }
                
        }
    })
}
function btnGuardarCarpeta(frm, seccion) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_insertCarpetaPublica", frm, function (data) {
        console.log("data de carpeta INGRESADA", data);
        if (data.estado) {

        }
    })
}
function spIrBuscar(frm) {
    //console.log(frm);
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_getPublicoByRuta", frm, function (data) {
        console.log(data);
        if (data.estado) {
            if (data.carpetaPublica !== undefined && data.carpetaPublica !== null) {
                window.location = RAIZ + "RepositorioPublico/index/" + data.carpetaPublica._idCarpetaPublica;
            } else {
                //printMessage($(".divMensajesGenerales"), "No se a encontrado ese directorio, por favor digite bien la ruta", false);
            }
            
        }
    })
}
function btnEliminarArchivo(frm, tr) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_removeShareArchivoPublico", frm, function (data) {
        if (data.estado) {
            tr.remove();
        }
    })
}
function btnEliminarCarpeta(frm, tr) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_deleteCarpetaPublica", frm, function (data) {
        console.log(data);
        if (data.estado) {
            tr.remove();
        }
    })
}