$(document).ready(function () {
    // eventos
        // click 
        $(document).on("click", ".btnAgregarTel", function () {
            seccion = $(this).parents("tr");
            frm = serializeSection(seccion);
            console.log(frm);
            btnAgregarTel(frm);
        });
})