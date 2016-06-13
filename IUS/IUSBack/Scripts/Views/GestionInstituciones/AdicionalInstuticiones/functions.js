// scripts
    function btnGuardarAreaConocimiento(frm) {
        console.log("El formulario es", frm);
        frm.strAreaCarrera = "";
        if (frm.areaCarrera !== undefined && frm.areaCarrera != null && frm.areaCarrera.length > 0) {
            $.each(frm.areaCarrera, function (i, val) {
                if (i == 0) {
                    frm.strAreaCarrera += val;
                } else {
                    frm.strAreaCarrera += "," + val;
                }
            })
        }
        console.log("El formulario es 2", frm);
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_insertAreaConocimientoInstitucion", frm, function (data) {
            if (data.estado) {
                printMessage($(".divGeneralesAreasConocimiento"), "Niveles actualizados correctamente", true);
            } else {
                printMessage($(".divGeneralesAreasConocimiento"), "Ocurrio un error", false);
            }
        })
    }
    /*function guardarNivel(frm) {
        frm.strEstadoNivel = "";
        
    }*/
    function btnGuardarNivel(frm) {
        frm.strEstadoNivel = ""; frm.strNumAlumno = "";
        if (frm.estadoNivel !== undefined && frm.estadoNivel != null && frm.estadoNivel.length > 0) {
            console.log("entro");
            $.each(frm.estadoNivel, function (i, val) {
                if (i == 0) {
                    frm.strEstadoNivel += val;
                    frm.strNumAlumno += frm.alumnos[i];
                } else {
                    frm.strEstadoNivel += "," + val;
                    frm.strNumAlumno += ","+frm.alumnos[i];
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