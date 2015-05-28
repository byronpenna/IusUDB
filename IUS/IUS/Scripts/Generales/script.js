$(document).ready(function () {
    // Constantes
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
});