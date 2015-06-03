$(document).ready(function () {
    // eventos 
        // click 
            $(document).on("click", ".btnDesplegableAddComent", function () {
                div = $(".agregarComentarioSection");
                if (div.is(':visible')) {
                    $(".spanIco i").removeClass("fa-angle-up");
                    $(".spanIco i").addClass("fa-angle-down");
                    div.hide("slow");
                } else {
                    $(".spanIco i").removeClass("fa-angle-down");
                    $(".spanIco i").addClass("fa-angle-up");
                    div.show("slow");
                }
            });
});