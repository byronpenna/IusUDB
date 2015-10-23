$(document).ready(function () {
    // plugins 
        $(".cbChosenPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
        $(".cbChosenCarrera").chosen({ no_results_text: "Carrera no encontrada", width: '100%' });
        $(".cbChosenInstitucion").chosen({ no_results_text: "No se a encontrado institucion", width: '100%' });
        
    // eventos 
        // doble click  
            $(document).on("dblclick", ".titlePersona", function (e) {
                controlesEdit(true, $(this).parents(".divTituloNombre"));
            })
        // click 
            $(document).on("click", ".icoVolverAnombre", function () {
                controlesEdit(false, $(this).parents(".divTituloNombre"));
            })
            $(document).on("click", ".icoVista", function (e) {
                e.preventDefault();
                $(".divTab").addClass("hidden");
                $(".icoVista").removeClass("activeVista");
                var selector = $(this).attr("id");
                console.log("Selector a mostrar", selector);
                $("." + selector).removeClass("hidden");
                $(this).addClass("activeVista");
            })
            
            $(document).on("click", ".btnCancelarUni", function (e) {
                var tr = $(this).parents("tr");
                controlesEdit(false, tr);
            })
            // instituciones
                // edicion 
                $(document).on("click", ".btnEditarInstitucion", function (e) {
                    // variables
                        var tr = $(this).parents("tr");
                        var datosSet = new Object();
                    // recolectando datos
                        datosSet.nombreInstitucion = $.trim(tr.find(".tdNombreInstitucion").text());
                        datosSet.idPais = tr.find(".txtHdIdPaisInstitucion").val();
                        var cb = tr.find(".cbPaisInstitucionEducativa");
                    // seteando
                        tr.find(".cbPaisInstitucionEducativa").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
                        var frm = {};
                        console.log("data es para setear", datosSet);
                        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_getPaises", frm, function (data) {
                            if (data.estado && data.paises.length > 0) {
                                var ops = "";
                                $.each(data.paises, function (i, pais) {
                                    ops += getCbPaises(pais);
                                })
                                cb.empty().append(ops);
                            }
                            resetChosenWithSelectedVal(cb, datosSet.idPais);
                        })
                        
                        tr.find(".txtInstitucionEducativa").val(datosSet.nombreInstitucion);
                    controlesEdit(true, tr);
                });
                $(document).on("click", ".btnActualizarInstitucionEducativa", function (e) {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        console.log("formulario a enviar", frm);
                        
                        btnActualizarInstitucionEducativa(frm,tr);
                    })
                    
                $(document).on("click", ".btnEliminarInstitucion", function (e) {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    //console.log("Formulario a eliminar", frm);
                    var val = validarIngresoInstituciones(frm);
                    btnEliminarInstitucion(frm, tr);
                })
                $(document).on("click", ".btnAgregarInstitucion", function (e) {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    //console.log("formulario a agregar",frm);
                    var val             = validarIngresoInstituciones(frm);
                    var targetSeccion   = $(".tablaInstitucionEducativa .trAgregar");
                    if (val.estado) {
                        targetSeccion.find(".divResultado").removeClass("visibilitiHidden");
                        targetSeccion.find(".divResultado").addClass("hidden");
                        btnAgregarInstitucion(frm, function (data) {
                            if (data.estado) {
                                tr.find(".txtInstitucionEducativa").val("");
                            }
                        });
                    } else {
                        var errores;
                        targetSeccion.find(".divResultado").addClass("visibilitiHidden");
                        targetSeccion.find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = targetSeccion.find("." + i).parents("th").find(".divResultado")
                            //console.log(i, ": " + val);
                            if (val.length > 0) {
                                //console.log("entro");
                                divResultado.removeClass("visibilitiHidden");
                                $.each(val, function (i, val) {
                                    errores += getSpanMessageError(val);
                                })
                                //console.log("errores", errores);
                                divResultado.empty().append(errores);
                            }
                        })
                    }
                    
                })
            // carreras 
                // edicion 
                    $(document).on("click", ".btnEditarCarrera", function (e) {
                        var tr = $(this).parents("tr");
                        var datosSet = {};
                        datosSet.carrera        = $.trim(tr.find(".tdCarrera").text());
                        datosSet.idInstitucion  = tr.find(".txtHdIdInstitucion").val();
                        datosSet.idNivelCarrera = tr.find(".txtHdIdNivelCarrera").val();
                        datosSet.idArea         = tr.find(".txtHdIdArea").val();
                        // set
                        var frm = {};
                        actualizarCatalogo(RAIZ + "/FormacionPersonas/getEditCarreras", frm, function (data) {
                            console.log("data", data);
                            if (data.estado) {
                                var cbInstuciones = '', cbNivelesTitulos = '',cbAreaCarrera;
                                if (data.nivelesTitulos != null && data.nivelesTitulos.length > 0) {
                                    $.each(data.nivelesTitulos, function (i,nivelTitulo) {
                                        cbNivelesTitulos += getCbNivelTitulo(nivelTitulo);
                                    })
                                }
                                if (data.instituciones != null && data.instituciones.length > 0) {
                                    $.each(data.instituciones, function (i, institucion) {
                                        cbInstuciones += getCbInstituciones(institucion);
                                    })
                                }
                                if (data.areasCarreras != null && data.areasCarreras.length > 0) {
                                    $.each(data.areasCarreras, function (i, areaCarrera) {
                                        cbAreaCarrera += getCbAreaCarrera(areaCarrera);
                                    })
                                }
                                
                                var selectCarrera = tr.find(".cbNivelCarrera"); var selectInstitucion = tr.find(".cbInsticionesParaCarrera");
                                var selectAreaCarrera = tr.find(".cbAreaCarreras");
                                selectCarrera.empty().append(cbNivelesTitulos);
                                selectInstitucion.empty().append(cbInstuciones);
                                selectAreaCarrera.empty().append(cbAreaCarrera);
                                
                                selectAreaCarrera.val(datosSet.idArea);
                                resetChosenWithSelectedVal(selectInstitucion, datosSet.idInstitucion)
                                resetChosenWithSelectedVal(selectCarrera, datosSet.idNivelCarrera)
                                
                            } else {
                                // cargar error de editar
                            }
                        })
                        tr.find(".txtNombreCarrera").val(datosSet.carrera);

                        controlesEdit(true, tr);
                    })
                    $(document).on("click", ".btnActualizarCarrera", function (e) {
                        console.log("D: ");
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        console.log(frm);
                        btnActualizarCarrera(frm, tr);
                    })
                    
                $(document).on("click", ".btnEliminarCarrera", function (e) {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log(frm);
                    btnEliminarCarrera(frm, tr);
                })
                $(document).on("click", ".btnAgregarCarreraIndividual", function (e) {
                    var frm = serializeSection($(this).parents("tr"));
                    console.log("formulario frm", frm);
                    var val = validarIngresoCarreraIndividual(frm);
                    if (val.estado) {
                        btnAgregarCarreraIndividual(frm);
                    } else {
                        console.log("val es", val);
                        var errores;
                        var targetSeccion = $(".tablaCarrera .trAgregar");
                        targetSeccion.find(".divResultado").addClass("visibilitiHidden");
                        targetSeccion.find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = targetSeccion.find("." + i).parents("th").find(".divResultado")
                            //console.log(i, ": " + val);
                            if (val.length > 0) {
                                //console.log("entro");
                                divResultado.removeClass("visibilitiHidden");
                                $.each(val, function (i, val) {
                                    errores += getSpanMessageError(val);
                                })
                                //console.log("errores", errores);
                                divResultado.empty().append(errores);
                            }
                        })
                    }
                    
                })
            // formacion persona
                // edicion
                    $(document).on("click", ".btnActualizarTituloPersona", function () {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        console.log("formulario a editar", frm);
                        btnActualizarTituloPersona(frm,tr);
                    })
                    $(document).on("click", ".btnEditarTitulos", function () {
                        var tr = $(this).parents("tr");
                        var datosSet = {};
                        datosSet.yearInicio         = $.trim(tr.find(".tdYearInicio").text());
                        datosSet.yearFin            = $.trim(tr.find(".tdYearFin").text());
                        datosSet.observaciones      = $.trim(tr.find(".tdObservaciones").text());
                        datosSet.carrera            = $.trim(tr.find(".tdCarrera").text());
                        datosSet.idEstadoCarrera    = $.trim(tr.find(".txtHdIdEstadoCarrera").val());
                        datosSet.institucion        = $.trim(tr.find(".tdInstitucion").text());
                        datosSet.idNivel            = tr.find(".txtHdIdNivel").val();
                        datosSet.idArea             = tr.find(".txtHdIdArea").val();
                        datosSet.idPais             = tr.find(".txtHdIdPais").val();

                        // cargando selects 
                            var frm = {};
                            actualizarCatalogo(RAIZ + "/FormacionPersonas/getEditTitulos", frm, function (data) {
                                console.log("data para edit titulo es", data);
                                var cbNiveles = ""; var cbEstadoCarrera = ""; var cbAreas = "";
                                var cb = getCbsEditTitulos(data);
                                var selectNiveles   = tr.find(".cbNivelCarrera");   var selectEstadoCarrera = tr.find(".cbEstadoCarrera");
                                var selectAreas     = tr.find(".cbAreaCarrera");    var selectPaises        = tr.find(".cbPaisInstitucionEducativa");
                                // llenando select 
                                selectEstadoCarrera.empty().append(cb.cbEstadoCarrera);
                                selectNiveles.empty().append(cb.cbNiveles);
                                selectAreas.empty().append(cb.cbAreasCarreras);
                                selectPaises.empty().append(cb.cbPaises);
                                console.log("datos set para editar", datosSet);
                                // reset chosen
                                resetChosenWithSelectedVal(selectPaises, datosSet.idPais);
                                selectEstadoCarrera.val(datosSet.idEstadoCarrera)
                                // reset normal 
                                tr.find(".cbNivelCarrera").val(datosSet.idNivel);
                                tr.find(".cbAreaCarrera").val(datosSet.idArea);
                                
                            })
                        // set 
                            console.log("datos set", datosSet);
                            tr.find(".cbEstadoCarrera").val(datosSet.idEstadoCarrera);
                            tr.find(".txtYearInicio").val(datosSet.yearInicio);
                            tr.find(".txtYearFin").val(datosSet.yearFin);
                            tr.find(".txtAreaObservaciones").val(datosSet.observaciones);
                            tr.find(".txtCarrera").val(datosSet.carrera);
                            tr.find(".txtInstitucionEducativa").val(datosSet.institucion);
                        controlesEdit(true, tr);
                    })
                $(document).on("click", ".btnAgregarCarrera", function () {
                    var frm = serializeSection($(this).parents("tr"));
                    frm.idPersona = $(".txtHdIdPersona").val();
                    console.log("formulario", frm);
                    btnAgregarCarrera(frm);
                })
                $(document).on("click", ".btnEliminarTitulo", function () {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log("formulario", frm);
                    btnEliminarTitulo(frm,tr);
                })
                
            
})