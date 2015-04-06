$(document).ready(function () {
   
    // acciones sub tabla 
        // desasociar rol 
            $(document).on("click", ".btnDesasociar", function () {
                var x           = confirm("¿Esta seguro que desea eliminale este rol al usuario?");
                trUsuarioRol    = $(this).parents(".trRol");
                frm             = new Object();
                frm.idUsuario   = $(this).parents(".tablaRolesUsuario").find(".idUsuario").val();
                frm.idRol       = trUsuarioRol.find(".txtIdRol").val();
                if (x) {
                    desasociarRol(frm, trUsuarioRol);
                }
            })
    // Acciones tabla 
        // ver roles 
            $(document).on("click", ".btnVerRoles", function () {
                trUsuario = $(this).parents(".trUsuario");
                if (!trUsuario.next().hasClass("trTableRol")) {
                    //$(".tableUsuarios").find(".trTableRol").remove();
                    verRoles(false);
                    verRoles(true, trUsuario);
                    $(this).val("Ocultar Roles");
                    //tablaRoles(trUsuario);
                } else {
                    verRoles(false);
                    $(this).val("Ver Roles");
                    //$(".tableUsuarios").find(".trTableRol").remove();
                }
                
            })
        // deshabilitar
            $(document).on("click", ".btnDeshabilitar", function () {
                trUsuario = $(this).parents(".trUsuario");
                var idUsuario = trUsuario.find(".txtHdIdUser").val();
                var x = confirm("¿Esta seguro que desea cambiar el estado de este usuario?");
                if (x) {
                    
                    deshabilitarUsuario(idUsuario,trUsuario);
                }
            });
        // actualizar
            $(document).on("click", ".btnActualizar", function () {
                var x = confirm("¿Esta seguro que desea salvar los cambios?");
                trUsuario = $(this).parents(".trUsuario");
                if (x) {
                    actualizar(trUsuario);
                }
            });
        // editar 
            $(document).on("click", ".btnCancelarEdit", function () {
                trUsuario = $(this).parents(".trUsuario");
                var x = confirm("¿Esta seguro que desea cancelar la edición?");
                if (x) {
                    salirEditMode(trUsuario);
                }
            });
            $(document).on("click", ".btnEditar", function () {
                trUsuario = $(this).parents(".trUsuario");
                var x = confirm("¿Esta seguro que desea editar este usuario?");
                if (x) {
                    verRoles(false);
                    formTableEditar(trUsuario);
                }
            })
        // experiencia de usuario 
            $(document).on("click", ".btnEditMode,.btnEditar", function () {
                cambiarEstadoControlGlobal();
            })
            $(document).on("click", ".btnCancelarGlobal", function () {
                cancelarGlobal();
                cambiarEstadoControlGlobal();
            })
});