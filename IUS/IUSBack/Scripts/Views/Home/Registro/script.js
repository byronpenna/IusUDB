$(document).ready(function () {
    // plugins
        // datepicker
            $(".dtpFechaNac").datepicker({
                dateFormat: "dd/mm/yy"
            });
    // eventos
        // click
            $(document).on("submit", ".frmRegistrar", function (e) {
                e.preventDefault();
                var frm = serializeToJson($(this).serializeArray());
                console.log(frm);
                var val = validarIngreso(frm);
                console.log(val);
                $(".frmRegistrar").find(".divResultado").addClass("hidden");
                $(".spanResultado").addClass("hidden");
                if (val.estado) {
                    frmRegistrar(frm);
                } else {
                    var errores;
                    $(".frmRegistrar").find(".divResultado").removeClass("hidden");
                    $(".frmRegistrar").find(".divResultado").addClass("visibilitiHidden");
                    $.each(val.campos, function (i, val) {
                        errores = "";
                        console.log("I es ", i);
                        var divResultado = $(".frmRegistrar").find("." + i).parents(".vv").find(".divResultado");
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
});