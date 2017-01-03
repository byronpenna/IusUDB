function btnEliminarArchivo(frm,tr) {
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