$(document).ready(function () {
    // eventos 
        // click 
            // Para subir
                $(document).on("click", ".icoSubirFichero", function (e) {
                    e.preventDefault();
                    $(".divUpload").fadeIn(400, function () {

                    });
                })
                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
                $(document).on("click", ".divUpload", function (e) {
                    $(this).fadeOut();
                })
                $(document).on("click", ".closeModal", function (e) {
                    $(".divUpload").click();
                })
            // para menu 
                $(document).on("click", ".NewActionLi", function () {
                    var elemento = $(this).find(".ulSub");
                    if (elemento.is(":visible")) {
                        elemento.hide();
                        console.log("Mostrar");
                    } else {
                        //$(this).find(".ulSub").hide();
                        elemento.show();
                        console.log("Ocultar");
                    }
                });
})