$(document).ready(function () {
    
    $(document).on("click", ".btnGuardar", function () {
        var frm = serializeSection($(this).parents(".divFrmInformacionExtra"));
        console.log("Formulario a enviar es", frm);
    })
})