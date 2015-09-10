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
                // editar tel
                    // edit | cancelar
                        $(document).on("click", ".btnCancelarUpdateTel", function () {
                            var tr = $(this).parents("tr");
                            controlesEdit(false, tr);
                        })
                        $(document).on("click", ".btnEditarTel", function () {
                            var tr = $(this).parents("tr");
                            var telefono = $.trim(tr.find(".tdTelefono").text());
                            var etiqueta = $.trim(tr.find(".tdEtiqueta").text());
                            tr.find(".txtTelefono").val(telefono);
                            tr.find(".txtEtiquetaTel").val(etiqueta);
                            // cargar paises 
                            controlesEdit(true, tr);
                        })
                    $(document).on("click", ".btnActualizarTel", function () {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        console.log(frm);
                    })
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