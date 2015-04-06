$(document).ready(function () {
    $(document).on("click", ".navBtn", function () {
        var direccion = $(this).attr("direccion");
        slide(direccion);
    });
})