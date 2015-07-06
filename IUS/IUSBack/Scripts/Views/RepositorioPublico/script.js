$(document).ready(function () {
    // eventos
        //click 
            $(document).on("click", ".icoNuevaCarpeta", function (e) {
                e.preventDefault();
                div = getDivNewFolder();
                $(".folders").prepend(div);
            })
            // guardar carpeta
                $(document).on("click", ".btnGuardarCarpeta", function (e) {
                    seccion = $(this).parents(".folder");
                    frm = {
                        idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                        nombre: seccion.find(".txtNombreCarpetaSave").val()
                    }
                    console.log(frm);
                    btnGuardarCarpeta(frm, seccion);
                })
            // cambiar nombre a carpeta
                $(document).on("click", ".btnEditarCarpeta", function (e) {
                    folder = $(this).parents(".folder");
                    frm = {
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtNombreCarpeta").val()
                    }
                    btnEditarCarpeta(frm, folder);
                })
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    seccion = $(this).parents(".detalleCarpeta");
                    btnCancelarEdicionCarpeta(seccion);
                })
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                e.cancelBubble = true;
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
})