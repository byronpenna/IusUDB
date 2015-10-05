$(document).ready(function () {
    // plugins 
        $(".tablaLaboral").DataTable({
            "bDestroy": true,
            "bSort": false
        });
        
    // eventos 
        // click 

            // cargos 
                $(document).on("click", ".btnActividad", function () {
                    var tr = $(this).parents("tr");
                    if (!tr.next().hasClass("trTable")) {
                        // enseña los roles
                        getTableActividades(tr);
                        $(this).empty().append("Ocultar actividades");
                    } else {
                        // oculta los roles
                        tr.parents("table").find(".trTable").remove();
                        $(this).empty().append("Ver Actividades");
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
                            console.log("formulario a enviar D: D: D: ", frm);
                            btnActualizarActividadEmpresa(frm,tr);
                        })
                    $(document).on("click", ".btnEliminarActividad", function () {
                        var tr = $(this).parents(".trEliminarActividad");
                        var frm = serializeSection(tr);
                        console.log("Eliminar", frm);
                        btnEliminarActividad(frm, tr);
                    })
                    $(document).on("click", ".btnAgregarActividad", function () {
                        var tr                      = $(this).parents(".trAgregar");
                        var frm                     = serializeSection(tr);
                        frm.txtHdIdLaboralPersona   = $(this).parents(".trTable").find(".txtHdIdLaboralPersona").val();
                        //frm.idLaboral             = $(this).parents(".trTable").find(".txtHdIdLaboralPersona").val();
                        console.log("formulario a enviar es", frm);
                        btnAgregarActividad(frm,tr);
                    })
                // laboral persona 
                    $(document).on("click", ".btnAgregarLaboralPersona", function () {
                    var frm = serializeSection($(this).parents("tr"));
                    frm.idPersona = $(".txtHdIdPersona").val();
                    console.log("formulario a enviar",frm);
                    btnAgregarLaboralPersona(frm);
                })
                    $(document).on("click", ".btnEliminarLaboralPersona", function () {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log("formulario a enviar", frm);
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
                            console.log("data devuelta para edit mode", data);
                            var cbCargos = "", cbEmpresas = "";
                            if (data.estado) {
                                if (data.cargos !== undefined && data.cargos !== null && data.cargos.length > 0) {
                                    console.log("D:");
                                    $.each(data.cargos, function (i, cargo) {
                                        cbCargos += getCbCargos(cargo);
                                    })
                                }
                                if (data.empresas !== undefined && data.empresas !== null && data.empresas.length > 0) {
                                    console.log(":/");
                                    $.each(data.empresas, function (i, empresa) {
                                        cbEmpresas += getCbEmpresas(empresa);
                                    })
                                }
                            } else {
                                
                            }
                            var selectCargo = tr.find(".cbCargo"), selectEmpresa = tr.find(".cbEmpresa");
                            selectCargo.empty().append(cbCargos);
                            selectEmpresa.empty().append(cbEmpresas);
                            // seleccionando
                            selectCargo.val(datosSet.idCargoEmpresa);
                            selectEmpresa.val(datosSet.idEmpresa);
                        })
                        tr.find(".txtInicio").val(datosSet.fechaInicio);
                        tr.find(".txtFin").val(datosSet.fechaFin);
                        tr.find(".txtAreaObservacion").val(datosSet.observaciones);

                    controlesEdit(true, tr);
                })
                $(document).on("click", ".btnCancelarUni", function (e) {
                    var tr = $(this).parents("tr");
                    controlesEdit(false, tr);
                })
                $(document).on("click", ".btnActualizarLaboralPersona", function (e) {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log("formulario a enviar", frm);
                    btnActualizarLaboralPersona(frm,tr);
                })
                
})