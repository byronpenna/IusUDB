$(document).ready(function () {
    
    // eventos
        
        // click
            /*$(document).on("click", ".menuLateral li", function () {
                $(this).addClass(".activeInstitucion");
                $(this).find(".imgMap").attr("src", $(this).find(".txtHdRoja").val());
            })*/
            $(document).on("click", ".paginador", function () {
                var nPagina = $(this).attr("id");
                $(".activePaginador").removeClass("activePaginador");
                $(this).addClass("activePaginador");
                nPagina -= 1;
                var instituciones = instArr[nPagina];
                var tr = "";
                var target = $(".tablaInstitucion").find("tbody");
                if (instituciones !== undefined && instituciones != null && instituciones.length > 0) {
                    $.each(instituciones, function (i, institucion) {
                        tr += getTrInstitucion(institucion);
                    });
                } else {
                    tr = getTrInstitucionNull();
                }

                target.empty().append(tr);
            })
            $(document).on("click", ".menuLateral li", function () {
                var frm = {
                    id: $(this).attr("id")
                }

                $(".divInfoInicial").addClass("hidden");
                $(".divInfoInstituciones").removeClass("hidden");

                $(".activeInstitucion").find(".imgMap").attr("src", $(".activeInstitucion").find(".txtHdNormal").val());
                $(".activeInstitucion").removeClass("activeInstitucion");

                $(this).find(".imgMap").attr("src", $(this).find(".txtHdRoja").val());
                $(this).addClass("activeInstitucion");

                window.history.pushState({}, "Titulo", RAIZ+"Instituciones/index/" + frm.id);
                buscarContinente(frm);
            })
    // iniciales
        iniciales();
})