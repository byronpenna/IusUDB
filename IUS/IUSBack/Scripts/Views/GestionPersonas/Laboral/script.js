$(document).ready(function () {
    // eventos 
        // click 
            $(document).on("click", ".btnAgregarLaboralPersona", function () {
                var frm = serializeSection($(this).parents("tr"));
                frm.idPersona = $(".txtHdIdPersona").val();
                console.log("formulario a enviar",frm);
                btnAgregarLaboralPersona(frm);
            })
            $(document).on("click", ".btnEliminarLaboralPersona", function () {
                var tr = $(this).parents("tr");
                var frm = serializeSection(tr);
                console.log("formulario a enviar", frm);
                btnEliminarLaboralPersona(frm,tr);
            })
            // editar
                $(document).on("click", ".btnEditarLaboralPersona", function () {
                    // variables
                        var tr = $(this).parents("tr");
                        var datosSet = getObjetoSetEditLaboral(tr);
                    // set 
                        tr.find(".txtInicio").val(datosSet.fechaInicio);
                        tr.find(".txtFin").val(datosSet.fechaFin);
                        tr.find(".txtAreaObservacion").val(datosSet.observaciones);

                    controlesEdit(true, tr);
                })
                $(document).on("click", ".btnCancelarUni", function (e) {
                    var tr = $(this).parents("tr");
                    controlesEdit(false, tr);
                })
})