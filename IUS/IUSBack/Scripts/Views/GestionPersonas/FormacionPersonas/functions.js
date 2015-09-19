// genericas
    // partes
        // tr     
            function getTrInstitucionEducativa(institucion) {
                var tr = "<tr>\
                       <td class='hidden'>\
                           <input name='txtHdIdInstitucionEducativa' class='txtHdIdInstitucionEducativa' value='"+institucion._idInstitucion+"' />\
                           <input name='txtHdIdPaisInstitucion' class='txtHdIdPaisInstitucion' value='"+institucion._pais._idPais+"'/>\
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                               <input class='txtInstitucionEducativa form-control' name='txtInstitucionEducativa' />\
                           </div>\
                           <div class='normalMode tdNombreInstitucion'>\
                               "+institucion._nombre+"\
                           </div>\
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                               <select name='cbPaisInstitucionEducativa' class=' cbChosenPais form-control cbPaisInstitucionEducativa'></select>\
                           </div>\
                           <div class='normalMode tdPais'>\
                               "+institucion._pais._pais+"\
                           </div> \
                       </td>\
                       <td>\
                           <div class='editMode hidden'>\
                                <button class='btn btnActualizarInstitucionEducativa'>Actualizar</button>\
                                <button class='btn btnCancelarInstitucionEducativa btnCancelarUni'>Cancelar</button>\
                           </div>\
                           <div class='normalMode tdEmail'>\
                               <button class='btn btnEditarInstitucion'>Editar</button>\
                               <button class='btn btnEliminarInstitucion'>Eliminar</button>\
                           </div>\
                       </td>\
                   </tr>";
            return tr;
            }
            function getTrCarrera(carrera) {
                var tr = "\
                  <tr>\
                        <td class='hidden'>\
                            <input class='txtHdIdCarrera' name='txtHdIdCarrera' value='"+carrera._idCarrera+"'/>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <input name='txtNombreCarrera' class='txtNombreCarrera form-control' />\
                            </div>\
                            <div class='normalMode tdCarrera'>\
                                "+carrera._carrera+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select name='cbNivelCarrera' class='cbNivelCarrera cbChosenCarrera form-control'>\
                                </select>\
                            </div>\
                            <div class='normalMode tdEmail'>\
                                "+carrera._nivelTitulo._nombreNivel+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select class='cbInsticionesParaCarrera form-control' name='cbInsticionesParaCarrera'>\
                                </select>\
                            </div>\
                            <div class='normalMode tdEmail'>\
                                "+carrera._institucion._nombre+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <button class='btn btnActualizarCarrera'>Actualizar</button>\
                                <button class='btn btnCancelarUni'>Cancelar</button>\
                            </div>\
                            <div class='normalMode tdEmail'>\
                                <button class='btnEditarCarrera btn'>Editar</button>\
                                <button class='btn btnEliminarCarrera'>Eliminar</button>\
                            </div>\
                        </td>\
                    </tr>\
                ";
                return tr;
            }
            function getTrFormacionPersonas(formacionPersona) {
                var tr = "\
                    <tr>\
                        <td class='hidden'>\
                            <input name='txtHdIdFormacionPersona' class='txtHdIdFormacionPersona' value='"+ formacionPersona._idFormacionPersona + "'/>\
                            <input name='txtHdIdEstadoCarrera' class='txtHdIdEstadoCarrera' value='"+ formacionPersona._estado._idEstadoCarrera + "' />\
                            <input name='txtHdIdCarrera' class='txtHdIdCarrera' value='"+ formacionPersona._carrera._idCarrera + "' />\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select name='cbCarrera' class='cbCarrera form-control'></select>\
                            </div>\
                            <div class='normalMode tdCarrera'>\
                                "+ formacionPersona._carrera._carrera + "\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <input name='txtYearInicio' class='txtYearInicio form-control' />\
                            </div>\
                            <div class='normalMode tdYearInicio'>\
                                "+ formacionPersona._yearInicio + "\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <input name='txtYearFin' class='txtYearFin form-control' />\
                            </div>\
                            <div class='normalMode tdYearFin'>\
                                    "+formacionPersona._yearFin+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <textarea name='txtAreaObservaciones' class='txtAreaObservaciones form-control'></textarea>\
                            </div>\
                            <div class='normalMode tdObservaciones'>\
                                "+ formacionPersona._observaciones + "\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select name='cbEstadoCarrera' class='cbEstadoCarrera form-control'></select>\
                            </div>\
                            <div class='normalMode tdEstadoTitulo'>\
                                "+ formacionPersona._estado._estado + "\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <button class='btn btnActualizarTituloPersona'>Actualizar</button>\
                                <button class='btn btnCancelarUni'>Actualizar</button>\
                            </div>\
                            <div class='normalMode tdEmail'>\
                                <button class='btn btnEditarTitulos '>Editar</button>\
                                <button class='btn btnEliminarTitulo '>Eliminar</button>\
                            </div>\
                        </td>\
                    </tr>\
                ";
                return tr;
            }
        // cb
            function getCbCarrera(carrera) {
                var cb = "<option value="+carrera._idCarrera+">"+carrera._carrera+"</option>";
                return cb;
            }
            function getCbEstadosCarreras(estadoCarrera) {
                var cb = "<option value="+estadoCarrera._idEstadoCarrera+">"+estadoCarrera._estado+"</option>";
                return cb;
            }
            function getCbInstituciones(institucion) {
                var cb = "<option value="+institucion._idInstitucion+">"+institucion._nombre+"</option>";
                return cb;
            }
            function getCbNivelTitulo(nivelTitulo) {
                var cb = "<option value=" + nivelTitulo._idNivel + ">" + nivelTitulo._nombreNivel + "</option>";
                return cb;
            }
            function getCbPaises(pais) {
            var cb = "<option value=" + pais._idPais + " >" + pais._pais + "</option>";
            return cb;
        }
