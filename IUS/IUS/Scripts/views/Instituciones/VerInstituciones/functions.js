function buscarPorPais(idPais) {
    var cuadritos = $(".divInstituciones").find(".todoCuadritoInstitucion");
    if (idPais == -1) {
        cuadritos.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = cuadritos.find(".txtHdIdInstitucion[value='" + idPais + "']");
        //cuadrosSeleccionados.val("D: ");
        //cuadrosSeleccionados.hide();
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
        //cuadrosSeleccionados.css("background", "red");
        cuadrosSeleccionados.removeClass("hidden");
    }
}
function buscarInstitucionesPorNombre(txt) {
    var cuadritos = $(".divInstituciones").find(".todoCuadritoInstitucion");

    if (txt == "") {
        cuadritos.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = cuadritos.find(".hTituloInstitucion:containsi(" + txt + ")");
        //cuadrosSeleccionados.hide();
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
        //cuadrosSeleccionados.css("background", "red");
        cuadrosSeleccionados.removeClass("hidden");
    }
}