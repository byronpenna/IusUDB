function buscarContinente(frm) {
    var target = $(".tbodyInstitucion");
    actualizarCatalogo(RAIZ + "/Instituciones/sp_frontui_getInstitucionesByContinente", frm, function (data) {
        console.log("D: :DP", data)
        if (data.estado) {
            var instituciones = data.instituciones.instituciones;

            var tr = "";
            console.log("instituciones", instituciones);
            var hContinente = "";
            if (data.instituciones.continente !== undefined && data.instituciones.continente != null) {
                hContinente = data.instituciones.continente._continente;
            }
            if (instituciones !== undefined && instituciones != null && instituciones.length > 0) {
                console.log("entro aqui");
                $.each(instituciones, function (i, institucion) {
                    tr += getTrInstitucion(institucion);
                });
            } else {
                tr = getTrInstitucionNull();
            }

            target.empty().append(tr);
            $(".hContinente").empty().append(hContinente);
        }

    }, function () {
        var trLoading = "\
            <tr>\
                <td colspan='3' class='text-center'>\
                    <img src='" + IMG_GENERALES + "ajax-loader.gif'>\
                </td>\
            </tr>\
            ";
        target.empty().append(trLoading);
    })
}
    function getTrInstitucionNull() {
        var tr = "\
                <tr >\
                    <td colspan='3' class='text-center tdNull'>No hay instituciones para mostrar</td>\
                </tr>\
            ";
        return tr;
    }