// acciones scripts
    function btnDesplegarEventos(divEventos) {
        if (divEventos.is(":visible")) {
            divEventos.hide("slow");
        } else {
            divEventos.show("slow");
        }
    }