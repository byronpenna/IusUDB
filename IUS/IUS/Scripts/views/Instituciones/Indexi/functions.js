﻿function buscarContinente(frm) {
    var target = $(".tablaInstitucion").find("tbody");
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
            //$(".hContinente").empty().append(hContinente);
        }

    }, function () {
        var trLoading = "\
            <tr>\
                <td colspan='6' class='text-center'>\
                    <img src='" + IMG_GENERALES + "ajax-loader.gif'>\
                </td>\
            </tr>\
            ";
        target.empty().append(trLoading);
    })
}
    function getTrInstitucion(institucion) {
        var tel = ""; var enlaces = "";
        //console.log("los objetos de instituciones son", institucion);
        /*if (institucion._enlaces !== undefined && institucion._enlaces != null) {
            $.each(institucion._enlaces, function (i, enlace) {
                enlaces += "\
                    <div class='row marginNull'>\
                        <a href='"+ enlace._enlace + "'>\
                            " + enlace._nombreEnlace + "\
                        </a>\
                    </div>";
            })
        }*/
        if (institucion._telefonos !== undefined && institucion._telefonos != null) {
            $.each(institucion._telefonos, function (i, telelefono) {
                tel += "\
                    <div class='row marginNull divTel'>\
                        <a href='tel:" + telelefono._telefono + "'>\
                            " + telelefono._textoTelefono + "\
                        </a>\
                    </div>";
            })
        } else {
            tel = "No hay numeros asignados";
        }

        var tr = "\
            <tr>\
                <td>"+ institucion._pais._pais + " </td>\
                <td>" + institucion._nombre + "</td>\
                <td>" + tel + "</td>\
                <td>SR. JOSE RAMIREZ</td>\
                <td>JOSE@UDB.EDU.SV</td>\
            </tr>\
        ";
        /*<td>\
                    <a href='" + RAIZ + "/Instituciones/FichaInstitucion/" + institucion._idInstitucion + "' class='btn btn-default'>Ficha</a>\
                </td>\*/
        /*<div class='col-lg-6 mitadLinks'>\
                    <h4>Telefonos </h4>\
        " + tel + "\
                </div>\
                <div class='col-lg-6 mitadLinks'>\
                    <h4>Medios electronicos </h4>\
        "+enlaces+"\
                </div>\*/
        return tr;
    }
    function getTrInstitucionNull() {
        var tr = "\
                <tr >\
                    <td colspan='6' class='text-center tdNull'>No hay instituciones para mostrar</td>\
                </tr>\
            ";
        return tr;
    }