// genericas 
    // texto 
        // cb
            function getCbCargos(cargo) {
                var cb = "<option value='"+cargo._idCargoEmpresa+"'>" + cargo._cargo + "</option>";
                return cb;
            }
            function getCbEmpresas(empresa)
            {
                var cb = "<option value='"+empresa._idEmpresa+"'>"+empresa._nombre+"</option>";
                return cb;
            }
        // tr
            function getTrLaboralPersona(laboralPersona) {
            var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input type='hidden' value='"+laboralPersona._idLaboralPersona+"' class='txtHdIdLaboralPersona' name='txtHdIdLaboralPersona' />\
                    <input type='hidden' value='"+laboralPersona._empresa._idEmpresa+"' class='txtHdIdEmpresa' name='txtHdIdEmpresa'>\
                    <input type='hidden' value='"+laboralPersona._cargo._idCargoEmpresa+"' class='txtHdIdCargoEmpresa' name='txtHdIdCargoEmpresa'>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='form-control cbEmpresa' name='cbEmpresa'>\
                        </select>\
                    </div>\
                    <div class='normalMode tdNombreEmpresa'>\
                        "+laboralPersona._empresa._nombre+"\
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
                        <textarea class='form-control txtAreaObservacion' name='txtAreaObservacion'></textarea>\
                    </div>\
                    <div class='normalMode tdObservaciones'>\
                        "+laboralPersona._observaciones+"\
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
                <td>\
                    <div class='editMode hidden'>\
                        <button class='btn btnActualizarLaboralPersona'>Actualizar</button>\
                        <button class='btn btnCancelarUni'>Cancelar</button>\
                    </div>\
                    <div class='normalMode tdBotones'>\
                        <button class='btn btnEditarLaboralPersona'>Editar</button>\
                        <button class='btn btnEliminarLaboralPersona'>Eliminar</button>\
                    </div>\
                </td>\
            </tr>\
            ";
            return tr;
        }
        // table
            function getTrTableActividades(idLaboralPersona,actividades) {
                var tr = "\
                <tr class='trTable'>\
                    <td class='hidden'>\
                        <input class='txtHdIdLaboralPersona' name='txtHdIdLaboralPersona' value='"+idLaboralPersona+"'>\
                    </td>\
                    <td colspan='6'>\
                        <table class='table tablaActividadesEmpresa'>\
                            <thead>\
                                <tr>\
                                    <td class='text-center' colspan='2'>Actividades realizadas</td>\
                                </tr>\
                                <tr>\
                                    <th>Actividad realizada</th>\
                                    <th>Acciones</th>\
                                </tr>\
                                <tr>\
                                    <th><input name='txtActividad' class='form-control txtActividad' /></th>\
                                    <th>\
                                        <button class='btnAgregarActividad btn'>Agregar</button>\
                                    </th>\
                                </tr>\
                            <thead>\
                            <tbody>";
                if (actividades !== undefined && actividades != null && actividades.length > 0) {
                    $.each(actividades, function (i, actividad) {
                        tr += "\
                        <tr class='trEliminarActividad'>\
                            <td class='hidden'>\
                                <input class='txtHdIdActividadEmpresa' name='txtHdIdActividadEmpresa' value='" + actividad._idActividadesEmpresa + "'>\
                            </td>\
                            <td>" + actividad._actividad + "</td>\
                            <td>\
                                <button class='btnEliminarActividad btn'>Eliminar</button>\
                                <button class='btnEditar btn' >Editar</button>\
                            </td>\
                        </tr>\
                        "
                    })
                } else {
                    tr += "\
                    <tr>\
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
         function getTableActividades(tr) {
            var frm = { idLaboralPersona: tr.find(".txtHdIdLaboralPersona").val() }; 
            console.log("traer ", frm);
            actualizarCatalogo(RAIZ + "GestionLaboral/sp_rrhh_getActividadesEmpresa", frm, function (data) {
                console.log("trajimonos D: ", data);
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
                datosSet.idEmpresa = tr.find(".txtHdIdEmpresa").val();
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
// acciones 
    // actividades
        function btnEliminarActividad(frm,tr) {
            actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_eliminarActividadadesEmpresa", frm, function (data) {
                console.log("Data de eliminar", data);
                if (data.estado) {
                    tr.remove();
                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        function btnAgregarActividad(frm) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertActividadEmpresa", frm, function (data) {
            console.log("Actividad ingresada ", data);
            if (data.estado) {

            }
        })
    }
    // laboral
        function btnAgregarLaboralPersona(frm) {
        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_insertLaboralPersonas", frm, function (data) {
            console.log("Data devuelta por el servidor",data);
            if(data.estado){
                var trAgregar = getTrLaboralPersona(data.laboralAgregada);
                $(".tbodyLaboralPersona").prepend(trAgregar);
            } else {
                alert("Ocurrio un error");
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
                        tr.find(".tdNombreEmpresa").empty().append(laboral._empresa._nombre);
                    // hiddens 
                        tr.find(".txtHdIdCargoEmpresa").val(laboral._cargo._idCargoEmpresa);
                        tr.find(".txtHdIdEmpresa").val(laboral._empresa._idEmpresa);
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
                    tr.remove();
                }
            })
        }