$(document).ready(function () {
    // eventos 
        // keypress
            $(document).on("keypress", ".txtEnterAgregar", function (e) {
                var charCode = e.which;
                var tr = $(this).parents("tr");
                switch (charCode) {
                    case 13:
                        {
                            tr.find(".btnAgregar").click();
                            break;
                        }
                }
            })
        // clicks 
                $(document).on("click", ".btnCancelar", function () {
                    var x = confirm("¿Esta seguro de cancelar edicion?");
                    if (x) {
                        trMedio = $(this).parents("tr");
                        controlesEdit(false, trMedio);
                    }
                })
                $(document).on("click", ".btnActualizar", function () {
                    var seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    console.log("Formulario a actualizar",frm);
                    var val = validarEdit(frm);
                    console.log("val es",val);
                    if (val.estado) {
                        var regex = new RegExp("^(http[s]?:\\/\\/(www\\.)?|ftp:\\/\\/(www\\.)?|www\\.){1}([0-9A-Za-z-\\.@:%_\+~#=]+)+((\\.[a-zA-Z]{2,3})+)(/(.)*)?(\\?(.)*)?");
                        if (regex.test(frm.txtEnlace)) {
                            regex = new RegExp("^http://");
                            if (!regex.test(frm.txtEnlace)) {
                                frm.txtEnlace = "http://" + frm.txtEnlace;
                            }
                            btnActualizar(frm, seccion);
                        }
                        else {
                            printMessage($(".divMensajesGenerales"), "Favor ingresar una url valida", false)
                        }
                    }
                    else {
                        seccion.find(".divResultado").removeClass("hidden");
                        seccion.find(".divResultado").addClass("visibilitiHidden");
                        var errores;
                        $.each(val.campos, function (i, val) {
                            errores = "";
                            var divResultado = seccion.find("." + i).parents("td").find(".divResultado")
                            if (val.length > 0) {
                                divResultado.removeClass("visibilitiHidden");
                                $.each(val, function (i, val) {
                                    errores += "<span class='spanMessage1 failMessage'>" + val + "</span>";
                                })
                                divResultado.empty().append(errores);
                            }
                        })
                    }
                    
                })
                $(document).on("click", ".btnEditar", function () {
                    var trMedio = $(this).parents("tr");
                    trMedio.find(".divResultado").addClass("hidden");
                    btnEditar(trMedio);
                })
                //*************************************
            // email institucion
                $(document).on("click", ".btnAddEmailInstitucion", function () {
                    var tr  = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log("Frm es: ", frm);
                    btnAddEmailInstitucion(frm);
                })
                $(document).on("click", ".btnEliminarEmailsInstitucion", function () {
                    var tr = $(this).parents("tr");
                    var frm = serializeSection(tr);
                    console.log("Frm eliminar institucion ", frm)
                    var x = confirm("¿Desea eliminar institucion?");
                    if (x) {
                        btnEliminarEmailsInstitucion(frm,tr);
                    }
                })
            // medios  
                $(document).on("click", ".btnAgregar", function () {
                    var seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    var val;
                    var regex = new RegExp("^(http[s]?:\\/\\/(www\\.)?|ftp:\\/\\/(www\\.)?|www\\.){1}([0-9A-Za-z-\\.@:%_\+~#=]+)+((\\.[a-zA-Z]{2,3})+)(/(.)*)?(\\?(.)*)?");
                    console.log("Frm antes", frm);
                    if (regex.test(frm.txtEnlace)) {
                        regex = new RegExp("^http://");
                        if (!regex.test(frm.txtEnlace)) {
                            frm.txtEnlace = "http://" + frm.txtEnlace;
                        }
                        //seccion.find(".divResultado").empty();
                        console.log("Frm despues", frm);
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
                    var x = confirm("¿Esta seguro que desea eliminar registro?");
                    if (x) {
                        btnEliminar(frm, seccion);
                    }
                });
})