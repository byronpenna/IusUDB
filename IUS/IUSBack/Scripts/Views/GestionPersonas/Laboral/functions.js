// genericas 
    // texto 
        // cb
            function getCbCargos(cargo) {
                var cb = "<option value='"+cargo._idCargoEmpresa+"'>" + cargo._cargo + "</option>";
                return cb;
            }
            //function getCbInstituciones(insti)
            function getCbInstituciones(institucion)
            {
                var cb = "<option value='" + institucion._idInstitucion + "'>" + institucion._nombre + "</option>";
                return cb;
            }
        // tr
            function getTrLaboralPersona(laboralPersona) {
                /*<input type='hidden' value='"+laboralPersona._empresa._idEmpresa+"' class='txtHdIdEmpresa' name='txtHdIdEmpresa'>\*/
                var tr = "\
                <tr>\
                    <td class='hidden'>\
                        <input type='hidden' value='"+laboralPersona._idLaboralPersona+"' class='txtHdIdLaboralPersona' name='txtHdIdLaboralPersona' />\
                        <input type='hidden' value='"+laboralPersona._cargo._idCargoEmpresa+"' class='txtHdIdCargoEmpresa' name='txtHdIdCargoEmpresa'>\
                    </td>\
                    <td>\
                        <div class='editMode hidden'>\
                            <select class='form-control cbEmpresa' name='cbEmpresa'>\
                            </select>\
                        </div>\
                        <div class='normalMode tdNombreEmpresa'>\
                            " + laboralPersona._institucion._nombre + "\
                        </div>\
                    </td>\
                    <td>\
                        <div class='editMode hidden'>\
                            <input type='number' class='form-control txtInicio' name='txtInicio' />\
                        </div>\
                        <div class='normalMode tdFechaInicio'>\
                            "+laboralPersona._inicio+"\
                        </div>\
                    </td>\
                    <td>\
                        <div class='editMode hidden'>\
                            <input type='number' class='form-control txtFin' name='txtFin' />\
                        </div>\
                        <div class='normalMode tdFechaFin'>\
                            "+laboralPersona._fin+"\
                        </div>\
                    </td>\
                    <td>\
                        <div class='editMode hidden'>\
                            <select class='form-control cbCargo' name='cbCargo'>\
                            </select>\
                        </div>\
                        <div class='normalMode tdCargo'>\
                            "+laboralPersona._cargo._cargo+"\
                        </div>\
                    </td>\
                    <td class='tdEditActividades'>\
                        <div class='editMode hidden'>\
                            <div class='btn-group'>\
                                <button class='btn btnActualizarLaboralPersona btn-default btn-xs'>Actualizar</button>\
                                <button class='btn btnCancelarUni btn-default btn-xs'>Cancelar</button>\
                            </div>\
                        </div>\
                        <div class='normalMode tdBotones'>\
                            <div class='btn-group'>\
                                <button class='btn btnEditarLaboralPersona btn-xs btn-default'>Editar</button>\
                                <button class='btn btnActividad btn-xs btn-default'>Actividades realizadas</button>\
                                <button class='btn btnEliminarLaboralPersona btn-xs btn-default'>Eliminar</button>\
                            </div>\
                        </div>\
                    </td>\
                </tr>\
                ";
                return tr;
            }
        // table
            function getTrActividadTabla(actividad) {
                var tr = "\
                <tr class='trEliminarActividad'>\
                    <td class='hidden'>\
                        <input class='txtHdIdActividadEmpresa' name='txtHdIdActividadEmpresa' value='" + actividad._idActividadesEmpresa + "'>\
                    </td>\
                    <td>\
                        <div class='editMode hidden'>\
                            <div class='row marginNull divControl'>\
                                <input class='form-control txtActividad' name='txtActividad'>\
                                <div class='row marginNull divResultado hidden'>\
                                    _\
                                </div>\
                            </div>\
                        </div>\
                        <div class='normalMode tdActividad'>\
                            " + actividad._actividad + "\
                        </div>\
                    </td>\
                    <td class='tdEditActividades'>\
                        <div class='editMode hidden'>\
                            <div class='btn-group'>\
                                <button class='btnActualizarActividadEmpresa btn btn-default btn-xs'>Actualizar</button>\
                                <button class='btn btnCancelarUni btn-default btn-xs' >Cancelar</button>\
                            </div>\
                        </div>\
                        <div class='normalMode'>\
                            <div class='btn-group'>\
                                <button class='btnEditarActividad btn btn-xs btn-default' >Editar</button>\
                                <button class='btnEliminarActividad btn btn-xs btn-default'>Eliminar</button>\
                            </div>\
                        </div>\
                    </td>\
                </tr>\
                ";
                return tr;
            }
            function getTrTableActividades(idLaboralPersona, actividades) {
                var tr = "\
                <tr class='trTable'>\
                    <td class='hidden'>\
                        <input class='txtHdIdLaboralPersona' name='txtHdIdLaboralPersona' value='"+idLaboralPersona+"'>\
                    </td>\
                    <td colspan='6'>\
                        <table class='table tablaActividadesEmpresa'>\
                            <thead>\
                                <tr>\
                                    <td class='text-center titleTrTable' colspan='2'>Actividades realizadas</td>\
                                </tr>\
                                <tr>\
                                    <th>Actividad realizada</th>\
                                    <th>Acciones</th>\
                                </tr>\
                                <tr class='trAgregar'>\
                                    <th>\
                                        <div class='row marginNull divControl'>\
                                            <input name='txtActividad' class='form-control txtActividad input-sm' />\
                                            <div class='row marginNull divResultado hidden'>\
                                                _\
                                            </div>\
                                        </div>\
                                    </th>\
                                    <th>\
                                        <button class='btnAgregarActividad btn btn-xs btn-default'>Agregar</button>\
                                    </th>\
                                </tr>\
                            <thead>\
                            <tbody class='tbodyActividades'>";
                if (actividades !== undefined && actividades != null && actividades.length > 0) {
                    $.each(actividades, function (i, actividad) {
                        tr += "\
                        <tr class='trEliminarActividad'>\
                            <td class='hidden'>\
                                <input class='txtHdIdActividadEmpresa' name='txtHdIdActividadEmpresa' value='" + actividad._idActividadesEmpresa + "'>\
                            </td>\
                            <td>\
                                <div class='editMode hidden'>\
                                    <div class='row marginNull divControl'>\
                                        <input class='form-control txtActividad input-sm' name='txtActividad'>\
                                        <div class='row marginNull divResultado hidden'>\
                                            _\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class='normalMode tdActividad'>\
                                    " + actividad._actividad + "\
                                </div>\
                            </td>\
                            <td class='tdEditActividades'>\
                                <div class='editMode hidden'>\
                                    <div class='btn-group'>\
                                        <button class='btnActualizarActividadEmpresa btn btn-xs btn-default'>Actualizar</button>\
                                        <button class='btn btnCancelarAct btn-xs btn-default' >Cancelar</button>\
                                    </div>\
                                </div>\
                                <div class='normalMode'>\
                                    <div class='btn-group'>\
                                        <button class='btnEditarActividad btn btn-xs btn-default' >Editar</button>\
                                        <button class='btnEliminarActividad btn btn-xs btn-default'>Eliminar</button>\
                                    </div>\
                                </div>\
                            </td>\
                        </tr>\
                        "
                    })
                } else {
                    tr += "\
                    <tr class='trNoActividad'>\
                        <td class='text-center' colspan='2'>\
                            Aun no se le agregan actividades que desempeño\
                        </td>\
                    </tr>\
                    ";
                }
                tr += "\
                            </tbody>\
                        </table>\
                    </td>\
                </tr>\
                ";
                return tr;
            }
    // otras
        function frmCurriculumn(data,url) {
            console.log("entro ");
            accionAjaxWithImage(url, data, function (data) {
                console.log("La data es", data);
                var icoCurriculumn = $(".icoCurriculumn");
                if (data.estado) {
                    if ($(".txtHdTieneVitae").val() == 0) {
                        $(".txtHdTieneVitae").val(1);
                        icoCurriculumn.attr("src", IMG_GENERALES + "repositorio/adobe-reader.png");
                        icoCurriculumn.parents("a").attr("href", data.informacionPersona._curriculumn);
                        icoCurriculumn.parents("a").removeClass("noHref");
                    }
                    printMessage($(".divMensajesGenerales"), "Curriculumn se actualizo correctamente", true);
                } else {
                    printMessage($(".divMensajesGenerales"), "Ocurrio un error", false);
                }
            })
        }

        function getTableActividades(tr) {
            var frm = { idLaboralPersona: tr.find(".txtHdIdLaboralPersona").val() }; 
            actualizarCatalogo(RAIZ + "GestionLaboral/sp_rrhh_getActividadesEmpresa", frm, function (data) {
                
                var actividades = null;
                if(data.actividadesEmpresa !== undefined && data.actividadesEmpresa != null){
                    actividades = data.actividadesEmpresa;
                }
                var tabla = getTrTableActividades(tr.find(".txtHdIdLaboralPersona").val(), actividades);
                tr.after(tabla);
            })
        }
        function getObjetoSetEditLaboral(tr) {
            var datosSet = new Object();
            // recolectando datos
                datosSet.fechaInicio = $.trim(tr.find(".tdFechaInicio").text());
                datosSet.fechaFin = $.trim(tr.find(".tdFechaFin").text());
                datosSet.idInstitucion = tr.find(".txtHdIdInstitucion").val();
                datosSet.idCargoEmpresa = tr.find(".txtHdIdCargoEmpresa").val();
                datosSet.observaciones = $.trim(tr.find(".tdObservaciones").text());
            return datosSet;
        }
        function verCargos(estado, tr) {
            if (estado) {

            } else {
                //$(".tableUsuarios").find(".trTableRol").remove();
                tr.parents("table").find(".trTable").remove();
            }
        }
        function validarInsertActividad(frm) {
            var val = new Object();
            val.campos = {
                txtActividad: new Array(),
            }
            if (frm.txtActividad == "") {
                val.campos.txtActividad.push("-Debe llenar el campo <br>");
            }
            val.estado = objArrIsEmpty(val.campos);
            return val;
        }
        function validarInsertLaboral(frm) {
            var val = new Object();
            val.campos = {
                cbCargo:            new Array(),
                //cbEmpresa:          new Array(),
                txtAreaObservacion: new Array(),
                txtFin:             new Array(),
                txtInicio:          new Array()
            }
            console.log(frm);
            // vacios
                /*if (frm.txtAreaObservacion == "") {
                    val.campos.txtAreaObservacion.push("Campo no debe quedar vacio");
                }*/
                if (frm.txtFin == "") {
                    val.campos.txtFin.push("-Llenar campo<br>");
                }
                if (frm.txtInicio == "") {
                    val.campos.txtInicio.push("-Llenar campo<br>");
                }
            // fecha 
                if (!(frm.txtFin >= 1970 && frm.txtFin <= 2100)) {
                    val.campos.txtFin.push("-Favor colocar una fecha coherente");
                }
                if (!(frm.txtInicio >= 1970 && frm.txtInicio <= 2100)) {
                    val.campos.txtInicio.push("-Favor colocar una fecha coherente");
                }
                
           val.estado = objArrIsEmpty(val.campos);
                if (frm.txtInicio > frm.txtFin) { // >= para que si esta trabajando ponga la misma fecha
                    val.estado = false;
                    printMessage($(".divMensajesGenerales"), "La fecha de inicio debe ser menor que la de fin", false);
                }
            return val;
        }

        function limpiarAgregar(div) {
            div.find(".txtInicio").val("");
            div.find(".txtFin").val("");
            div.find(".txtAreaObservacion").val("");
        }
