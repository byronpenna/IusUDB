﻿$(document).ready(function () {
    // eventos
        // click 
            $(document).on("click", ".btnCancelar", function () {
                controlesEdit(false, $(this).closest("tr"));
            })
            $(document).on("click", ".btnCambiar", function () {
                var tr =  $(this).closest("tr");
                fillTiposArchivos(tr.find(".cbTipoArchivo"), tr.find(".txtHdIdTipoArchivo").val());
                controlesEdit(true, tr);

            })
            $(document).on("click", ".btnGuardar", function () {
                var tr = $(this).closest("tr");
            })
})