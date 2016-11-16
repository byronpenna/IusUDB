var tbRevision  = $(".tbRevision");
var tbAprobar   = $(".tbAprobar");
// metodos
    function getTrAprobar(noticiaEvento) {
        //<td>"+noticiaEvento._descripcion+"</td>\
        var tr = "\
        <tr>\
            <td class='hidden'>\
                <input class='txtHdIdNotiEvento' name='txtHdIdNotiEvento' value='"+noticiaEvento._id+"' />\
                <input class='txtHdTipoEvento' name='txtHdTipoEvento' value='"+noticiaEvento._idTipoEntrada+"' />\
            </td>\
            <td>"+noticiaEvento._titulo+"</td>\
            <td></td>\
            <td>"+noticiaEvento.getStrTipoEntrada+"</td>\
            <td>\
                <input type='text' class='form-control inputBack txtFechaCaducidad' name='txtFechaCaducidad' />\
            </td>\
            <td>\
                <button class='btn btnBack btn-block btnCambiarEstado'>Aprobar</button>\
                <button class='btn btnBack btn-block btnRechazarNoticia'>Rechazar</button>\
            </td>\
        </tr>\
        ";
        return tr;
    }
    function getTrRevision(notiEvento) {
        var strEstado = "Publicar";
        if (notiEvento._estado) {
            strEstado = "Mandar revisión";
        }
        //<td>"+notiEvento._descripcion+"</td>\
        var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input class='txtHdIdNotiEvento' name='txtHdIdNotiEvento' value='" + notiEvento._id + "'>\
                    <input class='txtHdTipoEvento' name='txtHdTipoEvento' value='" + notiEvento._idTipoEntrada + "'>\
                    <input class='txtFechaCaducidad' name='txtFechaCaducidad' value='10/10/2015'>\
                </div>\
                <td>" + notiEvento._titulo + "</td>\
                <td></td>\
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
    // tab clicks
        function tabAprobar() {
            var frm = {};
            actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarnoticia_getNoticiasAprobar", frm, function (data) {
                console.log("La respuesta del servidor", data);
                if (data.estado) {
                    if (data.noticiasEventos !== undefined && data.noticiasEventos.length > 0) {
                        var tr = "";
                        $.each(data.noticiasEventos, function (i, notiEvento) {
                            console.log("Vas a pedir aprobar");
                            tr += getTrAprobar(notiEvento);
                        })
                        tbAprobar.find("tbody").empty().append(tr);
                        $(".txtFechaCaducidad").datepicker({
                            dateFormat: "dd/mm/yy"
                        });
                    }
                }
            })
            
        }
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
    //---------------
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