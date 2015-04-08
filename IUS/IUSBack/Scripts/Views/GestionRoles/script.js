$(document).ready(function () {
    // plugins
        // tabs
            $('#horizontalTab').responsiveTabs();
        // chosen 
            // tab 1
                $(".cbUsuarios").chosen();
                $(".cbRoles").chosen({ no_results_text: "Rol no encontrado", width: '100%' });
            // tab 2
                $(".cbRolTab2").chosen({ no_results_text: "Rol no encontrado", width: '100%' });
                $(".cbAsignarPermisos").chosen({ no_results_text: "Permiso no encontrado", width: '100%' });
                $(".cbSubMenu").chosen({ no_results_text: "Submenu no encontrado", width: '100%' });
                
    // eventos
        // click 
            $(document).on("click", "#btnAddRoles", function () {
                var x = confirm("¿Esta seguro que desea agregar los siguientes roles?");
                if (x) {
                    var frm = new Object();
                    frm.rolesAgregar    = $(".cbRoles").val();
                    frm.idUsuario       = $(".cbUsuarios").val();
                    if (frm.rolesAgregar != null) {
                        console.log("formulario a enviar", frm);
                        agregarRoles(frm);
                    }else {
                        alert("Seleccione un rol a agregar");
                    }
                }
            });
            $(document).on("click", ".iconQuitarRol", function () {
                var x = confirm("¿Esta seguro que desea quitarle ese rol?");
                trRol = $(this).parents(".trEstadoRol");
                if (x) {
                    frm = new Object();
                    frm.idUsuario   = $(".cbUsuarios").val();
                    frm.idRol = trRol.find(".txtIdRol").val();
                    console.log("formulario a enviar sera", frm);
                    desasociarRol(frm, trRol);
                }
            })
        // change
            $(document).on("change", ".cbUsuarios", function () {
                // llenar tablita    
                idUsuario = $(this).val();
                llenarTablaRolesUsuario(idUsuario);
            })
})
