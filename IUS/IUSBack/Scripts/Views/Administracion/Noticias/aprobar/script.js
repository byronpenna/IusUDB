$(document).ready(function () {
    $(document).on("click", ".btnCambiarEstado", function () {
        tr = $(this).parents("tr");
        frm = serializeSection(tr);
        btnCambiarEstado(frm,tr);
    })
});