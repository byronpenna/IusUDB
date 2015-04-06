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
        console.log("cambiarasEstado");
        estadoControl = estadoControlGlobal();
        console.log("El estado del control es", estadoControl);
        if (!estadoControl) {
            $(".controlGlobal").removeClass("hidden");
        } else {
            $(".controlGlobal").addClass("hidden");
        }
    }
// subtabla
    function desasociarRol(frm,trUsuarioRol) {
        actualizarCatalogo("/GestionRoles/desasociarRolUsuario", frm, function (data) {
            console.log("la data devuelta es:", data);
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
    function tablaRoles(trUsuario) {
        frm = new Object();
        frm.idUsuario = trUsuario.find(".txtHdIdUser").val();
        cargarObjetoGeneral("/GestionRoles/getJSONRoles", frm, function (data) {
            var roles = data.roles;
            table = "<tr class='trTableRol'>\
                        <td colspan=5>\
                            <table class='table tablaRolesUsuario'>\
                                <thead>\
                                    <tr class='hidden'>\
                                        <input type='hidden' name='idUsuario' class='idUsuario' value='"+frm.idUsuario+"'>\
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
                    
            /*$.each(roles, function (i, val) {
                table = "<tr>\
                            <td>
            });*/
            $.each(roles, function (i, val) {
                table += "\
                <tr class='trRol'>\
                    <td class='hidden'><input type='hidden' class='txtIdRol' value='" + val._idRol + "'/></td>\
                    <td>"+ val._rol + "</td>\
                    <td>" + val.stringEstado + "</td>\
                    <td class='tdAccionDesasociar'>\
                        <a href='#' title='Desasociar Rol' class='btnDesasociar'><i class='fa fa-times'></i></a>\
                    </td>\
                </tr>"
            });
            table += "\
                                </tbody>\
                            </table>\
                        </td>\
                  </tr>";
            trUsuario.after(table);
        });
    }
// Actualizar
    function actualizar(trUsuario) {
        obj = new Object();
        
        frm = serializeToJson(trUsuario.find("input,select").serializeArray());
        actualizarCatalogo("GestionUsuarios/actualizarUsuario", frm, function (data) {
            console.log("la data devuelta es:", data);
            if (data.estado) {
                usuario = data.usuario;
                console.log("actualizacion correcta");
                trUsuario.find(".tdTxtNombreCompleto").empty().append(usuario._persona.nombreCompleto);
                trUsuario.find(".tdTxtUsuario").empty().append(usuario._usuario);
                controlesEdit(false, trUsuario); // salimos del modo de edicion
                alert("actualizado correctamente");
                if (!usuario._estado) {
                    var x = confirm("El usuario que acaba de editar esta ¿deshabilitado desea habilitarlo?");
                    if (x) {
                        deshabilitarUsuario(usuario._idUsuario, trUsuario);
                    }
                }
            } else {
                alert("Ocurrio un error durante la actualizacion");
            }
        });
    }
// editar
    function controlesEdit(mostrar, trUsuario) {
        if (mostrar) {
            selectorMostrar = ".editMode";
            selectorOcultar = ".normalMode";
        } else {
            selectorMostrar = ".normalMode";
            selectorOcultar = ".editMode";
        }
        if (!trUsuario.find(selectorOcultar).hasClass("hidden")) {
            trUsuario.find(selectorOcultar).addClass("hidden");
        }
        if (trUsuario.find(selectorMostrar).hasClass("hidden")) {
            trUsuario.find(selectorMostrar).removeClass("hidden");
        }
    }

    function llenarCbPersonas(Personas, combo,selected) {
        combo.empty();
        $.each(Personas, function (i, value) {
            var opcion = new Object();
            opcion.value    = value._idPersona;
            opcion.text = value.nombreCompleto;
            comboAddOpcion(combo, opcion,selected);
        });
    }
    function entrarEditMode(trUsuario) {
        txtUsuario = trUsuario.find(".tdTxtUsuario").text();
        idPersonaActual = trUsuario.find(".txtHdIdUser").val();
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
    function deshabilitarUsuario(idUsuario, trUsuario) {
        console.log("entro a deshabilitar");
        $.ajax({
            data:{
                usuarioId: idUsuario
            },
            url: "/GestionUsuarios/cambiarEstadoUsuario",
            type: 'POST',
            beforeSend: function(){
                console.log("enviando");
            },
            success: function (data) {
                console.log("hizo success");
                console.log(data);
                if (data.estadoEjecucion) {
                    trUsuario.find(".tdEstadoUsuario").empty().append(data.nuevoEstadoUsuario);
                }
            
            }
        });
    }