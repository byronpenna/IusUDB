$(document).ready(function () {
    // plugins 
        $(".tablaLaboral").DataTable({
            "bDestroy": true,
            "bSort": false
        });
        $(".cbPersonas").chosen({ no_results_text: "No se a encontrado personas", width: '100%' });
        $(".cbEmpresa").chosen({ no_results_text: "No se encontro esa empresa", width: '100%' });
        $(".cbCargo").chosen({ no_results_text: "No se encontro cargo", width: '100%' });
    // eventos 
        // doble click  
            $(document).on("dblclick", ".titlePersona", function (e) {
                controlesEdit(true, $(this).parents(".divTituloNombre"));
            })
        // change 
            $(document).on("change", ".cbPersonas", function (e) {
                location.href = RAIZ + "GestionLaboral/index/" + $(this).val();
            })
            $(document).on("change", ".flCurriculum", function (e) {
                var btnTarget = $(".btnSubir");
                if ($(this).val() != "") {
                    btnTarget.prop("disabled", false);
                } else {
                    btnTarget.prop("disabled", true);
                }
            })
        // submit 
            $(document).on("submit", ".frmCurriculumn", function (e) {
                var frm     = serializeSection($(this));
                var files   = $(".flCurriculum")[0].files;
                var data = getObjFormData(files, frm);
                e.preventDefault();
                console.log("El formulario es", frm);
                frmCurriculumn(data, $(this).attr("action"));
            })
        // click 
            $(document).on("click", ".icoVolverAnombre", function () {
                controlesEdit(false, $(this).parents(".divTituloNombre"));
            })
            // cargos 
                $(document).on("click", ".btnActividad", function () {
                    var tr = $(this).parents("tr");
                    if (!tr.next().hasClass("trTable")) {
                        // enseña los roles
                        getTableActividades(tr);
                        $(this).empty().append("Ocultar actividades");
                    } else {
                        // oculta los roles
                        if (tr.next().hasClass("trTable")) {
                            tr.next().remove();
                        }
                        $(this).empty().append("Actividades realizadas");
                    }
                })
            // operaciones basicas
                // actividades 
                    // editar 
                        $(document).on("click", ".btnEditarActividad", function () {
                            var tr = $(this).parents(".trEliminarActividad");
                            var datosSet = {
                               actividad: $.trim(tr.find(".tdActividad").text())
                            }
                            tr.find(".txtActividad").val(datosSet.actividad);
                            controlesEdit(true, tr);
                        })
                        $(document).on("click", ".btnActualizarActividadEmpresa", function () {
                            var tr = $(this).parents(".trEliminarActividad");
                            var frm = serializeSection(tr);
                            var val = validarInsertActividad(frm);
                            if (val.estado) {
                                limpiarVal(tr);
                                btnActualizarActividadEmpresa(frm, tr);
                            } else {
                                //############
                                var errores;
                                tr.find(".divResultado").addClass("visibilitiHidden");
                                tr.find(".divResultado").removeClass("hidden");
                                $.each(val.campos, function (i, val) {
                                    errores = "";
                                    var divResultado = tr.find("." + i).parents(".divControl").find(".divResultado")
                                    if (val.length > 0) {
                                        divResultado.removeClass("visibilitiHidden");
                                        $.each(val, function (i, val) {
                                            errores += getSpanMessageError(val);
                                        })
                                        divResultado.empty().append(errores);
                                    }
                                })
                            }
                        })
                    $(document).on("click", ".btnEliminarActividad", function () {
                        var tr = $(this).parents(".trEliminarActividad");
                        var frm = serializeSection(tr);
                        btnEliminarActividad(frm, tr);
                    })
                    //##############################################
                    $(document).on("click", ".btnAgregarActividad", function () {
                        var tr                      = $(this).parents(".trAgregar");
                        var frm                     = serializeSection(tr);
                        frm.txtHdIdLaboralPersona   = $(this).parents(".trTable").find(".txtHdIdLaboralPersona").val();
                        var val = validarInsertActividad(frm);
                        if (val.estado) {
                            btnAgregarActividad(frm, tr);
                        } else {
                            //############
                            var errores;
                            tr.find(".divResultado").addClass("visibilitiHidden");
                            tr.find(".divResultado").removeClass("hidden");
                            $.each(val.campos, function (i, val) {
                                errores = "";
                                var divResultado = tr.find("." + i).parents(".divControl").find(".divResultado")
                                if (val.length > 0) {
                                    divResultado.removeClass("visibilitiHidden");
                                    $.each(val, function (i, val) {
                                        errores += getSpanMessageError(val);
                                    })
                                    divResultado.empty().append(errores);
                                }
                            })
                        }
                        
                    })
                // laboral persona 
                    $(document).on("click", ".btnAgregarLaboralPersona", function () {
                        console.log("Click");
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        frm.idPersona = $(".txtHdIdPersona").val();
                        var val = validarInsertLaboral(frm);
                        console.log("El valor de val",val);
                        if (val.estado) {
                            //console.log("El formulario a enviar es: ", frm);
                            btnAgregarLaboralPersona(frm,tr);
                        } else {
                            //############
                            var errores;
                            tr.find(".divResultado").addClass("visibilitiHidden");
                            tr.find(".divResultado").removeClass("hidden");
                            $.each(val.campos, function (i, val) {
                                errores = "";
                                var divResultado = tr.find("." + i).parents(".divControl").find(".divResultado")
                                if (val.length > 0) {
                                    divResultado.removeClass("visibilitiHidden");
                                    $.each(val, function (i, val) {
                                        errores += getSpanMessageError(val);
                                    })
                                    divResultado.empty().append(errores);
                                }
                            })
                        }
                        
                    })
                    $(document).on("click", ".btnEliminarLaboralPersona", function () {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        btnEliminarLaboralPersona(frm,tr);
                    })
            // editar
                $(document).on("click", ".btnEditarLaboralPersona", function () {
                    // variables
                        var tr = $(this).parents("tr");
                        var datosSet = getObjetoSetEditLaboral(tr);
                    // set 
                        var frm = {  }
                        actualizarCatalogo(RAIZ + "/GestionLaboral/sp_rrhh_getEditModeLaboralPersona", frm, function (data) {
                            var cbCargos = "", cbInstituciones = "";
                            console.log("La data recibida", data);
                            if (data.estado) {
                                if (data.cargos !== undefined && data.cargos !== null && data.cargos.length > 0) {
                                    $.each(data.cargos, function (i, cargo) {
                                        cbCargos += getCbCargos(cargo);
                                    })
                                }
                                if (data.instituciones !== undefined && data.instituciones !== null && data.instituciones.length > 0) {
                                    $.each(data.instituciones, function (i, institucion) {
                                        cbInstituciones += getCbInstituciones(institucion);
                                    })
                                }
                            } else {
                                
                            }
                            var selectCargo = tr.find(".cbCargo"), selectInstitucion = tr.find(".cbInstitucion");
                            selectCargo.empty().append(cbCargos);
                            selectInstitucion.empty().append(cbInstituciones);
                            // seleccionando
                            resetChosenWithSelectedVal(selectCargo, datosSet.idCargoEmpresa)
                            resetChosenWithSelectedVal(selectInstitucion, datosSet.idInstitucion)
                        })
                        tr.find(".txtInicio").val(datosSet.fechaInicio);
                        tr.find(".txtFin").val(datosSet.fechaFin);
                        tr.find(".txtAreaObservacion").val(datosSet.observaciones);
                    controlesEdit(true, tr);
                })
                $(document).on("click", ".btnCancelarUni", function (e) {
                    var tr = $(this).parents("tr");
                    limpiarVal(tr);
                    controlesEdit(false, tr);
                })
                $(document).on("click", ".btnCancelarAct", function (e) {
                    var tr = $(this).parents(".trEliminarActividad");
                    limpiarVal(tr);
                    controlesEdit(false, tr);
                })
                $(document).on("click", ".btnActualizarLaboralPersona", function (e) {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    var val = validarInsertLaboral(frm);
                    if (val.estado) {
                        tr.find(".divResultado").removeClass("visibilitiHidden");
                        tr.find(".divResultado").addClass("hidden");
                        btnActualizarLaboralPersona(frm, tr);
                    } else {
                        var errores;
                        tr.find(".divResultado").addClass("visibilitiHidden");
                        tr.find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = tr.find("." + i).parents(".divControl").find(".divResultado")
                            if (val.length > 0) {
                                divResultado.removeClass("visibilitiHidden");
                                $.each(val, function (i, val) {
                                    errores += getSpanMessageError(val);
                                })
                                divResultado.empty().append(errores);
                            }
                        })
                    }
                })
                
})