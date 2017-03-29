$(document).ready(function () {
    // eventos
        iniciales();
        // clics
            $(document).on("click", ".menuLateral li", function () {
                var id = $(this).attr("id");
                window.location.href = RAIZ + "Instituciones/index/" + id;
            })
})