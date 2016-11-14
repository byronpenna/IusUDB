$(document).ready(function () {
    // plugins 
        // datepicker
            $(".txtFechaCaducidad").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos 
        // click
            $(document).on("click", ".tabRevision", function () {
                tabRevision();
            })
            $(document).on("click", ".btnCambiarEstado", function () {
                tr = $(this).parents("tr");
                frm = serializeSection(tr);
                btnCambiarEstado(frm,tr);
            })
});