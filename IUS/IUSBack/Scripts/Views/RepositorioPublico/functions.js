// acciones script
    function btnGuardarCarpeta(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioPublico/sp_repo_insertCarpetaPublica", frm, function (data) {
            console.log(data);
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