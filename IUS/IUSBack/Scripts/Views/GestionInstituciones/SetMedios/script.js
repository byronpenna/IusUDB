$(document).ready(function () {
    // eventos 
        // clicks 
            $(document).on("click", ".btnAgregar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnAgregar(frm);
            })
})