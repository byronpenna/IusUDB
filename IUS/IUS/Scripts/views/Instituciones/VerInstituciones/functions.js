function getFiltroCuadritos(idPais,cuadritos) {
    var cuadritos               = $(".divInstituciones").find(".todoCuadritoInstitucion");
    var cuadrosSeleccionados = null;
    if (idPais != -1) {
        cuadrosSeleccionados = cuadritos.find(".txtHdIdInstitucion[value='" + idPais + "']");
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
    } else {
        cuadrosSeleccionados = cuadritos;
    }
    return cuadrosSeleccionados;
}
function buscarPorPais(idPais) {
    /*var cuadritos = $(".divInstituciones").find(".todoCuadritoInstitucion");
    if (idPais == -1) {
        cuadritos.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = cuadritos.find(".txtHdIdInstitucion[value='" + idPais + "']");
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
        cuadrosSeleccionados.removeClass("hidden");
    }*/
    var cuadritos = $(".divInstituciones").find(".todoCuadritoInstitucion");
    if (idPais == -1) {
        cuadritos.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = getFiltroCuadritos(idPais,cuadritos);
        cuadrosSeleccionados.removeClass("hidden");
    }
}
function buscarInstitucionesPorNombre(txt) {
    /*var cuadritos = $(".divInstituciones").find(".todoCuadritoInstitucion");
    if (txt == "") {
        cuadritos.removeClass("hidden");
        $(".cbPais").change();
    } else {
        cuadritos.addClass("hidden");
        var cuadrosSeleccionados = cuadritos.find(".hTituloInstitucion:containsi(" + txt + ")");
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
        cuadrosSeleccionados.removeClass("hidden");
    }*/
    var cuadritos               = $(".divInstituciones").find(".todoCuadritoInstitucion");
    var cuadrosSeleccionados = null;
    cuadrosSeleccionados = getFiltroCuadritos($(".cbPais").val(), cuadritos);
    if (txt == "") {
        //cuadritos.removeClass("hidden");
        cuadrosSeleccionados.removeClass("hidden");
    } else {
        cuadritos.addClass("hidden");
        console.log(":|", $(".cbPais").val())
        cuadrosSeleccionados = cuadrosSeleccionados.find(".hTituloInstitucion:containsi(" + txt + ")");
        //cuadrosSeleccionados.css("background", "red");
        cuadrosSeleccionados = cuadrosSeleccionados.parents(".todoCuadritoInstitucion");
        cuadrosSeleccionados.removeClass("hidden");
    }
}