// acciones 
    // actividades
        function btnActualizarActividadEmpresa(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_editarActividadEmpresa", frm, function (data) {
                console.log("D: D: D: ", data);
                if (data.estado) {
                    if (data.actividadEditada !== undefined && data.actividadEditada != null) {
                        //************************
                        tr.find(".tdActividad").empty().append(data.actividadEditada._actividad);
                        controlesEdit(false, tr);
                    }
                }
            })
        }
        function btnEliminarActividad(frm, tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarActividadadesEmpresa", frm, function (data) {
                console.log("Data de eliminar", data);
                if (data.estado) {
                    tr.remove();
                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        function btnAgregarActividad(frm, tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertActividadEmpresa", frm, function (data) {
                console.log("Actividad ingresada ", data);
                if (data.estado) {
                    if (data.actividadIngresada !== undefined && data.actividadIngresada != null) {
                        var htmlTr = getTrActividadTabla(data.actividadIngresada);
                        //console.log("D: ingresaras tr",htmlTr);
                        if ($(".trNoActividad").length == 1) {
                            tr.parents("table").find(".tbodyActividades").empty();   
                        }
                        tr.parents("table").find(".tbodyActividades").prepend(htmlTr);
                        tr.find(".txtActividad").val("");
                        
                    }
                
                }
            })
        }
    // laboral
        function btnAgregarLaboralPersona(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertLaboralPersonas", frm, function (data) {
                console.log("la data es", data);
                if (data.estado) {
                    console.log("Entro aqui");
                    tr.find(".divResultado").removeClass("visibilitiHidden");
                    tr.find(".divResultado").addClass("hidden");
                    limpiarAgregar(tr);
                    var trAgregar = getTrLaboralPersona(data.laboralAgregada);
                    var tbody       = $(".tbodyLaboralPersona");
                    if (tbody.find(".trNoRegistro").length == 0) {
                        printMessage($(".divMensajesGenerales"), "Agregado correctamente", true);
                        tbody.prepend(trAgregar);
                    } else {
                        tbody.empty().append(trAgregar);
                    }
                } else {
                    console.log("Entro al else");
                    var mjs = "Ocurrio un error";
                    printMessage($(".divMensajesGenerales"), mjs, false);
                    //alert("Ocurrio un error");
                }
            })
        }
        
        function btnActualizarLaboralPersona(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_editarLaboralPersonas", frm, function (data) {
                console.log("la respuesta es: ", data);
                if (data.estado) {
                    var laboral = data.laboralEditado;
                    // texto
                        tr.find(".tdFechaInicio").empty().append(laboral._inicio);
                        tr.find(".tdFechaFin").empty().append(laboral._fin);
                        tr.find(".tdObservaciones").empty().append(laboral._observaciones)
                        tr.find(".tdCargo").empty().append(laboral._cargo._cargo)
                        tr.find(".tdNombreInstitucion").empty().append(laboral._institucion._nombre);
                    // hiddens 
                        tr.find(".txtHdIdCargoEmpresa").val(laboral._cargo._idCargoEmpresa);
                        tr.find(".txtHdIdInstitucion").val(laboral._institucion._idInstitucion);
                    controlesEdit(false, tr);
                } else {
                    alert("Ocurrio un error tratand de actualizar");
                }
            })
        }
        function btnEliminarLaboralPersona(frm, tr) {
            //**
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarLaboralPersonas", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    printMessage($(".divMensajesGenerales"), "Eliminado correctamente", true);
                    tr.remove();
                } else {
                    var mjs = "Ocurrio un error al tratar de eliminar";
                    if (data.error._mostrar) {
                        mjs = data.error.Message;
                    }
                    printMessage($(".divMensajesGenerales"), mjs , false);
                }
            })
        }