$(document).ready(function () {
        setInterval(function () {
            //console.log("entro");
            txtTiempo = $(".txtHoraActual").text();
            $(".txtHoraActual").empty().append(clockHora(txtTiempo));
        }, 1000);
    // plugins
        // datatable 
            $(".tableUsuarios").DataTable({
                "bDestroy": true,
                "bSort": false
            });
        // chosen
            $(".cbPersona").chosen({ no_results_text: "No se a encontrado empleado", width: '100%' });
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
    // keyup 
            $(document).on("keydown", ".enterNormal", function (e) {
                switch (e.which) {
                    case 13: {
                        $(".btnAgregarUsuario").click();
                        break;
                    }
                }
            })

            $(document).on("keydown", ".enterEdit", function (e) {
                var tr = $(this).parents("tr");
                switch (e.which) {
                    case 13: {
                        tr.find(".btnActualizar").click();
                    }
                }
            })
            
    // Acciones tabla 
        // agregar 
            $(document).on("click", ".btnAgregarUsuario", function () {
                tr = $(this).parents("tr");
                btnAgregarUsuario(tr);
            });
        // ver roles 
            $(document).on("click", ".btnVerRoles", function () {
                var trUsuario = $(this).parents(".trUsuario");
                console.log("D: ");
                if (!trUsuario.next().hasClass("trTableRol")) {
                    //$(".tableUsuarios").find(".trTableRol").remove();
                    console.log("yes :D");

                    verRoles(false,trUsuario);
                    verRoles(true, trUsuario);
                    $(this).val("Ocultar Roles");
                    
                    //tablaRoles(trUsuario);
                } else {
                    console.log("no :D");
                    verRoles(false,trUsuario);
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
            // actualizar general 
            $(document).on("click", ".btnActualizarTodo", function () {
                tabla = $(this).parents("table");
                accionActualizarGeneral(tabla, "GestionUsuarios/sp_sec_actualizarUsuariosGeneral", function (data,frm) {
                    
                    
                    $.each(data.usuarios, function (i, val) {
                        tr = getEdit(tabla, ".txtHdIdUser", val._idUsuario);
                        tr = tr.parents("tr");
                        actualizarInformacionTr(tr, val);
                        controlesEdit(false, tr);
                    });
                    if (!data.estadoIndividual) {
                        alert("algunos usuarios no se actualizaron correctamente");
                    }
                    if (!tabla.find("tr.trEdit").length) {
                        cancelarControlGlobal();
                    }
                });
                
            });
            // actualizar individual
            $(document).on("click", ".btnActualizar", function () {
                //var x = confirm("¿Esta seguro que desea salvar los cambios?");
                console.log("ey este que ondas D: ");
                trUsuario = $(this).parents(".trUsuario");
                //if (x) {
                    actualizar(trUsuario);
                //}
            });
        // editar 
            $(document).on("click", ".btnCancelarEdit", function () {
                trUsuario = $(this).parents(".trUsuario");
                //var x = confirm("¿Esta seguro que desea cancelar la edición?");
                //if (x) {
                    salirEditMode(trUsuario);
                //}
            });
            $(document).on("click", ".btnEditar", function () {
                trUsuario = $(this).parents(".trUsuario");
                //var x = confirm("¿Esta seguro que desea editar este usuario?");
                //if (x) {
                    verRoles(false);
                    formTableEditar(trUsuario);
                //}
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