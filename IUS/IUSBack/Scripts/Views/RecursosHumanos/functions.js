// genericas 
function getTrPersonas(persona) {
    var tr = "\
        <tr>\
            <td class='hidden'>\
                <input type='text' name='txtHdIdPersona' class='txtHdIdPersona' value='" + persona._idPersona + "'>\
            </td>\
            <td>" + persona.nombreCompleto + "</td>\
            <td>\
                <button class='btn btnVerFicha'>Ver ficha</button>\
            </td>\
        </tr>";
    return tr;
}
function getTrPersonasNull(loadIcon) {
    var strContenido = "";
    if (loadIcon === undefined || !loadIcon) {
        strContenido = "No se encontraron personas";
    } else {
        strContenido = "<img src='" + IMG_GENERALES + "ajax-loader.gif" + "'";
    }
    var tr = "\
        <tr>\
            <td colspan='2' class='text-center'>"+strContenido+"</td>\
        </tr>\
    ";
    return tr;
}
// acciones script 
    function btnVerFicha(frm) {
        actualizarCatalogo(RAIZ + "/RecursosHumanos/sp_rrhh_detallePesona", frm, function (data) {
            console.log("respuesta de servidor", data);

        })
    }
    function btnBusquedaPerfil(frm) {
        var target = $(".tbodyPersonas");
        actualizarCatalogo(RAIZ + "/RecursosHumanos/sp_rrhh_buscarPersonas", frm, function (data) {
            console.log("La data devuelta es", data);
            if (data.estado) {
                var tr = getTrPersonasNull();
                if (data.personas !== undefined && data.personas != null && data.personas.length > 0) {
                    console.log("entro aqui");
                    tr = "";
                    $.each(data.personas, function (i,persona) {
                        tr += getTrPersonas(persona);
                    })
                } else {
                    console.log("D: no entro");
                }
                target.empty().append(tr);
            } else {
                alert("Ocurrio un error");
            }
        }, function () {
            var tr = getTrPersonasNull(true);
            target.empty().append(tr);
        })
    }