// acciones script
    // instituciones
        function btnActualizarInstitucionEducativa(frm,tr) {
                actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarInstitucionEducativa", frm, function (data) {
                    console.log("data al editar", data);
                })
            }
        function btnAgregarInstitucion(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
                if (data.estado) {
                    var tr = getTrInstitucionEducativa(data.institucionEducativa);
                    $(".tbTablaFormacionPersonas").prepend(tr);
                }
            })
        }
        function btnEliminarInstitucion(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarInstitucionEducativa", frm, function (data) {
                console.log("Data servidor", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
    // carreras
        function btnActualizarCarrera(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarCarrera", frm, function (data) {
                console.log("data del serivdor", data);
                if (data.estado) {
                    var carrera = data.carreraEditada;
                    tr.find(".txtHdIdCarrera").val(carrera._idCarrera);
                    tr.find(".tdCarrera").empty().append(carrera._carrera);

                    tr.find(".tdNivelTitulo").empty().append(carrera._nivelTitulo._nombreNivel)
                    tr.find(".tdInstitucionNombre").empty().append(carrera._institucion._nombre );

                    controlesEdit(false, tr);
                } else {
                    alert("ocurrio un error");
                }
            })
        }
        function btnEliminarCarrera(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarCarrera", frm, function (data) {
                console.log(data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }
        function btnAgregarCarreraIndividual(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarCarrera", frm, function (data) {
                console.log("Data servidor", data);
                if (data.estado) {
                    var tr = getTrCarrera(data.carreraAgregada);
                    $(".tbodyCarrera").prepend(tr);
                } else {
                    alert("Ocurrio un error")
                }
            })
        }      
    // formacion personas
        function btnActualizarTituloPersona(frm) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarFormacionPersona", frm, function (data) {
                console.log("la data es", data);
                if (data.estado) {

                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        function btnAgregarCarrera(frm) { // agrega formacion de personas a persar del nombre raro
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarFormacionPersona", frm, function (data) {
                console.log("la data es", data);
                if (data.estado) {
                    var tr = getTrFormacionPersonas(data.formacionAgregada);
                    $(".tbodyFormacionPersonas").prepend(tr);
                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        function btnEliminarTitulo(frm,tr){
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_eliminarTituloPersona", frm, function (data) {
                console.log("la data es:", data);
                if (data.estado) {
                    tr.remove();
                }
            })
        }