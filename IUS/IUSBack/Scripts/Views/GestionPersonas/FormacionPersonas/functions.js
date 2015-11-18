    function validarIngresoInstituciones(frm) {
        var val = new Object();
        val.campos = {
            cbPaisInstitucionEducativa: new Array(),
            txtInstitucionEducativa: new Array()
        }
        if (frm.cbPaisInstitucionEducativa == -1) {
            val.campos.cbPaisInstitucionEducativa.push("Este campo no puede quedar vacio")
        }
        if (frm.txtInstitucionEducativa == "") {
            val.campos.txtInstitucionEducativa.push("Este campo no puede quedar vacio")
        }
        val.estado = objArrIsEmpty(val.campos);
        console.log("val es D: ", val);
        return val;
    }
    function validarIngresoCarreraIndividual(frm) {
        var val = new Object();
        val.campos = {
            cbInsticionesParaCarrera:   new Array(),
            cbNivelCarrera:             new Array(),
            txtNombreCarrera:           new Array()
        }
        if (frm.cbInsticionesParaCarrera == -1) {
            val.campos.cbInsticionesParaCarrera.push("Este campo no puede quedar vacio")
        }
        if (frm.cbNivelCarrera == -1) {
            val.campos.cbNivelCarrera.push("Este campo no puede quedar vacio")
        }
        if (frm.txtNombreCarrera == "") {
            val.campos.txtNombreCarrera.push("Este campo no puede quedar vacio")
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function validarAgregarCarrera(frm) {
        var val = new Object();
        val.campos = {
            cbAreaCarrera:              new Array(),
            cbNivelCarrera:             new Array(),
            cbPaisInstitucionEducativa: new Array(),
            txtAreaObservaciones:       new Array(),
            txtCarrera:                 new Array(),
            txtInstitucionEducativa:    new Array(),
            txtYearFin:                 new Array()
        }
        if (frm.cbPaisInstitucionEducativa == -1) {
            val.campos.cbPaisInstitucionEducativa.push("Seleccione un país");
        }
        if (frm.txtAreaObservaciones == "") {
            val.campos.txtAreaObservaciones.push("Debe llenar este campo");
        }
        if (frm.txtCarrera == "") {
            val.campos.txtCarrera.push("Debe llenar este campo");
        }
        if (frm.txtInstitucionEducativa == "") {
            val.campos.txtInstitucionEducativa.push("Debe llenar este campo");
        }
        if (frm.txtYearFin == "") {
            val.campos.txtYearFin.push("Debe llenar este campo");
        }
        if ( !(frm.txtYearFin >= 1970 && frm.txtYearFin <= 2100) ) {
            val.campos.txtYearFin.push("Favor colocar fecha coherente");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
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
                            <input class='txtHdIdInstitucion' value='"+carrera._institucion._idInstitucion+"'/>\
                            <input class='txtHdIdNivelCarrera' value='" + carrera._nivelTitulo._idNivel + "' />\
                            <input class='txtHdIdArea' value='" + carrera._area._idArea + "'>\
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
                                <select name='cbAreaCarreras' class='form-control cbAreaCarreras'></select>\
                            </div>\
                            <div class='normalMode tdAreaCarrera'>\
                                "+carrera._area._area+"\
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
                            <input name='txtHdIdNivel' class='txtHdIdNivel' value='"+formacionPersona._nivelTitulo._idNivel+"'>\
                            <input name='txtHdIdNivel' class='txtHdIdArea' value='"+formacionPersona._areaCarrera._idArea+"'>\
                            <input name='txtHdIdPais' class='txtHdIdPais' value='"+formacionPersona._paisInstitucion._idPais+"' />\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <input type='text' name='txtCarrera' class='form-control txtCarrera' />\
                            </div>\
                            <div class='normalMode tdCarrera'>\
                                " + formacionPersona._carrera + "\
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
                                <select name='cbNivelCarrera' class='cbNivelCarrera form-control'></select>\
                            </div>\
                            <div class='normalMode tdNivelTitulo'>\
                                "+formacionPersona._nivelTitulo._nombreNivel+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select name='cbAreaCarrera' class='cbAreaCarrera form-control'></select>\
                            </div>\
                            <div class='normalMode tdAreaCarrera'>\
                                "+formacionPersona._areaCarrera._area+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <input class='form-control txtInstitucionEducativa' name='txtInstitucionEducativa' />\
                            </div>\
                            <div class='normalMode tdInstitucion'>\
                                "+formacionPersona._institucion+"\
                            </div>\
                        </td>\
                        <td>\
                            <div class='editMode hidden'>\
                                <select name='cbPaisInstitucionEducativa' class=' cbChosenPais form-control cbPaisInstitucionEducativa'>\
                                </select>\
                            </div>\
                            <div class='normalMode tdPais'>\
                                "+formacionPersona._paisInstitucion._pais+"\
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
            // conjuntos
                function getCbsEditTitulos(data){
                    var cb = new Object();
                    cb.cbEstadoCarrera = "";cb.cbNiveles = "";
                    if (data.estadosCarreras !== undefined && data.estadosCarreras != null && data.estadosCarreras.length > 0) {
                        $.each(data.estadosCarreras, function (i,estadoCarrera) {
                            cb.cbEstadoCarrera += getCbEstadosCarreras(estadoCarrera);
                        })
                    }
                    if (data.nivelesCarreras !== undefined && data.nivelesCarreras != null && data.nivelesCarreras.length > 0) {
                        $.each(data.nivelesCarreras, function (i, nivelTitulo) {
                            cb.cbNiveles += getCbNiveles(nivelTitulo);
                        })
                    }
                    if (data.areasCarreras !== undefined && data.areasCarreras != null && data.areasCarreras.length > 0) {
                        $.each(data.areasCarreras, function (i, areaCarrera) {
                            cb.cbAreasCarreras += getCbAreas(areaCarrera);
                        })
                    }
                    if (data.paises !== undefined && data.paises != null && data.paises.length > 0) {
                        $.each(data.paises, function (i, pais) {
                            cb.cbPaises        += getCbPaises(pais);
                        })
                    }
                    return cb;
                }
            // individuales
                function getCbAreaCarrera(areaCarrera) {
                    var cb = "<option value="+areaCarrera._idArea+">"+areaCarrera._area+"</option>";
                    return cb;
                }
                function getCbAreas(areaCarrera) {
                    var cb = "<option value="+areaCarrera._idArea+">"+areaCarrera._area+"</option>";
                    return cb;
                }
                function getCbNiveles(nivelTitulo) {
                    var cb = "<option value=" + nivelTitulo._idNivel + ">" + nivelTitulo._nombreNivel + "</option>";
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
                    console.log("La data es: ",data);
                    if (data.estado) {
                        tr.find(".tdNombreInstitucion").empty().append(data.institucionEditada._nombre);
                        tr.find(".tdPais").empty().append(data.institucionEditada._pais._pais);
                        tr.find(".txtHdIdPaisInstitucion").val(data.institucionEditada._pais._idPais)
                        controlesEdit(false, tr);
                    } else {
                        printMessage($(".divResultadoGeneralInstituciones"), "Ocurrio un error", false);
                    }
                    
                })
            }
        function btnAgregarInstitucion(frm,funcion) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarInstitucionEducativa", frm, function (data) {
                //console.log("Data servidor", data);
                
                if (data.estado) {
                    var tr = getTrInstitucionEducativa(data.institucionEducativa);
                    
                    $(".tbTablaFormacionPersonas").prepend(tr);
                    if (funcion !== undefined) {
                        funcion(data);
                    }
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
        /*function btnActualizarCarrera(frm, tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarCarrera", frm, function (data) {
                console.log("data del serivdor actualizados", data);
                if (data.estado) {
                    var carrera = data.carreraEditada;
                    tr.find(".txtHdIdCarrera").val(carrera._idCarrera);
                    tr.find(".txtHdIdNivelCarrera").val(carrera._nivelTitulo._idNivel);
                    tr.find(".tdCarrera").empty().append(carrera._carrera);
                    tr.find(".tdAreaCarrera").empty().append(carrera._area._area);

                    tr.find(".tdNivelTitulo").empty().append(carrera._nivelTitulo._nombreNivel)
                    tr.find(".tdInstitucion").empty().append(carrera._institucion);
                    console.log("D: ");
                    //tr.find(".txtHdIdInstitucion").val(carrera._institucion._idInstitucion);
                    tr.find(".txtHdIdArea").val(carrera._area._idArea);
                    //tdAreaCarrera
                    controlesEdit(false, tr);
                } else {
                    alert("ocurrio un error");
                }
            })
        }*/
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
                    var mjs = "Ocurrio un error";
                    if (data.error._mostrar) {
                        mjs =  data.error.Message;
                    }
                    printMessage($(".divResultadoGeneralCarrera"),mjs, false);
                    //alert("Ocurrio un error")
                }
            })
        }      
    // formacion personas
        function btnActualizarTituloPersona(frm,tr) {
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_editarFormacionPersona", frm, function (data) {
                console.log("la data es de actualizacion es:", data);
                if (data.estado) {
                    var formacion = data.formacionEditada;
                    // inputs hidden
                        /*console.log("id carrera D: ", formacion._carrera._idCarrera);
                        tr.find(".txtHdIdCarrera").val(formacion._carrera._idCarrera);
                        tr.find(".txtHdIdEstadoCarrera").val(formacion._estado._idEstadoCarrera);*/
                        tr.find(".txtHdIdPais").val(formacion._paisInstitucion._idPais);
                        tr.find(".txtHdIdNivel").val(formacion._nivelTitulo._idNivel);
                        tr.find(".txtHdIdArea").val(formacion._areaCarrera._idArea);
                    // cosas visibles 
                        tr.find(".tdCarrera").empty().append(formacion._carrera);//formacion._carrera._carrera
                        tr.find(".tdYearInicio").empty().append(formacion._yearInicio);
                        tr.find(".tdYearFin").empty().append(formacion._yearFin);
                        tr.find(".tdObservaciones").empty().append(formacion._observaciones);
                        
                        tr.find(".tdNivelTitulo").empty().append(formacion._nivelTitulo._nombreNivel);
                        tr.find(".tdAreaCarrera").empty().append(formacion._areaCarrera._area);
                        tr.find(".tdInstitucion").empty().append(formacion._institucion);
                        tr.find(".tdPais").empty().append(formacion._paisInstitucion._pais);
                        controlesEdit(false, tr);
                } else {
                    alert("Ocurrio un error");
                }
            })
        }
        function limpiarInputAgregar() {
            $(".txtCarrera").val("");
            $(".txtYearFin").val("");
            $(".txtAreaObservaciones").val("");
            $(".txtInstitucionEducativa").val("");

        }
        function btnAgregarCarrera(frm) { // agrega formacion de personas a persar del nombre raro
            actualizarCatalogo(RAIZ + "/FormacionPersonas/sp_rrhh_ingresarFormacionPersona", frm, function (data) {
                console.log("la data es", data);
                if (data.estado) {
                    var tr = getTrFormacionPersonas(data.formacionAgregada);
                    var trFrm = $(".trAgregar");
                    limpiarInputAgregar();
                    //clearTrWithOutHidden(trFrm);
                    $(".tbodyFormacionPersonas").prepend(tr);
                    printMessage($(".divMensajesAgregar"), "Agregado correctamente", true);
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
                    printMessage($(".divMensajesAgregar"), "Registro eliminado correctamente", true);
                }
            })
        }