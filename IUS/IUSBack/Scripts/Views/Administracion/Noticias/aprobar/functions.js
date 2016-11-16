var tbRevision = $(".tbRevision");
// metodos
    function getTrRevision(notiEvento) {
        var strEstado = "Publicar";
        if (notiEvento._estado) {
            strEstado = "Mandar revisión";
        }
        var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input class='txtHdIdNotiEvento' name='txtHdIdNotiEvento' value='" + notiEvento._id + "'>\
                    <input class='txtHdTipoEvento' name='txtHdTipoEvento' value='" + notiEvento._idTipoEntrada + "'>\
                    <input class='txtFechaCaducidad' name='txtFechaCaducidad' value='10/10/2015'>\
                </div>\
                <td>" + notiEvento._titulo + "</td>\
                <td>"+notiEvento._descripcion+"</td>\
                <td>" + notiEvento.getStrTipoEntrada + "</td>\
                <td>fecha caducidad</td>\
                <td>\
                    <button class='btn btn-default btnBack btn-block btnCambiarEstadoRevision'>\
                        "+strEstado+"\
                    </button>\
                    <button class='btn btn-default btnBack btn-block btnCaducidad'>\
                        Cambiar caducidad\
                    </button>\
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
    function btnRechazarNoticia(frm, tr) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/ajax_rechazar", frm, function (data) {
            console.log("La respuesta del servidor", data);
            if (data.estado) {
                tr.remove();
            }
        });
    }
    function btnCambiarEstadoRevision(frm, tr) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/ajax_revision", frm, function (data) {
            console.log("La respuesta del servidor", data);

        });
    }
    function btnCambiarEstado(frm, tr) {
        actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_cambiarEstado", frm, function (data) {
            console.log("La respuesta del servidor", data);
            //sp_adminfe_cambiarEstadoPublicacion
        })
    }