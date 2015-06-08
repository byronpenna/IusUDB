// acciones scripts
    function navEvento(actual, siguiente) {
        actual.fadeOut("slow", function () {
            siguiente.fadeIn("slow");
        });
    }
    function btnDesplegarEventos(divEventos) {
        if (divEventos.is(":visible")) {
            divEventos.hide("slow");
        } else {
            divEventos.show("slow");
        }
    }