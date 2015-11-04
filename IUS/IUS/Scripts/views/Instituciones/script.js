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
        // click
            $(document).on("click", ".continente", function () {
                var frm = {
                    id: $(this).attr("id")
                }
                buscarContinente(frm);
            })
            
});