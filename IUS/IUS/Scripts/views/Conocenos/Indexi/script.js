$(document).ready(function () {
    
    // eventos 
        $(document).on("click", ".menuConocenos li", function () {
            $(".menuConocenos li").removeClass("activeConocenos");
            $(this).addClass("activeConocenos");
            var id = $(this).attr("id");
            frm = { idSeleccion: id };
            window.history.pushState({}, "Titulo", RAIZ + "Conocenos/Index/" + frm.idSeleccion);
            if (frm.idSeleccion != -1) {
                divImgCambio(frm);
            } else {
                divDocumentos();
            }
            
        })
    // iniciales
        inicial();
})