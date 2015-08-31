// generics 
    function fillInputsEdit(trMedio,medio,callback) {
        trMedio.find(".txtEnlace").val(medio.enlace);
        trMedio.find(".txtTextoEnlaceEdit").val(medio.nombreEnlace);
        callback();
    }
    function exitEditMode(trMedios,medio) {
        trMedios.find(".tdEnlace").empty().append(medio._enlace);
        trMedios.find(".tdNombreEnlace").empty().append(medio._nombreEnlace);
        controlesEdit( false, trMedios);
    }
    //************************
    function validarAgregar(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            txtTextoEnlace: new Array()
        }
        val.general = new Array();
        if (frm.txtTextoEnlace == "") {
            val.campos.txtTextoEnlace.push("No puede dejar este campo vacio");
        }
        val.estado = getEstadoVal(val);
        //console.log("Retorno val es", val);
        return val;
    }
    function getTrMedios(enlace) {
        tr = "\
        <tr>\
            <td>\
                <input type='hidden' class='txtHdIdEnlace' name='txtHdIdEnlace' value='"+enlace._idEnlace+"' />\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input class='form-control txtEnlace' name='txtEnlace' />\
                </div>\
                <div class='normalMode tdEnlace'>"+enlace._enlace+"</div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input class='form-control txtTextoEnlaceEdit' name='txtTextoEnlaceEdit' />\
                </div>\
                <div class='normalMode tdNombreEnlace'>"+enlace._nombreEnlace+"</div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <button class='btn btnActualizar'>Actualizar</button>\
                    <button class='btn btnCancelar'>Cancelar</button>\
                </div>\
                <div class='normalMode'>\
                    <button class='btn btnEditar'>\
                        Editar\
                    </button>\
                    <button class='btn btnEliminar'>\
                        Eliminar\
                    </button>\
                </div>\
            </td>\
        </tr>\
        ";
        return tr;
    }
// acciones script
    function btnActualizar(frm, trMedio) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_editEnlaceInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                exitEditMode(trMedio, data.enlace);
            }
        });
    }
    function btnEditar(trMedio) {
        medio = {
            enlace:         trMedio.find(".tdEnlace").text(),
            nombreEnlace:   trMedio.find(".tdNombreEnlace").text(),
        }
        fillInputsEdit(trMedio, medio, function () {
            controlesEdit(true, trMedio);
        });
    }
    function btnAgregar(frm) {
        console.log("a agregar", frm);
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_insertEnlaceInstituciones", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr = getTrMedios(data.enlace);
                $(".tbodyMedios").prepend(tr);
            } else {
                if (data.error._mostrar) {
                    alert(data.error.Message);
                }
            }
            
        });
    }
    function btnEliminar(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_deleteEnlaceInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }