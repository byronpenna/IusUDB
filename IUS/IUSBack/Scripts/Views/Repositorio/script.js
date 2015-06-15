$(document).ready(function () {
    // eventos 
        // doble click
            $(document).on("dblclick", ".ttlNombreCarpeta", function (e) {
                seccion = $(this).parents(".detalleCarpeta");
                nombre = $(this).text();
                ttlNombreCarpeta(seccion, nombre);
            })
            
        // click 
            $(document).on("click", ".cuadritoIcono", function () {
                frm = 
            });
            // herramientas carpetas
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
                            nombre: seccion.find(".txtNombreCarpeta").val()
                    }
                    btnGuardarCarpeta(frm,seccion);
                });
                $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                    div = $(this).parents(".folder");
                    div.remove();
                })
            // actualizar carpeta
                $(document).on("click", ".btnEditarCarpeta", function () {
                    folder = $(this).parents(".folder");
                    frm = {
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtNombreCarpeta").val()
                    }
                    console.log("Formulario a enviar", frm);
                    btnEditarCarpeta(frm, folder);
                });
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    seccion = $(this).parents(".detalleCarpeta");
                    btnCancelarEdicionCarpeta(seccion);
                });

})