$(document).ready(function () {
    // eventos 
        // clicks 
           
                $(document).on("click", ".btnCancelar", function () {
                    var x = confirm("¿Esta seguro de cancelar edicion?");
                    if (x) {
                        trMedio = $(this).parents("tr");
                        controlesEdit(false, trMedio);
                    }
                })
                $(document).on("click", ".btnActualizar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    console.log(frm);
                    btnActualizar(frm, seccion);
                })
                $(document).on("click", ".btnEditar", function () {
                    trMedio = $(this).parents("tr");
                    btnEditar(trMedio);
                })
    
                $(document).on("click", ".btnAgregar", function () {
                    var seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    var val;
                    var regex = new RegExp("^(http[s]?:\\/\\/(www\\.)?|ftp:\\/\\/(www\\.)?|www\\.){1}([0-9A-Za-z-\\.@:%_\+~#=]+)+((\\.[a-zA-Z]{2,3})+)(/(.)*)?(\\?(.)*)?");
                    if (regex.test(frm.txtEnlace)) {
                        regex = new RegExp("^http://");
                        if (!regex.test(frm.txtEnlace)) {
                            frm.txtEnlace = "http://" + frm.txtEnlace;
                        }
                        //seccion.find(".divResultado").empty();
                        seccion.find(".divResultado").addClass("hidden");
                        val = validarAgregar(frm);
                        console.log("El valor de val es", val);
                        if (val.estado) {
                            btnAgregar(frm);
                        } else {
                            seccion.find(".divResultado").removeClass("hidden");
                            seccion.find(".divResultado").addClass("visibilitiHidden");
                            var errores;
                            $.each(val.campos, function (i, val) {

                                errores = "";
                                var divResultado = seccion.find("." + i).parents("th").find(".divResultado")
                                if (val.length > 0) {
                                    divResultado.removeClass("visibilitiHidden");
                                    $.each(val, function (i, val) {
                                        errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                                    })
                                    divResultado.empty().append(errores);
                                    console.log("append D: ");
                                }
                            })
                        }
                        
                    } else {
                        //alert("Favor ingresar una url valida");
                        printMessage($(".divMensajesGenerales"), "Favor ingresar una url valida", false)
                    }
                })
                $(document).on("click", ".btnEliminar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    btnEliminar(frm,seccion);
                });
})