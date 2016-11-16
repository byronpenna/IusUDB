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
            $(document).on("click", ".tabAprobar", function () {
                tabAprobar();
            });
            $(document).on("click", ".btnRechazarNoticia", function () {
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                var x = confirm("¿Esta seguro que desea rechazar esta publicación?");
                if (x) {
                    btnRechazarNoticia(frm, tr);
                }
            })
            $(document).on("click", ".btnCambiarEstadoRevision", function () {
                // ,txtHdTipoEvento,txtFechaCaducidad
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                var x = confirm("¿Esta seguro que desea " +$.trim(tr.find(".btnCambiarEstadoRevision").text()) + " ?");
                if (x) {
                    btnCambiarEstadoRevision(frm, tr);
                }
            })
            $(document).on("click", ".btnCaducidad", function () {
                console.log("boton de caducidad");
            })
            
            $(document).on("click", ".btnCambiarEstado", function () {
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                btnCambiarEstado(frm,tr);
            })
});