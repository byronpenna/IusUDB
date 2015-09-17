﻿$(document).ready(function () {
    // plugins 
        $(".cbChosenPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
    // eventos 
        // click 
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
                    console.log("Formulario a eliminar", frm);
                    btnEliminarInstitucion(frm, tr);
                })
                $(document).on("click", ".btnAgregarInstitucion", function (e) {
                    var frm = serializeSection($(this).parents("tr"));
                    console.log("formulario a agregar",frm);
                    btnAgregarInstitucion(frm);
                })
            // carreras 
                
                $(document).on("click", ".btnEliminarCarrera", function (e) {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log(frm);
                    btnEliminarCarrera(frm, tr);
                })
                $(document).on("click", ".btnAgregarCarreraIndividual", function (e) {
                var frm = serializeSection($(this).parents("tr"));
                console.log("formulario frm", frm);
                btnAgregarCarreraIndividual(frm);
            })
            // formacion persona
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