function btnAprobarArchivo(frm) {
    actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_aprobarArchivo", frm, function (data) {
        console.log("Data es ", data);
        if (data) {

        }
    })
}