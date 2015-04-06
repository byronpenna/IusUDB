$(document).ready(function () {
    // acciones sub tabla 
        // desasociar rol 
            $(document).on("click", ".btnDesasociar", function () {
                var x = confirm("¿Esta seguro que desea eliminale este rol al usuario?");
                if (x) {

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
                    //tablaRoles(trUsuario);
                } else {
                    verRoles(false);
                    //$(".tableUsuarios").find(".trTableRol").remove();

                }
                
            })
        // deshabilitar
            $(document).on("click", ".btnDeshabilitar", function () {
                trUsuario = $(this).parents(".trUsuario");
                var idUsuario = trUsuario.find(".txtHdIdUser").val();
                var x = confirm("¿Esta seguro que desea cambiar el estado de este usuario?");
                if (x) {
                    console.log("deshabilitara");
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
});