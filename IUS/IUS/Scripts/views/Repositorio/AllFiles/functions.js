// acciones scripts 
    function spIrBuscar(frm, seccion) {
        actualizarCatalogo(RAIZ + "Repositorio/sp_repo_front_getCarpetaPublicaByRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                var accionControlador = "";
                if (frm.txtIdFiltro == -1) {
                    accionControlador = "AllFiles";
                } else {
                    accionControlador = "FileByCategory";
                }
                window.location = RAIZ + "Repositorio/" + accionControlador + "/" + data.carpetaPublica._idCarpetaPublica + "/" + frm.txtIdFiltro;
            } else {
                alert("No se encontro la carpeta");
            }
        })
    }