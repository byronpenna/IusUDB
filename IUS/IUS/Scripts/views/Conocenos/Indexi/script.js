$(document).ready(function () {
    
    // eventos 
        $(document).on("click", ".menuConocenos li", function () {
            $(".menuConocenos li").removeClass("activeConocenos");
            $(this).addClass("activeConocenos");
            var id = $(this).attr("id");
            frm = { idSeleccion: id };
            window.history.pushState({}, "Titulo", "/Conocenos/Index/" + frm.idSeleccion);
            divImgCambio(frm);
        })
    // iniciales
        inicial();
})