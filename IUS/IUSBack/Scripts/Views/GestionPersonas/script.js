$(document).ready(function () {
    // editar
        $(document).on("click", ".btnEditar", function () {
            var x = confirm("¿Esta seguro que desea editar esta persona?");
            trPersona = $(this).parents(".trPersona");
            if(x){
                controlesEdit(true,trPersona);
            }
        });
        $(document).on("click", ".btnCancelarEdit", function () {
            trPersona = $(this).parents(".trPersona");
            controlesEdit(false, trPersona);
        })
});