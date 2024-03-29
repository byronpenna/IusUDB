﻿// experiencia de usuario
    function estadoControlGlobal() {
        if ($(".editMode").length == $(".editMode.hidden").length) {
            return true;
        } else {
            return false;
        }
    }
    function cancelarGlobal() {
        $(".editMode").addClass("hidden");
        $(".normalMode").removeClass("hidden");
    }
    function cambiarEstadoControlGlobal() {
        estadoControl = estadoControlGlobal();
        if (!estadoControl) {
            $(".controlGlobal").removeClass("hidden");
        } else {
            $(".controlGlobal").addClass("hidden");
        }
    }
// subtabla
    function desasociarRol(frm,trUsuarioRol) {
        actualizarCatalogo(RAIZ+"/GestionRoles/desasociarRolUsuario", frm, function (data) {
            if (data) {
                trUsuarioRol.remove();
            }
        });
    }
// tabla 
    function verRoles(val, trUsuario) { // variable booleana que dira si verlos o no
        if (val) {
            tablaRoles(trUsuario);
        } else {
            if (trUsuario !== undefined && trUsuario.next().hasClass("trTableRol")) {
                trUsuario.next().remove();
            }
        }
    }
    function getTbRoles(roles) {
        table = "<tr class='trTableRol'>\
                        <td colspan=5>\
                            <table class='table tablaRolesUsuario'>\
                                <thead>\
                                    <tr class='hidden'>\
                                        <input type='hidden' name='idUsuario' class='idUsuario' value='"+ frm.idUsuario + "'>\
                                    </tr>\
                                    <tr  class='trTableRol'>\
                                        <td colspan='2' class='text-center titleTrTable'>Roles</td>\
                                        <td class='titleTrTable'><a class='btn btn-sm btn-default' href='" + RAIZ + "GestionRoles/Index/1/" + frm.idUsuario + "'>Agregar Roles</a></td>\
                                    </tr>\
                                    <tr>\
                                        <th class='text-center'>Rol</th>\
                                        <th class='text-center'>Estado Rol</th>\
                                        <th class='text-center'>Acciones</th>\
                                    </tr>\
                                </thead>\
                                <tbody class='tbodyRoles'>\
                                ";
        if( !(roles === null) ){
            $.each(roles, function (i, val) {
                table += "\
                    <tr class='trRol'>\
                        <td class='hidden'><input type='hidden' class='txtIdRol' value='" + val._idRol + "'/></td>\
                        <td>"+ val._rol + "</td>\
                        <td>" + val.stringEstado + "</td>\
                        <td class='tdAccionDesasociar'>\
                            <a href='#' title='Desasociar Rol' class='btnDesasociar'><i class='fa fa-times'></i></a>\
                        </td>\
                    </tr>";
            });
        } else {
            table += "\
                    <tr class='trRol'>\
                        <td colspan='3' class='text-center'>No hay roles disponibles</td>\
                    </tr>";
        }
        table += "\
                                </tbody>\
                            </table>\
                        </td>\
                  </tr>";
        return table;
    }
    function tablaRoles(trUsuario) {
        frm = new Object();
        frm.idUsuario = trUsuario.find(".txtHdIdUser").val();
        cargarObjetoGeneral(RAIZ+"/GestionRoles/getJSONRoles", frm, function (data) {
            var roles = data.roles;
            table = getTbRoles(roles);
            trUsuario.after(table);
        });
    }
// agregar
    function agregarUsuario() {

    }
