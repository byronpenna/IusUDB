$(document).ready(function () {
    
    // eventos 
        // click 
            $(document).on("click", ".divImgCambio", function () {
                console.log("Cambio D: D: D: ");
                $(".divImgCambio").removeClass("selectedIco");
                $(this).addClass("selectedIco");
                var selecionado = parseInt($(this).attr("id"));
                frm = { idSeleccion: selecionado };
                divImgCambio(frm);
            })
    // iniciales
            inicial();
})