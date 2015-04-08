$(document).ready(function () {
    // plugins
        // tabs
            $('#horizontalTab').responsiveTabs();
        // chosen 
            $(".cbUsuarios").chosen();
            $(".cbRoles").chosen({ no_results_text: "Rol no encontrado",width:'100%' });
    // eventos
        // click 
            $(document).on("click", "#btnAddRoles", function () {
                var x = confirm("¿Esta seguro que desea agregar los siguientes roles?");
                if (x) {
                    var frm = new Object();
                    frm.rolesAgregar    = $(".cbRoles").val();
                    frm.idUsuario       = $(".cbUsuarios").val();
                    textoRoles = $(".cbRoles").text();
                    console.log("texto roles", textoRoles);
                    if (frm.rolesAgregar != null) {
                        console.log("formulario a enviar", frm);
                        var roles = new Array();
                        $.each(frm.rolesAgregar, function (i, val) {
                            roles[i] = new Object();
                            roles._idRol    = val;
                            roles._rol = textoRoles[i];
                        })
                        console.log("los roles son",roles);
                        //agregarRoles(frm);
                    }else {
                        alert("Seleccione un rol a agregar");
                    }
                }
            });
        // change
            $(document).on("change", ".cbUsuarios", function () {
                // llenar tablita    
                idUsuario = $(this).val();
                llenarTablaRolesUsuario(idUsuario);
            })
})
