$(document).ready(function () {
    // plugins
        $(".cbUsuarios").chosen({ no_results_text: "No existe ese usuario", width: '100%' });
    //eventos
        // click
            $(document).on("click", ".icoCompartirFile", function (e) {
                e.preventDefault();
                var folder = $(this).parents(".folder");

            })
            $(document).on("click", ".cuadritoCarpeta", function () {
                var estado = $(this).attr("id");
                if (estado != '0') {
                    window.location = RAIZ + "RepositorioCompartido/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                }
            });
})