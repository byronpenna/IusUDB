// genericas 
    function getTrTel(telefono,permisos) {
        var strEditar = "", strEliminar = "";
        if (permisos !== undefined) {
            if (!permisos._editar) {
                strEditar   = "disabled";
            }
            if (!permisos._eliminar) {
                strEliminar = "disabled";
            }
        }
        tr = "\
            <tr>\
                <td>\
                    <input type='hidden' class='txtHdIdTel' name='txtHdIdTel' value='"+telefono._idTelefono+"'/>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='tel' name='txtTelefonoEdit' class='inputBack form-control txtTelefonoEdit soloNumerosDecimal txtFrmEditar' />\
                        <div class='divResultado marginNull row hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdTelefono'>"+telefono._telefono+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='tel' name='txtEtiquetaEdit' class='inputBack form-control txtEtiquetaEdit txtFrmEditar' />\
                        <div class='divResultado marginNull row hidden'>\
                            _\
                        </div>\
                    </div>\
                    <div class='normalMode tdTextoTelefono'>"+telefono._textoTelefono+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <div class='btn-group btn-block'>\
                            <button class='btn btn-default col-lg-6 btn-xs btnActualizar'>\
                                Actualizar\
                            </button>\
                            <button class='btn btn-default col-lg-6 btn-xs btnCancelar'>\
                                Cancelar\
                            </button>\
                        </div>\
                    </div>\
                    <div class='normalMode'>\
                        <div class='btn-group btn-block'>\
                            <button class='btn btn-default btnBack col-lg-6 btnEditarTel' " + strEditar + " >\
                                Editar\
                            </button>\
                            <button class='btn btn-default btnBack col-lg-6 btnEliminarTel' " + strEliminar + " >\
                                Eliminar\
                            </button>\
                        </div>\
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
    
    function btnEliminarTel(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_deleteTelInstitucion", frm, function (data) {
            if (data.estado) {
                tr.remove();
            }
        })
    }
    // editar tel 
        function validarEditarTel() {
            var val = new Object();
            val.campos = {
                txtEtiquetaEdit: new Array(),
                txtTelefonoEdit: new Array()
            }
            val.general = new Array();
            if (frm.txtEtiquetaEdit == "") {
                val.campos.txtEtiquetaEdit.push("No puede dejar este campo vacio");
            }
            if (frm.txtTelefonoEdit == "") {
                val.campos.txtTelefonoEdit.push("No puede dejar este campo vacio");
            }
            val.estado = getEstadoVal(val);
            return val;
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
    // agregar tel
        function validarAgregarTel(frm) {
            var val = new Object();
            val.campos = {
                txtEtiqueta: new Array(),
                txtTelefono: new Array()
            }
            val.general = new Array();
            if (frm.txtTelefono == "") {
                val.campos.txtTelefono.push("No puede dejar este campo vacio");
            }
            if (frm.txtEtiqueta == "") {
                val.campos.txtEtiqueta.push("No puede dejar este campo vacio");
            }
            val.estado = getEstadoVal(val);
            return val;
        }
        function btnAgregarTel(frm, seccion) {
            actualizarCatalogo(RAIZ + "/GestionTelefonos/sp_frontui_insertTelInstitucion", frm, function (data) {
                if (data.estado) {
                    /*Llenar tablita*/
                    tr = getTrTel(data.telefono,data.permisos);
                    //$(".tbTelefonos").dataTable().fnAddTr($(tr)[0]);
                    $(".tbodyTelefonos").prepend(tr);
                    $(".txtTelefono").val("");
                    $(".txtEtiqueta").val("");
                } else {
                    var mjs = "Ocurrio un error no controlado";
                    if (data.error._mostrar)
                    {
                        mjs = data.error.Message;
                    }
                    printMessage($(".divMensajesGenerales"), mjs, false);
                }
            
            });
        }