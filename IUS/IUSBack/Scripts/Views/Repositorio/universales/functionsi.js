
$(document).on("click", ".spIrBuscar", function () {
    frm = { txtRuta: $(".txtDireccion").val() }
    if (frm.txtRuta.slice(-1) != "/") {
        frm.txtRuta += "/";
    }
    spIrBuscar(frm);
})
$(document).on("click", ".lkbSubMenu", function (e) {
    window.location = $(this).attr("href");
})