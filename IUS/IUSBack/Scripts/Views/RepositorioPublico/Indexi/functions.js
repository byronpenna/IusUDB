function btnEliminarCarpeta(frm, tr) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_deleteCarpetaPublica", frm, function (data) {
        console.log(data);
        if (data.estado) {
            tr.remove();
        }
    })
}