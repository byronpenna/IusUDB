$(document).ready(function () {
    // plugins
        $(".cbPais").chosen({ no_results_text: "Pais no encontrado", width: '100%' });
    // eventos
        // submit 
            $(document).on("submit", ".frmImagenPersona", function (e) {
                var frm         = new Object();
                frm.idPersona   = $(".txtHdIdPersona").val();
                var files       = $("#flMiniatura")[0].files;
                var data        = getObjFormData(files, frm);
                e.preventDefault();
                //var imagen = $("#flMiniatura")[0].files[0];
                var imagen      = files[0]
                //console.log(imagen);
                frmImagenPersona(data, $(this).attr("action"), imagen);
            })
        // click
            // email
                // editar
                    $(document).on("click", ".btnCancelarUpdateEmail", function () {
                        var tr = $(this).parents("tr");
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
                        btnActualizarEmail(frm,tr);
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
                    //console.log(frm);
                    btnGuardarEmail(frm);
                })
            // telefono  
                // editar tel
                    // edit | cancelar
                        $(document).on("click", ".btnCancelarUpdateTel", function () {
                            var tr = $(this).parents("tr");
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
                        //console.log(frm);
                        btnActualizarTel(frm,tr);
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
                    console.log("Formulario a enviar es", frm);
                    //var val = 
                    btnAgregarTel(frm);
                })
            // informacion
                $(document).on("click", ".btnGuardarInformacionBasica", function () {
                    var frm = serializeSection($(this).parents(".divFrmInformacionExtra"));
                    console.log("Formulario a enviar es", frm);
                    frm.txtHdIdPersona = $(".txtHdIdPersona").val();
                    btnGuardarInformacionBasica(frm);
                })
})