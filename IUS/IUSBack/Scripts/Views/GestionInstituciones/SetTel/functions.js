// genericas 
    function getTrTel() {
        tr = '\
            <tr>\
                <td\
            </tr>\
        ';
    }
    function fillInputsEdit(trTel,tel,callback) {
        trTel.find(".txtTelefonoEdit").val(tel.telefono);
        trTel.find(".txtEtiquetaEdit").val(tel.textoTelfono);
        callback();
    }
    function exitEditMode(trTel,tel) {
        trTel.find(".tdTelefono").empty().append(tel._telefono);
        trTel.find(".tdTextoTelefono").empty().append(tel._textoTelefono);
        controlesEdit(false, trTel);
    }
// acciones script 
    function btnActualizar(frm,trTel) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_editTelInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                exitEditMode(trTel, data.telefono);
            }
        });
    }
    function btnEditarTel(trTel) {
        telefono = {
            telefono: trTel.find(".tdTelefono").text(),
            textoTelfono: trTel.find(".tdTextoTelefono").text()
             
        }

        fillInputsEdit(trTel, telefono, function () {
            controlesEdit(true, trTel);
        })
    }
    function btnEliminarTel(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_deleteTelInstitucion", frm, function (data) {
            if (data.estado) {
                tr.remove();
            }
        })
    }
    function btnAgregarTel(frm) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_insertTelInstitucion", frm, function (data) {
            console.log(data);
            /*Llenar tablita*/
        });
    }