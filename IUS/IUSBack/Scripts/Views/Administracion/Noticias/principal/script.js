﻿$(document).ready(function () {
    $(document).on("click", ".btnPublicacion", function () {
        var x = confirm("¿Esta seguro que desea cambiar estado de publicacion?");
        if (x) {
            tr      = $(this).parents("tr");
            idPost  = tr.find(".txtHdIdPost").val();
            frm     = { idPost: idPost };
            btnPublicacion(frm, tr);
        }
    })
});