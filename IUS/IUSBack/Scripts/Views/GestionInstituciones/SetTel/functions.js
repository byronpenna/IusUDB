// genericas 
    function getTrTel(telefono) {
        tr = "\
            <tr>\
                <td>\
                    <input type='hidden' class='txtHdIdTel' name='txtHdIdTel' value='"+telefono._idTelefono+"'/>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='tel' name='txtTelefonoEdit' class='form-control txtTelefonoEdit' />\
                    </div>\
                    <div class='normalMode tdTelefono'>"+telefono._telefono+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='tel' name='txtEtiquetaEdit' class='form-control txtEtiquetaEdit' />\
                    </div>\
                    <div class='normalMode tdTextoTelefono'>"+telefono._textoTelefono+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btnActualizar'>\
                            Actualizar\
                        </button>\
                        <button class='btn'>\
                            Cancelar\
                        </button>\
                    </div>\
                    <div class='normalMode'>\
                        <button class='btn btnEliminarTel'>\
                            Eliminar\
                        </button>\
                        <button class='btn btnEditarTel'>\
                            Editar\
                        </button>\
                    </div>\
                </td>\
            </tr>\
        ";
        return tr;
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
    function btnAgregarTel(frm, seccion) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_insertTelInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                /*Llenar tablita*/
                tr = getTrTel(data.telefono);
                //$(".tbTelefonos").dataTable().fnAddTr($(tr)[0]);
                $(".tbodyTelefonos").prepend(tr);
                $(".txtTelefono").val("");
                $(".txtEtiqueta").val("");
                //clearTr(seccion);
            } else {
                if (data.error._mostrar)
                {
                    alert(data.error.Message);
                }
                
            }
            
        });
    }