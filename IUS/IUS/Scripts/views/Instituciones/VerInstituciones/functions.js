
function buscarInstitucionesPorNombre(txt) {
    var cuadritos = $(".divInstituciones").find(".cuadritoInstitucion");

    if (txt == "") {
        cuadritos.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = cuadritos.find(".hTituloInstitucion:containsi(" + txt + ")");
        //cuadrosSeleccionados.hide();
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".cuadritoInstitucion");
        //cuadrosSeleccionados.css("background", "red");
        cuadrosSeleccionados.removeClass("hidden");
    }
}