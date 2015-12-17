// genericas 
    function getTrInstituciones(institucion,permisos) {
        var strEditar="", strEliminar = "";
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
                    <input class='txtHdIdInstitucion' name='txtHdIdInstitucion' value='" + institucion._idInstitucion + "'>\
                    <input class='txtHdIdPais' name='txtHdIdPais' value='"+institucion._pais._idPais+"' />\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <input type='text' name='txtNombreInstitucionEdit' class='txtNombreInstitucionEdit form-control txtNombreInstitucionVal txtEnterEdit' />\
                        <div class='divResultado'></div>\
                    </div>\
                    <div class='normalMode tdNombre'>"+institucion._nombre+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <textarea class='txtAreaDireccionEdit form-control txtAreaDireccionVal' name='txtAreaDireccionEdit'></textarea>\
                    </div>\
                    <div class='normalMode tdDireccion '>"+institucion._direccion+"</div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='cbPaisEdit cbPaisVal' name='cbPaisEdit'></select>\
                    </div>\
                    <div class='normalMode tdPais'>\
                    "+institucion._pais._pais+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btnActualizarInstitucion'>\
                            Actualizar\
                        </button>\
                        <button class='btn'>\
                            Cancelar\
                        </button>\
                    </div>\
                    <div class='normalMode'>\
                        <div class='btn-group'>\
                            <button class='btn btn-default btnEditar btn-xs' "+strEditar+">Editar</button>\
                            <button class='btn btn-default btnDeleteInstitucion btn-xs' "+strEliminar+">Eliminar</button>\
                        </div>\
                        <div class='btn-group'>\
                            <a class='btn btnFromlink btn-default btn-xs' href='" + RAIZ + 'GestionInstituciones/SetLogo/' + institucion._idInstitucion + "' class='btn'>\
                                Logo\
                            </a>\
                            <a class='btn btnFromlink btn-default btn-xs' href='" + RAIZ + 'GestionTelefonos/Index/' + institucion._idInstitucion + "'>Telefonos</a>\
                            <a class='btn btnFromlink btn-default btn-xs' href='" + RAIZ + 'GestionMediosInstituciones/Index/' + institucion._idInstitucion + "'>Medios Electronicos</a>\
                        </div>\
                    </div>\
                </td>\
            </tr>\
        ";
        return tr;
    }
    function comboPaisAddOpcions(Paises,combo,selected) {
        combo.empty();
        $.each(Paises, function (i, pais) {
            opcion = { text: pais._pais ,value:pais._idPais}
            comboAddOpcion(combo,opcion,selected);
        });
    }
    function exitEditMode(trInstitucion,institucion) {
        trInstitucion.find(".tdNombre").empty().append(institucion._nombre);
        trInstitucion.find(".tdDireccion").empty().append(institucion._direccion);
        trInstitucion.find(".txtHdIdPais").val(institucion._pais._idPais);
        trInstitucion.find(".tdPais").empty().append(institucion._pais._pais)
        controlesEdit(false, trInstitucion);
    }
    function fillInputsEdit(trInstitucion, institucion, callback) {
        trInstitucion.find(".txtNombreInstitucionEdit").val(institucion.nombre);
        trInstitucion.find(".txtAreaDireccionEdit").val(institucion.direccion);
        // llenar cosa de paises
        frm = {};
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_getPaises", frm, function (data) {
            if (data.estado) {
                combo = trInstitucion.find(".cbPaisEdit");
                comboPaisAddOpcions(data.paises, combo, institucion.idPais);
                combo.chosen({ no_results_text: "Ese pais no existe", width: '100%' });
            }
            callback();
        });
    }
    // validaciones 
    function validacionEdit(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            
            txtNombreInstitucionVal: new Array(),
            txtAreaDireccionVal: new Array(),
            cbPaisVal: new Array()
        }
        if (frm.txtNombreInstitucionEdit == "") {
            val.campos.txtNombreInstitucionVal.push("Nombre de institucion no puede ir vacia");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function validacionIngreso(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            /*txtNombreInstitucion:   new Array(),
            txtAreaDireccion:       new Array(),
            cbPais:                 new Array()*/
            txtNombreInstitucionVal:    new Array(),
            txtAreaDireccionVal:        new Array(),
            cbPaisVal:                  new Array()
        }
        if (frm.txtNombreInstitucion == "") {
            val.campos.txtNombreInstitucionVal.push("Nombre de institucion no puede ir vacia");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
// acciones script
    function btnActualizarInstitucion(frm, trInstitucion) {
        console.log("actualizar", frm);
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_editInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                exitEditMode(trInstitucion, data.institucion);
            }
        });
    }
    function btnEditar(trInstitucion) {
        institucion = {
            nombre: trInstitucion.find(".tdNombre").text(),
            direccion: trInstitucion.find(".tdDireccion").text(),
            idPais: trInstitucion.find(".txtHdIdPais").val(),
            idInstitucion: trInstitucion.find(".txtHdIdInstitucion").val(),
            
        }
        console.log(institucion);
        fillInputsEdit(trInstitucion, institucion, function () {
            controlesEdit(true, trInstitucion);
        });
        
    }
    function btnDeleteInstitucion(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_deleteInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                printMessage($(".divMensajesGenerales"), "Institucion eliminada correctamente", true);
                table = $(".tbInstituciones");
                removeDataTable(table, tr);
            }
        });
    }
    function btnAddInstitucion(frm, seccion,callback) {
        console.log("formulario a agregar", frm);
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_insertInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr = getTrInstituciones(data.institucion,data.permisos);
                $(".tbInstituciones").dataTable().fnAddTr($(tr)[0]);
                //clearTr(seccion);
                seccion.find(".txtNombreInstitucion").val("");
                seccion.find(".txtAreaDireccion").val("");
            } else {
                console.log("Se fue al error");
                if (data.error._mostrar) {
                    printMessage($(".divMensajesGenerales"), data.error.Message, false);
                } else {
                    printMessage($(".divMensajesGenerales"), "Ocurrio un error no controlado", false);
                }
            }
            if (callback !== undefined) {
                callback();
            }
        })
    }