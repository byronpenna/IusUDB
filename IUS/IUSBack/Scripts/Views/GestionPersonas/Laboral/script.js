﻿$(document).ready(function () {
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
})