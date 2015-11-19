$(document).ready(function () {
    // eventos 
        // click 
            $(document).on("click", ".divImgCambio", function () {
                $(".divImgCambio").removeClass("selectedIco");
                $(this).addClass("selectedIco");
                var selecionado = parseInt($(this).attr("id"));
                frm = { idSeleccion: selecionado };
                divImgCambio(frm);
            })
            
})