$(document).ready(function () {
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
            $(document).on("click", ".btnAgregarInstitucion", function (e) {
                var frm = serializeSection($(this).parents("tr"));
                console.log("formulario a agregar",frm);
                btnAgregarInstitucion(frm);
            })
            $(document).on("click", ".btnAgregarCarreraIndividual", function (e) {
                var frm = serializeSection($(this).parents("tr"));
                console.log("formulario frm", frm);
                btnAgregarCarreraIndividual(frm);
            })
            $(document).on("click", ".btnAgregarCarrera", function () {
                var frm = serializeSection($(this).parents("tr"));
                frm.idPersona = $(".txtHdIdPersona").val();
                console.log("formulario", frm);
                btnAgregarCarrera(frm);
            })
            
            
})