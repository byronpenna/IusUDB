$(document).ready(function () {
    // eventos
        // click 
            $(document).on("click", ".btnActualizar", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                btnActualizar(frm,seccion);
            })
            $(document).on("click", ".btnEditarTel", function () {
                trTel = $(this).parents("tr");
                btnEditarTel(trTel);
            })
            $(document).on("click", ".btnAgregarTel", function () {
                seccion = $(this).parents("tr");
                frm = serializeSection(seccion);
                console.log("Frm a enviar", frm);
                var val = validarAgregarTel(frm);
                if (val.estado) {
                    //btnAgregarTel(frm, seccion);
                } else {
                    var errores;
                    console.log("entro a los arrores");
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        var divResultado = seccion.find("." + i).parents("th").find(".divResultado")
                        console.log(i, ": " + val);
                        if (val.length > 0) {
                            divResultado.removeClass("hidden");
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