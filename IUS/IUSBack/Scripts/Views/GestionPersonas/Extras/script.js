$(document).ready(function () {
    // plugins
        $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
        $(".cbPersonas").chosen({ no_results_text: "No se a encontrado personas", width: '100%' });
        // jcrop
            var jcrop_api = null;
    // eventos
        // change 
            $(document).on("change", ".flFotoPersona", function (e) {
                // vars 
                    var boton = $(".btnEstablecer"); var targetImg = $(".imgPersona");
                    var divLoading = $(".divLoadingPhoto");
                    var cambiar = false;
                // do it 
                if ($(this).val() == "") {
                    console.log("Es vacio");
                    boton.prop("disabled", true);
                } else {
                    console.log("Es vacio NO NO NO");
                    cambiar = true;
                    divLoading.empty().append("<img class='imgLoading' src='" + IMG_GENERALES + "ajax-loader.gif" + "'>");
                    boton.prop("disabled", false);
                }
                if (cambiar) {
                    getImageFromInputFileEvent(e, function (images) {
                        divLoading.empty();
                        //$(".imgPersona").fadeIn("slow");
                        console.log("mmm", images);
                        if (images !== undefined && images != null) {
                            targetImg.attr("src", images.src);
                            targetImg.attr("style", "");
                            if (jcrop_api != null) {
                                jcrop_api.destroy();
                            }
                            jcrop_api = $.Jcrop('.imgPersona', {
                                onSelect: storeCoords,
                                onChange: storeCoords,
                                aspectRatio: 1
                            });
                            inicialFoto();
                        }
                    });
                } else {
                    if (jcrop_api != null) {
                        jcrop_api.destroy();
                    }
                    targetImg.attr("src", IMG_GENERALES + "noimage.png");
                    targetImg.attr("style", "");
                }
                
            })
            $(document).on("change", ".cbPersonas", function (e) {
                location.href = RAIZ + "GestionPersonas/Extras/" + $(this).val();
            })
        // submit 
            $(document).on("submit", ".frmImagenPersona", function (e) {
                console.log($(".imgPersona").width(), $(".imgPersona").height());
                var frm = getFrmFoto();
                console.log(frm);
                var files       = $("#flMiniatura")[0].files;
                var data        = getObjFormData(files, frm);
                e.preventDefault();
                var imagen      = files[0];
                frmImagenPersona(data, $(this).attr("action"), imagen,frm,jcrop_api);
            })
        // doble click
            $(document).on("dblclick", ".hNombrePersona", function () {
                controlesEdit(true, $(this).parents(".divTituloNombre"));
            })
        // click
            $(document).on("click", ".icoVolverAnombre", function () {
                controlesEdit(false, $(this).parents(".divTituloNombre"));
            })
            // email
                // editar
                    $(document).on("click", ".btnCancelarUpdateEmail", function () {
                        var tr = $(this).parents("tr");
                        limpiarVal(tr);
                        controlesEdit(false, tr);
                    })
                    $(document).on("click", ".btnEditarEmail", function () {
                        var tr          = $(this).parents("tr");
                        var email       = $.trim(tr.find(".tdEmail").text());
                        var etiqueta    = $.trim(tr.find(".tdEtiqueta").text());
                        console.log(email, etiqueta);
                        tr.find(".txtEtiquetaEmail").val(etiqueta);
                        tr.find(".txtEmail").val(email);
                        controlesEdit(true, tr);
                    })
                    $(document).on("click", ".btnActualizarEmail", function () {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        console.log("Para actualizar", frm);
                        var val = validarInsertEmail(frm);
                        if (val.estado) {
                            btnActualizarEmail(frm, tr);
                        }
                        else {
                            var errores;
                            tr.find(".divResultado").addClass("visibilitiHidden");
                            tr.find(".divResultado").removeClass("hidden");
                            $.each(val.campos, function (i, val) {
                                errores = "";
                                var divResultado = tr.find("." + i).parents("td").find(".divResultado")
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
                $(document).on("click", ".btnEliminarEmail", function () {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    //console.log(frm);
                    btnEliminarEmail(frm,tr);
                })
                $(document).on("click", ".btnGuardarEmail", function () {
                    var tr          = $(this).parents("tr");
                    var frm         = serializeSection(tr);
                    frm.idPersona   = $(".txtHdIdPersona").val();
                    var val         = validarInsertEmail(frm);
                    if (val.estado)
                    {
                        $(".tablaCorreos thead").find(".divResultado").addClass("hidden");
                        $(".tablaCorreos thead").find(".divResultado").removeClass("visibilitiHidden");
                        btnGuardarEmail(frm);
                    } else {
                        var errores;
                        $(".tablaCorreos thead").find(".divResultado").addClass("visibilitiHidden");
                        $(".tablaCorreos thead").find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = $(".tablaCorreos thead").find("." + i).parents("th").find(".divResultado")
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
            // telefono  
                // editar tel
                    // edit | cancelar
                        $(document).on("click", ".btnCancelarUpdateTel", function () {
                            var tr = $(this).parents("tr");
                            limpiarVal(tr);
                            controlesEdit(false, tr);
                        })
                        //******************
                        $(document).on("click", ".btnEditarTel", function () {
                            var tr = $(this).parents("tr");
                            var telefono = $.trim(tr.find(".tdTelefono").text());
                            var etiqueta = $.trim(tr.find(".tdEtiqueta").text());
                            tr.find(".txtTelefono").val(telefono);
                            tr.find(".txtEtiquetaTel").val(etiqueta);
                            var frm = {}; var ops = "";
                            var selectedPais = tr.find(".txtHdIdPais").val();
                            console.log("selected pais", selectedPais);
                            tr.find(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
                            console.log("intento poner chosen");
                            actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_getPaises", frm, function (data) {
                                console.log("data pais", data)
                                var selected = false;
                                if (data.estado && data.paises.length > 0) {
                                    console.log("Entro aqui D: ");
                                    $.each(data.paises, function (i, pais) {
                                        ops += getCbPaises(pais, selected);
                                    })
                                    var cb = tr.find(".cbPais");
                                    cb.empty().append(ops);
                                    resetChosenWithSelectedVal(cb, selectedPais);
                                }
                            })
                            // cargar paises 
                            controlesEdit(true, tr);
                        })
                    $(document).on("click", ".btnActualizarTel", function () {
                        var tr = $(this).parents("tr");
                        var frm = serializeSection(tr);
                        var val = validarInsertTelefono(frm);
                        //console.log(frm);
                        if (val.estado) {
                            btnActualizarTel(frm, tr);
                        } else {
                            // errores 
                            var errores;
                            tr.find(".divResultado").addClass("visibilitiHidden");
                            tr.find(".divResultado").removeClass("hidden");
                            $.each(val.campos, function (i, val) {
                                errores = "";
                                var divResultado = tr.find("." + i).parents("td").find(".divResultado")
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
                $(document).on("click", ".btnEliminarTel", function () {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log(frm);
                    btnEliminarTel(frm,tr);
                })
                $(document).on("click", ".btnAgregarTel", function () {
                    var frm         = serializeSection($(this).parents("tr"));
                    frm.idPersona   = $(".txtHdIdPersona").val();
                    var val = validarInsertTelefono(frm);
                    var theadTabla = $(".tablaNumerosTelefonicos thead");
                    if (val.estado) {
                        theadTabla.find(".divResultado").addClass("hidden");
                        theadTabla.find(".divResultado").removeClass("visibilitiHidden");
                        btnAgregarTel(frm);
                    } else {
                        // errores 
                        var errores; 
                        theadTabla.find(".divResultado").addClass("visibilitiHidden");
                        theadTabla.find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = theadTabla.find("." + i).parents("th").find(".divResultado")
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
            // informacion
                $(document).on("click", ".btnGuardarInformacionBasica", function () {
                    var frm             = serializeSection($(this).parents(".divFrmInformacionExtra"));
                    frm.txtHdIdPersona  = $(".txtHdIdPersona").val();
                    console.log("Formulario a enviar es", frm);
                    var val = validarInsertExtra(frm);
                    var theadTabla = $(".rowControles");
                    if (val.estado) {
                        btnGuardarInformacionBasica(frm);
                    } else {
                        console.log(val);
                        // errores 
                        var errores;
                        theadTabla.find(".divResultado").addClass("visibilitiHidden");
                        theadTabla.find(".divResultado").removeClass("hidden");
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = theadTabla.find("." + i).parents(".divControl").find(".divResultado")
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