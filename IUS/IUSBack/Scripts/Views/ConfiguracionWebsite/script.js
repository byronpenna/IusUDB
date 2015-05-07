$(document).ready(function () {
    $(document).on("submit", "#frmInstitucional", function (e) {
        e.preventDefault();
        frm = serializeToJson($(this).serializeArray());
        console.log("Formulario a enviar es: ", frm);
        frmInstitucional(frm);
    })
    $(document).on("click", ".btnAddValor", function () {
        frmSection = $(this).parents(".agregarValorSection");
        frm = serializeSection(frmSection);
        console.log("formulario a agregar", frm);
    })
});