$(document).ready(function () {
    // plugins
        // tabs
            $('#horizontalTab').responsiveTabs();
        // chosen 
            $(".cbUsuarios").chosen();
            $(".cbRoles").chosen({ no_results_text: "Rol no encontrado",width:'100%' });
    // eventos
        // change
            $(document).on("change", ".cbUsuarios", function () {
                // llenar tablita    
                idUsuario = $(this).val();
                llenarTablaRolesUsuario(idUsuario);
            })
})
