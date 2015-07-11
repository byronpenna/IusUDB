﻿$(document).ready(function () {
    $.extend($.expr[':'], {
        'containsi': function (elem, i, match, array) {
            return (elem.textContent || elem.innerText || '').toLowerCase()
            .indexOf((match[3] || "").toLowerCase()) >= 0;
        }
    });
    // automaticos
        setIdiomaPreferido();
    // Constantes
    // Eventos 
        // click 
            $(document).on("click", ".desplegableMobile", function () {
                ulMenu = $(this).parents(".mobileButton").parent().find(".ulMenu");
                if (ulMenu.is(':visible')) {
                    console.log("visible")
                    ulMenu.hide();
                } else {
                    console.log("no visible")
                    ulMenu.show();
                }
            });
        // change
            $(document).on("change", ".cbIdioma", function () {
                idIdioma = $(this).val();
                console.log("entro");
                if (idIdioma != -1 && idIdioma > 0) {
                    cbIdioma(idIdioma);
                }
            })
});