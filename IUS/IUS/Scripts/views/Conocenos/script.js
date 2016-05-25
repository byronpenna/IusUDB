$(document).ready(function () {
    
    // eventos 
        // click 
            $(document).on("click", ".divImgCambio", function () {
                console.log("Cambio D: D: D: ");
                $(".divImgCambio").removeClass("selectedIco");
                $(this).addClass("selectedIco");
                var selecionado = parseInt($(this).attr("id"));
                frm = { idSeleccion: selecionado };
                window.history.pushState({}, "Titulo", RAIZ+"Conocenos/Index/" + frm.idSeleccion);
                //history.replaceState({}, "Titulo", "/Conocenos/Index/" + frm.idSeleccion);
                divImgCambio(frm);
            })
    // iniciales
            inicial();
})