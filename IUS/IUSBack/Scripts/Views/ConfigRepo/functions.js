// scripts 
function btnGuardar(frm,tr) {
    actualizarCatalogo(RAIZ + "/ConfigRepo/sp_repo_actualizarTipoArchivoExt", frm, function (data) {
        console.log("La data es: ", data);
        if (data.estado) {
            tr.find(".tdTipoArchivo").empty().append(data.extension._tipoArchivo._tipoArchivo);
            tr.find(".txtHdIdTipoArchivo").val(data.extension._tipoArchivo._idTipoArchivo);

            controlesEdit(false, tr);

        }
    })
}
function fillTiposArchivos(cb, idSelected) {
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