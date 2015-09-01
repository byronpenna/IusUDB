$(document).ready(function () {
    // plugins 
        // data table 
            var dataTableInstituciones = $(".tbInstituciones").DataTable({
                "bSort": false
            });
        // chosen
            $(".cbPais").chosen({ no_results_text: "Ese pais no existe", width: '100%' });
    // eventos 
        // down
            $(document).on("keydown", ".txtNombreInstitucion", function (e) {
                var charcode = e.which;
                console.log(charcode);
                switch (charcode) {
                    case 13: {
                        $(".btnAddInstitucion").click();
                        break;
                    }
                }
            })
        // click    
            
            $(document).on("click", ".btnCancelar", function () {
                trInstitucion = $(this).parents("tr");
                console.log("entro");
                controlesEdit(false, trInstitucion);
            })
            // **************************
            $(document).on("keydown", ".txtEnterEdit", function (e) {
                var charCode = e.which;
                var tr = $(this).parents("tr");
                switch (charCode) {
                    case 13: {
                        tr.find(".btnActualizarInstitucion").click();
                    }
                }
            })
            $(document).on("click", ".btnActualizarInstitucion", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                var val = validacionEdit(frm);
                console.log("Formulario D: D: ", frm);
                console.log("val", val);
                if (val.estado) {
                    btnActualizarInstitucion(frm, seccion);
                }
                else {
                    var errores;
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        var divResultado = seccion.find("." + i).parents("td").find(".divResultado")
                        if (val.length > 0) {
                            divResultado.removeClass("hidden");
                            $.each(val, function (i, val) {
                                errores += getSpanMessageError(val);
                                console.log(val);
                            })
                            divResultado.empty().append(errores);
                        }
                    });
                }
            })
            $(document).on("click", ".btnEditar", function () {
                trInstitucion = $(this).parents("tr");
                //table = $(".tbInstituciones").DataTable();
                trInstitucion.find(".divResultado").empty();
                trInstitucion.find(".divResultado").addClass("hidden");
                btnEditar(trInstitucion);
                
            })
            $(document).on("click", ".btnDeleteInstitucion", function () {
                //var x = confirm("¿?")
                seccion = $(this).parents("tr");
                frm = { idInstitucion: seccion.find(".txtHdIdInstitucion").val() }
                console.log(frm);
                btnDeleteInstitucion(frm,seccion);
            });
            //*************************************
            $(document).on("click", ".btnAddInstitucion", function () {
                seccion = $(".trFrmInstituciones");
                var frm = serializeSection(seccion);
                var val = validacionIngreso(frm);
                if (val.estado) {
                    btnAddInstitucion(frm, seccion, function () {
                        $(".tbInstituciones thead").find(".divResultado").empty();
                    });
                } else {
                    var errores;
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        var divResultado = $(".tbInstituciones thead").find("." + i).parents("td").find(".divResultado")
                        if (val.length > 0) {
                            console.log("val en la seccion de errores", val);
                            divResultado.removeClass("hidden");
                            $.each(val, function (i, val) {
                                errores += getSpanMessageError(val);
                                console.log(val);
                            })
                            divResultado.empty().append(errores);
                        }
                    });
                }
                //console.log("El formulario es", frm);
                //
            });
});