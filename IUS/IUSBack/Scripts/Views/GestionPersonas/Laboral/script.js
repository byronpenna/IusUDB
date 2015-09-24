$(document).ready(function () {
    // eventos 
        // click 
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
})