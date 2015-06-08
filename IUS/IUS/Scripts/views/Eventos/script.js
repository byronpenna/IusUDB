$(document).ready(function () {
    // eventos
        // click
            $(document).on("click", ".btnDesplegarEventos", function () {
                var divEventos = $(this).parents(".divSeparadorFecha").next();
                console.log("entro");
                btnDesplegarEventos(divEventos);
            })
            $(document).on("click", ".navEvento", function () {
                pagina = $(this).parents(".divEventos").find(".pageEvent:visible");
                var siguiente;
                direccion = $(this).attr("direccion");
                if (direccion == 0) {
                    siguiente = pagina.prev(".pageEvent");
                } else {
                    siguiente = pagina.next(".pageEvent");
                }
                indefinida = !siguiente.hasClass("pageEvent");
                if (indefinida && direccion == 0) {
                    siguiente = pagina.parent().find(".pageEvent").last();
                    console.log(siguiente.hasClass("pageEvent"));
                    console.log("final");
                } else if (indefinida && direccion == 1) {
                    siguiente = pagina.parent().find(".pageEvent").first();
                    console.log("Principio");
                }
                navEvento(pagina, siguiente);
                
            });
})