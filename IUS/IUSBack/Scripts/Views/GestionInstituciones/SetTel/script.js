$(document).ready(function () {
    // eventos
        // click 
            $(document).on("click", ".btnAgregarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnAgregarTel(frm);
            });
            $(document).on("click", ".btnEliminarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnEliminarTel(frm, seccion);
            });
})