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
})