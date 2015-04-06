$(document).ready(function () {
    
    $(document).on("click", ".nav", function () {
        var direccion = $(this).attr("direccion");
        slide(direccion);
    });
});