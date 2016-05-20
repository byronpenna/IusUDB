$(document).ready(function () {
    // eventos 
    $(document).on("click", ".menuLateral li", function () {
        //$(this).addClass("activeRecurso");

        $(".activeRecurso").find(".imgLateral").attr("src", $(".activeRecurso").find(".txtHdNormal").val());
        $(".activeRecurso").removeClass("activeRecurso");
        
        $(this).find(".imgLateral").attr("src", $(this).find(".txtHdRoja").val());
        $(this).addClass("activeRecurso");

        window.history.pushState({}, "Titulo", "/Repositorio/index/");
        var frm = serializeSection($(this));
        getArchivos(frm);
    })
})