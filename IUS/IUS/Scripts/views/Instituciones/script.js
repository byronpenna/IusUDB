$(document).ready(function () {
    // eventos
        // over
            $(document).on("mouseover", ".continente", function () {
                $(this).find("h3").removeClass("nombreContinente");
            });
        // out
            $(document).on("mouseout", ".continente", function () {
                $(this).find("h3").addClass("nombreContinente");
            })
});