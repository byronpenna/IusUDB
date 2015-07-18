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