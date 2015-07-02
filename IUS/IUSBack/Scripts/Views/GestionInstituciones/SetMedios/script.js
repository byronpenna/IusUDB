$(document).ready(function () {
    // eventos 
        // clicks 
    
            $(document).on("click", ".btnActualizar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                console.log(frm);
                btnActualizar(frm, seccion);
            })
            $(document).on("click", ".btnEditar", function () {
                trMedio = $(this).parents("tr");
                btnEditar(trMedio);
            })
    
            $(document).on("click", ".btnAgregar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnAgregar(frm);
            })
            $(document).on("click", ".btnEliminar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnEliminar(frm,seccion);
            });
})