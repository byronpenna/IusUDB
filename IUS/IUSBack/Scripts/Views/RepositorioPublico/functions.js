// acciones script
    function btnGuardarCarpeta(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_insertCarpetaPublica", frm, function (data) {
            console.log(data);
        })
    }