$(document).ready(function () {
    // eventos
        // click
            /*$(document).on("click", ".menuLateral li", function () {
                $(this).addClass(".activeInstitucion");
                $(this).find(".imgMap").attr("src", $(this).find(".txtHdRoja").val());
            })*/
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

                window.history.pushState({}, "Titulo", "/Instituciones/index/" + frm.id);
                buscarContinente(frm);
            })
})