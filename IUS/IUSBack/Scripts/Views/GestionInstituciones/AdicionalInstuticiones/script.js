$(document).ready(function () {
    $(document).on("click", ".divAreaCarrera,.cuadritoNivelEducacion", function () {
        console.log("Click en area carrera");
        //var padre = $(this).parents(".divAreaCarrera");
        if ($(this).find(".txtHdEstado").val() == 1) {
            $(this).removeClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(0)
        } else {
            $(this).addClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(1)
        }
    })
    $(document).on("click", ".btnGuardarAreaConocimiento", function () {

    })
    $(document).on("click", ".btnGuardarNivel", function () {

    })
})