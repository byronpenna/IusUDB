$(document).ready(function () {
    // eventos
        // over
            $(document).on("mouseover", ".continente", function () {
                $(this).find("h3").removeClass("hidden");
            });
        // out
            $(document).on("mouseout", ".continente", function () {
                $(this).find("h3").addClass("hidden");
            })
});