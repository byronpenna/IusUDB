$(document).ready(function () {
    // eventos
        // click
            $(document).on("click", ".btnDesplegarEventos", function () {
                var divEventos = $(this).parents(".divSeparadorFecha").next();
                console.log("entro");
                btnDesplegarEventos(divEventos);
            })
})