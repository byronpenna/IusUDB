// acciones scripts
    function ckAble(inputs, divPadre, ck) {
        var able = false;
        if (divPadre.parents(".rowControlesBusqueda").find("input:checked").length > 0) {
            able = true;
        }
        if (ck.prop("checked")) {
            inputs.prop("disabled", false);
            able = true;
        } else {
            inputs.prop("disabled", true);
        }
        if (able) {
            divPadre.parents(".seccionBusqueda").find(".botonBuscar").prop("disabled", false);
        } else {
            divPadre.parents(".seccionBusqueda").find(".botonBuscar").prop("disabled", true);
        }
    }
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