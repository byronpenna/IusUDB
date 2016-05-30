function fillTiposArchivos(cb,idSelected) {
    var frm = {};
    actualizarCatalogo(RAIZ + "/ConfigRepo/sp_repo_getTipoArchivo", frm, function (data) {
        console.log("La respuesta es: ", data);
        if (data.estado) {
            var options = "",selected="";
            if (data.tiposArchivos !== undefined && data.tiposArchivos != null) {
                $.each(data.tiposArchivos, function (i, tipoArchivo) {
                    if (tipoArchivo._idTipoArchivo == idSelected) {
                        selected = "selected";
                    } else {
                        selected = "";
                    }
                    options += "<option "+selected+" value='" + tipoArchivo._idTipoArchivo + "'>" + tipoArchivo._tipoArchivo + "</option>";
                })
            }
            cb.empty().append(options);
        }
    })
}