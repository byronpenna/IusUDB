// scripts
    function btnGuardarCambiosOtros(frm) {
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/guardarOtrosInstituciones", frm, function (data) {
            if (data.estado) {

            }
        })
    }
    function getObjRevista(trRevista) {
        var revista = {
            _revista: $.trim(trRevista.find(".tdRevista").text()),
            _categoria: $.trim(trRevista.find(".tdCategoria").text()),
            _anioPublicacion: $.trim(trRevista.find(".tdAnioPublicacion").text())
        }
        return revista;
    }
    function editMode(trRevista) {
        var revista = getObjRevista(trRevista);
        console.log("Revista es: ", revista);
        trRevista.find(".txtNombreRevista").val(revista._revista);
        trRevista.find(".txtCategoria").val(revista._categoria);
        trRevista.find(".txtAnioPublicacion").val(revista._anioPublicacion);

        controlesEdit(true, trRevista);
    }
    function btnAceptarEdicionRevista(frm) {
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_updateRevistaInstitucion", frm, function (data) {
            console.log("Update es: ", data);
            if (data.estado) {

            }
        })
    }
    function btnEliminarRevista(frm,tr) {
        actualizarCatalogo(RAIZ + "/AdicionalesInstituciones/sp_frontui_deleteRevistaInstitucion", frm, function (data) {
            console.log("La respuesta eliminar es: ", data);
            if (data.estado) {
                tr.remove();
            }
        })
    }
    function getTrRevista(revista) {
        var tr = "\
        <tr>\
            <td class='hidden'>\
                <input class='hidden txtHdIdRevista' name='txtHdIdRevista' value='" + revista._idRevistaInstitucion + "'>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input type='text' name='txtNombreRevista' class='inputBack input-sm form-control txtNombreRevista txtEdit' />\
                    <div class='row marginNull divResultado hidden'>\
                    </div>\
                </div>\
                <div class='normalMode tdRevista'>\
                    " + revista._revista + "\
                </div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input type='text' name='txtCategoria' class='inputBack input-sm form-control txtCategoria txtEdit' />\
                    <div class='row marginNull divResultado hidden'>\
                    </div>\
                </div>\
                <div class='normalMode tdCategoria'>\
                " + revista._categoria + "\
                </div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <input type='text' name='txtAnioPublicacion' class='inputBack input-sm form-control txtAnioPublicacion txtEdit' />\
                    <div class='row marginNull divResultado hidden'>\
                    </div>\
                </div>\
                <div class='normalMode tdAnioPublicacion'>\
                " + revista._anioPublicacion + "\
                </div>\
            </td>\
            <td>\
                <div class='editMode hidden'>\
                    <button class='btn btnAceptarEdicionRevista'>\
                        Aceptar\
                    </button>\
                    <button class='btn btnCancelarEdicionRevista'>\
                        Cancelar\
                    </button>\
                </div>\
                <div class='normalMode tdBotonesRevista'>\
                    <button class='btn btnEliminarRevista'>\
                        Eliminar\
                    </button>\
                    <button class='btn btnModificarRevista'>\
                        Modificar\
                    </button>\
                </div>\
            </td>\
        </tr>\
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