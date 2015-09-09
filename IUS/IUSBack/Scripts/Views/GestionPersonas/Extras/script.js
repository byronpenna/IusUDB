$(document).ready(function () {
    // plugins
        $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
    // eventos
        // click
        
            $(document).on("click", ".btnEliminarTel", function () {
                var tr  = $(this).parents("tr");
                var frm = serializeSection(tr);
                console.log(frm);
                btnEliminarTel(frm,tr);
            })
            $(document).on("click", ".btnAgregarTel", function () {
                var frm         = serializeSection($(this).parents("tr"));
                frm.idPersona   = $(".txtHdIdPersona").val();
                console.log("Formulario a enviar es", frm);
                //var val = 
                btnAgregarTel(frm);
            })
            $(document).on("click", ".btnGuardarInformacionBasica", function () {
                var frm = serializeSection($(this).parents(".divFrmInformacionExtra"));
                console.log("Formulario a enviar es", frm);
                frm.txtHdIdPersona = $(".txtHdIdPersona").val();
                btnGuardarInformacionBasica(frm);
            })
})