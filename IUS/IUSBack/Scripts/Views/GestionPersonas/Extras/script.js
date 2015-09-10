$(document).ready(function () {
    // plugins
        $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
    // eventos
        // click
            // email
                $(document).on("click", ".btnEliminarEmail", function () {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log(frm);
                    btnEliminarEmail(frm,tr);
                })
                $(document).on("click", ".btnGuardarEmail", function () {
                    var tr          = $(this).parents("tr");
                    var frm         = serializeSection(tr);
                    frm.idPersona   = $(".txtHdIdPersona").val();
                    //console.log(frm);
                    btnGuardarEmail(frm);
                })
            // telefono  
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
            // informacion
                $(document).on("click", ".btnGuardarInformacionBasica", function () {
                    var frm = serializeSection($(this).parents(".divFrmInformacionExtra"));
                    console.log("Formulario a enviar es", frm);
                    frm.txtHdIdPersona = $(".txtHdIdPersona").val();
                    btnGuardarInformacionBasica(frm);
                })
})