var tbRevision = $(".tbRevision");
// metodos
    function getTrRevision(notiEvento) {
        var tr = "\
            <tr>\
                <td>" + notiEvento._titulo + "</td>\
                <td>"+notiEvento._descripcion+"</td>\
                <td>" + notiEvento.getStrTipoEntrada + "</td>\
                <td>fecha caducidad</td>\
                <td>\
                    <button>Dar de baja</button>\
                </td>\
            </tr>\
        ";
        return tr;
    }
// acciones 
    function tabRevision() {
        var frm = {};
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_getNoticiasRevision", frm, function (data) {
            console.log("La respuesta del servidor", data);
            if (data.estado) {
                console.log("Ahh ", data.noticiasEventos.length);
                if (data.noticiasEventos !== undefined && data.noticiasEventos.length > 0) {
                    var tr = "";
                    $.each(data.noticiasEventos, function (i, notiEvento) {
                        tr += getTrRevision(notiEvento);
                    })
                    tbRevision.find("tbody").empty().append(tr);
                }
            }
        });
    }
    function btnCambiarEstado(frm, tr) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_cambiarEstado", frm, function (data) {
            console.log("La respuesta del servidor", data);
            //sp_adminfe_cambiarEstadoPublicacion
        })
    }