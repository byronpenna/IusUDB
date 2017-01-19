// tabs 
    function tabRevision() {

    }
    function tabAprobar() {

    }
    function btnEliminarArchivo(frm,tr) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_removeShareArchivoPublico", frm, function (data) {
            if (data.estado) {
                tr.remove();
            }
        })
    }
    function btnAprobarArchivo(frm,tr) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_aprobarArchivo", frm, function (data) {
            console.log("Data es ", data);
            if (data.estado) {
                console.log("Entro aqui ");
                tr.find(".td4").empty().append(data.archivoPublico.getStrEstado);
            }
        })
    }