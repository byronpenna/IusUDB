// generics 
    // emails institucion
        function getTrEmailInstitucion(emailInstitucion) {
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input name='txtHdIdEmailInstitucion' class='txtHdIdEmailInstitucion' value='"+ emailInstitucion._idEmailInstitucion + "' />\
                </td>\
                <td>"+ emailInstitucion._email + "</td>\
                <td>\
                    <button class='btn btn-default'>Editar</button>\
                    <button class='btn btn-default btnEliminarEmailsInstitucion'>Eliminar</button>\
                </td>\
            </tr>\
            ";
            console.log("Llego aqui");
            return tr;
        }
        function btnAddEmailInstitucion(frm) {
            actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_agregarEmailInstitucion", frm, function (data) {
                console.log("La respuesta es: ", data);
                if (data.estado) {
                    console.log("Llego aqui");
                    var tr = getTrEmailInstitucion(data.emailInstitucion);
                    $(".tbEmailInstitucion tbody").empty().append(tr);
                    $(".btnAddEmailInstitucion").prop("disabled", "true");
                }
            })
        }
        function btnEliminarEmailsInstitucion(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_eliminarEmailInstitucion", frm, function (data) {
                console.log("La respuesta es: ", data);
                if (data.estado) {
                    tr.remove();
                    $(".btnAddEmailInstitucion").prop("disabled", "");
                    console.log("D: ");
                }
            })
        }
    //
    function fillInputsEdit(trMedio, medio, callback) {
        trMedio.find(".txtEnlace").val(medio.enlace);
        trMedio.find(".txtTextoEnlaceEdit").val(medio.nombreEnlace);
        callback();
    }
    function exitEditMode(trMedios, medio) {
        var enlace = "\
        <a href='"+medio._enlace+"'>\
            "+medio._enlace+"\
        </a>\
        "
        trMedios.find(".tdEnlace").empty().append(enlace);
        trMedios.find(".tdNombreEnlace").empty().append(medio._nombreEnlace);
        controlesEdit( false, trMedios);
    }
    function validarEdit(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            txtTextoEnlaceEdit: new Array()
        }
        val.general = new Array();
        if (frm.txtTextoEnlaceEdit == "") {
            val.campos.txtTextoEnlaceEdit.push("No puede dejar este campo vacio");
        }
        val.estado = getEstadoVal(val);
        return val;
    }
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
        return val;
    }
    function getTrMedios(enlace,permisos) {
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
                <input type='hidden' class='txtHdIdEnlace' name='txtHdIdEnlace' value='"+enlace._idEnlace+"' />\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input class='form-control input-sm txtEnlace' name='txtEnlace' />\
                    <div class='divResultado hidden'>\
                        _\
                    </div>\
                </div>\
                <div class='normalMode tdEnlace'>\
                    <a href='"+enlace._enlace+"'>\
                    " + enlace._enlace + "\
                    </a>\
                </div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input class='form-control input-sm txtTextoEnlaceEdit' name='txtTextoEnlaceEdit' />\
                    <div class='divResultado hidden'>\
                        _\
                    </div>\
                </div>\
                <div class='normalMode tdNombreEnlace'>"+enlace._nombreEnlace+"</div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <div class='btn-group'>\
                        <button class='btn btn-default btn-xs btnActualizar'>Actualizar</button>\
                        <button class='btn btn-default btn-xs btnCancelar'>Cancelar</button>\
                    </div>\
                </div>\
                <div class='normalMode'>\
                    <div class='btn-group'>\
                        <button class='btn btn-default btn-xs btnEditar' "+strEditar+">\
                            Editar\
                        </button>\
                        <button class='btn btn-default btn-xs btnEliminar' "+strEliminar+">\
                            Eliminar\
                        </button>\
                    </div>\
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
            enlace:         $.trim(trMedio.find(".tdEnlace").text()),
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
                tr = getTrMedios(data.enlace,data.permisos);
                $(".tbodyMedios").prepend(tr);
                limpiarInsert();
            } else {
                var mjs = "Ocurrio un error al agregar";
                if (data.error._mostrar) {
                    mjs = data.error.Message;
                }
                printMessage($(".divMensajesGenerales"), data.error.Message, false);
            }
            
        });
    }
    function limpiarInsert() {
        $(".tbEnlaces thead .trFormAgregar").find(".form-control").val("");
    }
    function btnEliminar(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionMediosInstituciones/sp_frontui_deleteEnlaceInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }