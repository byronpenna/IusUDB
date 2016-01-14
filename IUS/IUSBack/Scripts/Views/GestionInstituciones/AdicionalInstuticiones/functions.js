function btnGuardarNivel(frm) {
    frm.strEstadoNivel = "";
    if (frm.estadoNivel !== undefined && frm.estadoNivel != null && frm.estadoNivel.length > 0) {
        console.log("entro");
        $.each(frm.estadoNivel, function (i, val) {
            if (i == 0) {
                frm.strEstadoNivel += val;
            } else {
                frm.strEstadoNivel += "," + val;
            }
        })
    } else {
        console.log("entro al else");
    }
    console.log("Antes de formulario",frm);
    actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_insertNivelInstituciones", frm, function (data) {
        console.log(data);
        if (data.estado) {
            printMessage($(".divGeneralesNivelEducativo"), "Niveles actualizados correctamente", true);
        } else {
            printMessage($(".divGeneralesNivelEducativo"), "Ocurrio un error intentando actualizar", false);
        }
    });
}