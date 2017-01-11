var tbRevision  = $(".tbRevision");
var tbAprobar   = $(".tbAprobar");
// metodos
    function getTrAprobar(noticiaEvento) {
        //<td>"+noticiaEvento._descripcion+"</td>\
        /*
            <td>\
                <input type='text' class='form-control inputBack txtFechaCaducidad' name='txtFechaCaducidad' />\
            </td>\
        */
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
                <button class='btn btnBack btn-block btnCambiarEstado'>Aprobar</button>\
                <button class='btn btnBack btn-block btnRechazarNoticia'>Rechazar</button>\
                <a class='btn btnBack btn-block' href='" + RAIZ + "/Noticias/preview/" + notiEvento._id + "'>Ver</a>\
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
                    <a class='btn btn-default btnBack btn-block' href='" + RAIZ + "/AprobarNoticiaAccion/preview/" + notiEvento._id + "'>\
                        Ver\
                    </a>\
                </td>\
            </tr>\
        ";
        return tr;
    }
/*
    <button class='btn btn-default btnBack btn-block btnCaducidad'>\
        Cambiar caducidad\
    </button>\
    <div class='btn-group btn-block'>\
        <button class='btn btn-default btnBack col-lg-6 btnEliminarInvolucrado'>\
            Eliminar\
        </button>\
    </div>\
*/
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
        function btnEliminarInvolucrado(frm,tr) {
            actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_eliminarSolicitudPublicacion", frm, function (data) {
                console.log("La respuesta del servidor", data);
                if (data.estado) {
                    alert("Notica eliminada");
                    tr.remove();
                }
            })
        }
        function btnRechazarNoticia(frm, tr) {
            actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/ajax_rechazar", frm, function (data) {
                console.log("La respuesta del servidor", data);
                if (data.estado) {
                    alert("Notica fue rechazada correctamente");
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
                if (data.estado) {
                    alert("Publicada correctamente");
                    tr.remove();
                } else {
                    alert("Ocurrio un error");
                }
                //sp_adminfe_cambiarEstadoPublicacion
            })
        }