// scripts
    function getTrRevista(revista) {
        var tr = "\
        <td class='hidden'>\
            \
        </td>\
        <td>\
            " + revista._revista + "\
        </td>\
        <td>\
            " + revista._categoria + "\
        </td>\
        <td>\
            " + revista._anioPublicacion + "\
        </td>\
        <td>\
            <button class='btn btnEliminarRevista'>\
                Eliminar\
            </button>\
            <button class='btn btnModificarRevista'>\
                Modificar\
            </button>\
        </td>\
        ";
        return tr;
    }
    function tabRevistas(frm) {
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_getRevistasInstitucion", frm, function (data) {
            console.log("La respuesta TAB REVISTAS es", data);
            if (data.estado) {
                if (data.revistasInstitucion !== undefined && data.revistasInstitucion != null) {
                    var tr = "";
                    $.each(data.revistasInstitucion, function (i, revista) {
                         tr += getTrRevista(revista);
                    })
                    $(".tbBodyRevista").empty().append(tr);
                }
            }
        })
    }
    function btnAddRevista(frm) {
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_addRevistaInstitucion", frm, function (data) {
            console.log("la respuesta es: ", data);
        })
    }
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