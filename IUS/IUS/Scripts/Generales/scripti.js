$(document).ready(function () {
    setIdiomaPreferido();
    $.extend($.expr[':'], {
        'containsi': function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || '').toLowerCase()
            .indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });
    // eventos         
            //$(".navPrincipal ul li").css("background", "pink");
            $(document).on("click", ".liPrincipal", function () {
                console.log("Entro a li", $(window).width());
                if ($(window).width() < 520) {
                    var a = $(this).find(".aMenuPrincipal");
                    var url = a.attr("href");
                    console.log("La url es", url);

                    location.replace(url);
                    //a.css("background", "yellow")
                }
            })
            $(document).on("click", ".hamburguerButton", function () {
                var padre = $(this).parent();
                var ul = padre.find("ul");
                if (ul.is(":visible")) {
                    ul.hide("slow");
                } else {
                    ul.show("slow");
                }
            })
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
    