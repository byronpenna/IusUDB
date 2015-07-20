// scripts 
    function icoCompartirFile(folder) {
        var nombreArchivo = folder.find(".ttlNombreCarpeta").text();
        var idArchivo = folder.find(".txtHdIdArchivo").val();
        $(".nombreFileCompartir").empty().append(nombreArchivo);
        $(".txtHdIdArchivoCompartir").val(idArchivo);
    }
    function btnShareArchivo(frm) {
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_compartirArchivo", frm, function (data) {
            console.log(data);
            if (data.estado) {

            }
        })
    }
    function spIrBuscar(frm) {

        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                window.location = RAIZ + "Repositorio/index/" + data.carpeta._idCarpeta;
            } else {
                if (data.error._mostrar) {
                    alert(data.error.Message);
                }
            }
        })
    }