// Actualizar
    function actualizar(trUsuario) {
        frm = serializeToJson(trUsuario.find("input,select").serializeArray());
        console.log(frm);
        var val = validacionIngreso(frm);
        if (val.estado) {
            actualizarCatalogo(RAIZ + "GestionUsuarios/actualizarUsuario", frm, function (data) {
                console.log("Respuesta actualizar", data);
                if (data.estado) {
                    usuario = data.usuario;
                    actualizarInformacionTr(trUsuario, usuario)
                    controlesEdit(false, trUsuario); // salimos del modo de edicion
                    if (!usuario._estado) {
                        var x = confirm("El usuario que acaba de editar esta ¿deshabilitado desea habilitarlo?");
                        if (x) {
                            deshabilitarUsuario(usuario._idUsuario, trUsuario);
                        }
                    }
                    updateAllDataTable($(".tableUsuarios"));
                } else {
                    //alert("Ocurrio un error durante la actualizacion");
                    if (data.error._mostrar) {
                        printMessage($(".divMensajesGenerales"), data.error.Message, false);
                    }
                }
            });
        } else {
            console.log("no validado");
            var errores;
            $.each(val.campos, function (i, val) {
                errores = "";
                var divResultado = trUsuario.find("." + i).parents("td").find(".divResultado")
                console.log(i, ": " + val);
                if (val.length > 0) {
                    divResultado.removeClass("hidden");
                    $.each(val, function (i, val) {
                        errores += getSpanMessageError(val);
                    })
                    divResultado.empty().append(errores);
                }
            })
        }
        
    }
    function actualizarInformacionTr(trUsuario,usuario) {
        trUsuario.find(".tdTxtNombreCompleto").empty().append(usuario._persona.nombreCompleto);
        trUsuario.find(".tdTxtUsuario").empty().append(usuario._usuario);
        trUsuario.find(".txtHdIdPersona").val(usuario._persona._idPersona);
    }
// editar
    function llenarCbPersonas(Personas, combo,selected) {
        combo.empty();
        opcion = { value: -1, text: 'Sin asignar' };
        comboAddOpcion(combo,opcion, selected);
        if (!(Personas === null)) {
            $.each(Personas, function (i, value) {
                var opcion = new Object();
                opcion.value = value._idPersona;
                opcion.text = value.nombreCompleto;
                comboAddOpcion(combo, opcion, selected);
            });
        }
        resetChosenWithSelectedVal(combo, selected);
    }
    function entrarEditMode(trUsuario) {
        trUsuario.find(".divResultado").empty();
        txtUsuario = trUsuario.find(".tdTxtUsuario").text();
        idPersonaActual = trUsuario.find(".txtHdIdPersona").val();
        controlesEdit(true, trUsuario);
        combo = trUsuario.find(".cbPersona");
        obj = cargarObjetoPersonas(function (Personas) {
            llenarCbPersonas(Personas,combo,idPersonaActual);
        });
        $(".cbPersona").chosen({ no_results_text: "No se a encontrado empleado", width: '100%' });
        resetChosenWithSelectedVal(combo, idPersonaActual);
        trUsuario.find(".txtEditUsuario").val($.trim(txtUsuario));
    }
    function salirEditMode(trUsuario) {
        controlesEdit(false,trUsuario);
    }
    function formTableEditar(TrUsuario) {
        // estado de boton: 0: normal; 1: editando
        entrarEditMode(TrUsuario);
    }
// deshabilitar
    function changeTxtBtnHabilitar(estado, trUsuario) {
        if (estado) {
            trUsuario.find(".btnDeshabilitar").text("Deshabilitar");
        } else {
            trUsuario.find(".btnDeshabilitar").text("Habilitar");
        }
    }
    function deshabilitarUsuario(idUsuario, trUsuario) {
        
        $.ajax({
            data:{
                usuarioId: idUsuario
            },
            url: RAIZ+"/GestionUsuarios/cambiarEstadoUsuario",
            type: 'POST',
            beforeSend: function(){
            },
            success: function (data) {
                if (data.estadoEjecucion) {                    
                    trUsuario.find(".tdEstadoUsuario").empty().append(data.nuevoEstadoUsuario);
                    changeTxtBtnHabilitar(data._estado, trUsuario);
                    updateAllDataTable($(".tableUsuarios"));
                }
            
            }
        });
    }
