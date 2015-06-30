$(document).ready(function () {
    // eventos 
        // clicks 
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