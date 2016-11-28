$(document).ready(function () {
    // eventos 
        //click
            // cuadro modal 
                $(document).on("click", ".divUpload", function () {
                    $(this).fadeOut();
                })
                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
                $(document).on("click", ".btnSolicitarCambio", function (e) {
                    //icoSubirFichero
                    console.log("Esta por solicitar cambio");
                    $(".divUpload").fadeIn(400, function () {

                    });
                })
});