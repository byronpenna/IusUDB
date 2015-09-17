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
            // instituciones
            
                $(document).on("click", ".btnEditarInstitucion", function (e) {
                    var tr = $(this).parents("tr");
                    var data = new Object();
                    data.nombreInstitucion = $.trim(tr.find(".tdNombreInstitucion").text());
                    data.idPais = tr.find(".txtHdIdPaisInstitucion").val();
                    controlesEdit(true, tr);
                });
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