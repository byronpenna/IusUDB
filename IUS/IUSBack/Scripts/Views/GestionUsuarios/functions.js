// experiencia de usuario
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
            $(".tableUsuarios").find(".trTableRol").remove();
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
                                        <th colspan='3' class='text-center'>Roles</th>\
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
        actualizarCatalogo(RAIZ+"GestionUsuarios/actualizarUsuario", frm, function (data) {
            if (data.estado) {
                usuario = data.usuario;
                actualizarInformacionTr(trUsuario,usuario)
                controlesEdit(false, trUsuario); // salimos del modo de edicion
                alert("actualizado correctamente");
                if (!usuario._estado) {
                    var x = confirm("El usuario que acaba de editar esta ¿deshabilitado desea habilitarlo?");
                    if (x) {
                        deshabilitarUsuario(usuario._idUsuario, trUsuario);
                    }
                }
                updateAllDataTable($(".tableUsuarios"));
            } else {
                alert("Ocurrio un error durante la actualizacion");
            }
        });
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
        
    }
    function entrarEditMode(trUsuario) {
        txtUsuario = trUsuario.find(".tdTxtUsuario").text();
        idPersonaActual = trUsuario.find(".txtHdIdPersona").val();
        controlesEdit(true, trUsuario);
        combo = trUsuario.find(".cbPersona");
        obj = cargarObjetoPersonas(function (Personas) {
            llenarCbPersonas(Personas,combo,idPersonaActual);
        });
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
        var j = "<tr class='trUsuario'>\
                <td class='hidden'>\
                    <input type='hidden' name='txtHdIdUser'  class='txtHdIdUser' value='"+ usu._idUsuario + "' />\
                    <input type='hidden' name='txtHdIdPersona' class='txtHdIdPersona' value='"+ usu._persona._idPersona + "' />\
                </td>\
                <td class='tdNombre'>\
                    <div class='editMode hidden'>\
                        <select name='cbPersona' class='form-control cbPersona'>\
                            <option>Seleccione una persona</option>\
                        </select>\
                    </div>\
                    <div class='normalMode tdTxtNombreCompleto'>\
                        "+ usu._persona.nombreCompleto + "\
                    </div>\
                </td>\
                <td class='tdUsuario'>\
                    <div class='editMode hidden'>\
                        <input name='txtEditUsuario' type='text' class='form-control txtEditUsuario'/>\
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
                        <button class='btn btn-sm btnEditMode btnActualizar'>Actualizar</button>\
                        <button class='btn btn-sm btnEditMode btnCancelarEdit'>Cancelar</button>\
                    </div>\
                    <div class='normalMode '>\
                        <button class='btn btn-xs btnEditar' "+permiso.stringEditar+">\
                            Editar\
                        </button>\
                        <button class='btn btn-xs btnDeshabilitar' "+permiso.stringEditar+" >\
                            "+ usu.txtBtnHabilitar + "\
                        </button>\
                        <input type='button' class='btn btn-xs btnVerRoles' "+permiso.stringEditar+" value='Ver Roles'>\
                        <button class='btn btn-xs'>\
                            Ver persona\
                        </button>\
                    </div>\
                </td>\
            </tr>";
        //";
        return j;
    }
// validaciones
    function validacionIngreso(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            cbPersona: new Array(),
            txtEditUsuario: new Array()
        }
        if (frm.txtEditUsuario == "") {
            val.campos.txtEditUsuario.push("Usuario no puede quedar vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
// acciones scripts 
    function btnAgregarUsuario(tr) {
        frm = serializeSection(tr);
        var val = validacionIngreso(frm);
        console.log(frm);
        console.log(val);
        if (val.estado) {
            tbody = tr.parents("table").find("tbody");
            actualizarCatalogo(RAIZ + "GestionUsuarios/sp_sec_agregarUsuario", frm, function (data) {
                if (data.estado) {
                    tr = getTrUsuario(data.usuarioAgregado, data.permiso);
                    tbody.prepend(tr);
                    $(".tableUsuarios").dataTable().fnAddTr($(tr)[0]);
                } else {
                    error = data.error;
                    alert(error.Message);
                }
            });
        } else {
            var errores;
            $.each(val.campos, function (i, val) {
                errores = "";
                var divResultado = $(".tableUsuarios thead").find("." + i).parents("td").find(".divResultado")
                if (val.length > 0) {
                    console.log("entro");
                    divResultado.removeClass("hidden");
                    $.each(val, function (i, val) {
                        errores += getSpanMessageError(val);
                    })
                    divResultado.empty().append(errores);
                }
            })
            
        }
        /**/
        
        
    }