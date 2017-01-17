$(document).ready(function () {
    // eventos
        $(document).on("click", ".btnAprobarArchivo", function () {
            var x = confirm("¿Esta seguro que desea aprobar archivo?");
            if (x) {
                var frm = {
                    idArchivo: $(this).parents("tr").find(".txtHdIdArchivo").val()
                }
                btnAprobarArchivo(frm);

            }
        })
});