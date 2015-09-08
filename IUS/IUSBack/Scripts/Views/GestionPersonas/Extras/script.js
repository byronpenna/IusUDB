$(document).ready(function () {
    
    $(document).on("click", ".btnGuardarInformacionBasica", function () {
        var frm = serializeSection($(this).parents(".divFrmInformacionExtra"));
        console.log("Formulario a enviar es", frm);
        frm.txtHdIdPersona = $(".txtHdIdPersona").val();
        btnGuardarInformacionBasica(frm);
    })
})