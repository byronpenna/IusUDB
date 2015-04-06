$(document).ready(function () {
    // plugins 
        $(".dtFechaNacimiento").datepicker({
            dateFormat: "dd/mm/yy"
        });
    // actualizar 
        $(document).on("click", ".btnActualizar", function () {
            var x = confirm("¿Esta seguro que desea actualizar esta persona?");
            trPersona = $(this).parents(".trPersona");
            if (x) {
                actualizar(trPersona)
            }
        })
    // editar
        $(document).on("click", ".btnEditar", function () {
            var x = confirm("¿Esta seguro que desea editar esta persona?");
            trPersona = $(this).parents(".trPersona");
            if (x) {
                editMode(trPersona);
            }
        });
        $(document).on("click", ".btnCancelarEdit", function () {
            trPersona = $(this).parents(".trPersona");
            controlesEdit(false, trPersona);
        })
    // experiencia de usuario
        $(document).on("click", ".btnEditMode,.btnEditar", function () {
            cambiarEstadoControlGlobal();
        })
        $(document).on("click", ".btnCancelarGlobal", function () {
            cancelarGlobal();
            cambiarEstadoControlGlobal();
        });
        
});