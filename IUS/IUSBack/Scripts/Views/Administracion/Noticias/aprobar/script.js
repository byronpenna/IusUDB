$(document).ready(function () {
    // plugins 
        // datepicker
            $(".txtFechaCaducidad").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos 
        // click
            $(document).on("click", ".btnRechazarNoticia", function () {
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                btnRechazarNoticia(frm, tr);
            })
            $(document).on("click", ".btnCambiarEstadoRevision", function () {
                // ,txtHdTipoEvento,txtFechaCaducidad
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                btnCambiarEstadoRevision(frm, tr);
            })
            $(document).on("click", ".btnCaducidad", function () {
                console.log("boton de caducidad");
            })
            $(document).on("click", ".tabRevision", function () {
                tabRevision();
            })
            $(document).on("click", ".btnCambiarEstado", function () {
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                btnCambiarEstado(frm,tr);
            })
});