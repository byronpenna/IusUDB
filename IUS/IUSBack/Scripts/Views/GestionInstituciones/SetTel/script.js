$(document).ready(function () {
    // eventos
        // keypress
            $(document).on("keypress", ".txtFrmAgregar", function (e) {
                var charCode = e.which;
                switch (charCode) {
                    case 13: {
                        $(this).parents("tr").find(".btnAgregarTel").click();
                        break;
                    }
                }
            })
            $(document).on("keypress", ".txtFrmEditar", function (e) {
                var charCode = e.which;
                console.log("Entro");
                switch (charCode) {
                    case 13: {
                        $(this).parents("tr").find(".btnActualizar").click();
                    }
                }
            })
            
        // click 
            $(document).on("click", ".btnCancelar", function () {
                var tr = $(this).parents("tr");
                controlesEdit(false, tr);
            })
            $(document).on("click", ".btnActualizar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                seccion.find(".divResultado").addClass("hidden");
                console.log(frm);
                var val = validarEditarTel(frm);
                if (val.estado) {
                    btnActualizar(frm, seccion);
                } else {
                    var errores; var divResultado;
                    seccion.find(".divResultado").removeClass("hidden");
                    seccion.find(".divResultado").addClass("visibilitiHidden");
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        divResultado = seccion.find("." + i).parents("td").find(".divResultado")
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
            $(document).on("click", ".btnEditarTel", function () {
                var trTel = $(this).parents("tr");
                trTel.find(".divResultado").addClass("hidden");

                btnEditarTel(trTel);
            })
            $(document).on("click", ".btnAgregarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                seccion.find(".divResultado").addClass("hidden");
                var val = validarAgregarTel(frm);
                if (val.estado) {
                    btnAgregarTel(frm, seccion);
                } else {
                    var errores; var divResultado;
                    seccion.find(".divResultado").removeClass("hidden");
                    seccion.find(".divResultado").addClass("visibilitiHidden");
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        divResultado = seccion.find("." + i).parents("th").find(".divResultado")
                        if (val.length > 0) {
                            divResultado.removeClass("visibilitiHidden");
                            $.each(val, function (i, val) {
                                errores += getSpanMessageError(val);
                            })
                            divResultado.empty().append(errores);
                        }
                    })
                }
            });
            $(document).on("click", ".btnEliminarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnEliminarTel(frm, seccion);
            });
})