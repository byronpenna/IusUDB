$(document).ready(function () {
    console.log("Inicio");
    $(document).on("click", ".btnAprobar", function () {
        var frm = {
            txtFechaCaducidad: '2018-01-01',
            txtHdIdNotiEvento: $(".txtHdIdEvento").val(),
            txtHdTipoEvento: 2
        }
        btnAprobar(frm);
    })
})