// genericas
    function getTrUsuario(usu, permiso) {
        console.log("usuario antes de tr", usu);
        var j = "<tr class='trUsuario'>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdUser'  class='txtHdIdUser' value='"+ usu._idUsuario + "' />\
                    <input type='hidden' name='txtHdIdPersona' class='txtHdIdPersona' value='"+ usu._persona._idPersona + "' />\
                </td>\
                <td class='tdNombre'>\
                    <div class='editMode hidden'>\
                        <select name='cbPersona' class='form-control cbPersona input-sm'>\
                            <option>Seleccione una persona</option>\
                        </select>\
                    </div>\
                    <div class='normalMode tdTxtNombreCompleto'>\
                        "+ usu._persona.nombreCompleto + "\
                    </div>\
                </td>\
                <td class='tdUsuario'>\
                    <div class='editMode hidden'>\
                        <input name='txtEditUsuario' type='text' class='form-control enterEdit txtUsuarioEdit txtEditUsuario input-sm'/>\
                        <div class='row marginNull divResultado hidden'>\
                        </div>\
                    </div>\
                    <div class='normalMode tdTxtUsuario'>\
                        "+ usu._usuario + "\
                    </div>\
                </td>\
                <td class='tdFecha'>\
                    " + usu.getFechaCreacion + "\
                </td>\
                <td class='tdEstadoUsuario'>\
                    "+ usu.estadoUsuario + "\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <div class='btn-group'>\
                            <button class='btn btn-xs btn-default btnEditMode btnActualizar'>Actualizar</button>\
                            <button class='btn btn-xs btn-default btnEditMode btnCancelarEdit'>Cancelar</button>\
                        </div>\
                    </div>\
                    <div class='normalMode '>\
                        <div class='btn-group'>\
                            <button class='btn btn-xs btn-default btnEditar' " + permiso.stringEditar + ">\
                                Editar\
                            </button>\
                            <button class='btn btn-xs btn-default btnDeshabilitar' " + permiso.stringEditar + " >\
                                "+ usu.txtBtnHabilitar + "\
                            </button>\
                            <input type='button' class='btn btn-xs btn-default btnVerRoles' "+permiso.stringEditar+" value='Ver Roles'>\
                        </div>";
        if (usu._persona._idPersona != -1) {
            j += "\
                   <a href='" + RAIZ + "GestionPersonas/Detalle/" + usu._persona._idPersona + "" + "' class='btn btn-xs'>\
                        Ver persona\
                   </a>";
        }
            j += "\
                    </div>\
                </td>\
            </tr>";
        //";
        return j;
    }
// validaciones
    function validacionEdit(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            cbPersona: new Array(),
            /*txtEditUsuario: new Array()*/
            txtUsuarioEdit: new Array()
        }
        if (frm.txtEditUsuario == "") {
            val.campos.txtEditUsuario.push("Usuario no puede quedar vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function validacionIngreso(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            cbPersona: new Array(),
            /*txtEditUsuario: new Array()*/
            txtUsuarioEdit: new Array()
        }
        if (frm.txtEditUsuario == "") {
            val.campos.txtUsuarioEdit.push("Usuario no puede quedar vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
// acciones scripts 
    function btnAgregarUsuario(tr) {
        frm = serializeSection(tr);
        var val = validacionIngreso(frm);
        tr.find(".divResultado").empty();
        tr.find(".divResultado").addClass("hidden");
        if (val.estado) {
            tbody = tr.parents("table").find("tbody");
            actualizarCatalogo(RAIZ + "GestionUsuarios/sp_sec_agregarUsuario", frm, function (data) {
                if (data.estado) {
                    tr.find(".txtUsuarioEdit").val("");
                    tr = getTrUsuario(data.usuarioAgregado, data.permiso);
                    tbody.prepend(tr);
                    $(".tableUsuarios").dataTable().fnAddTr($(tr)[0]);
                    
                } else {
                    console.log("Error ", data);
                    if (data.error._mostrar) {
                        printMessage($(".divMensajesGenerales"), data.error.Message, false);
                    }
                    /*error = data.error;
                    alert(error.Message);*/
                }
            });
        } else {
            var errores;
            $.each(val.campos, function (i, val) {
                errores = "";
                var divResultado = $(".tableUsuarios thead").find("." + i).parents("td").find(".divResultado")
                console.log(i,": "+val);
                if (val.length > 0) {
                    console.log("entro");
                    divResultado.removeClass("hidden");
                    $.each(val, function (i, val) {
                        
                        errores += getSpanMessageError(val);
                    })
                    console.log("errores", errores);
                    divResultado.empty().append(errores);
                }
            })
            
        }
        /**/
        
        
    }