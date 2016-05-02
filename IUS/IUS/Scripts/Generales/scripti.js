$(document).ready(function () {
    setIdiomaPreferido();
    // eventos 
        // change
            $(document).on("change", ".cbIdioma", function () {
                idIdioma = $(this).val();
                console.log("entro");
                if (idIdioma != -1 && idIdioma > 0) {
                    cbIdioma(idIdioma);
                }
            })
        // mouseenter        
            $(document).on("mouseenter", ".hoverEventNoti", function () {
                $(this).find(".divHoverNotiEve").stop();
                $(this).find(".text-tituloNotiEve").stop();

                $(this).find(".divHoverNotiEve").fadeIn("slow");
                $(this).find(".text-tituloNotiEve").fadeIn("slow");
                //$(this).finish();
            })
        // mouseleave
            $(document).on("mouseleave", ".hoverEventNoti", function () {
                $(this).find(".divHoverNotiEve").stop();
                $(this).find(".text-tituloNotiEve").stop();

                $(this).find(".divHoverNotiEve").fadeOut("slow");
                $(this).find(".text-tituloNotiEve").fadeOut("slow");
                //$(this).finish();
            })
})
    