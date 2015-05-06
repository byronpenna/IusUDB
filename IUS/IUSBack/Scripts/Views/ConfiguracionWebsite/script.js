$(document).ready(function () {
    $(document).on("submit", "#frmInstitucional", function (e) {
        e.preventDefault();
        frm = serializeToJson($(this).serializeArray());
        console.log("Formulario a enviar es: ", frm);
    })
});