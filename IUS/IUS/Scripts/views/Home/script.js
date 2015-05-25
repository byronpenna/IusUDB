$(document).ready(function () {
    // eventos
        // change
            $(document).on("change", ".cbIdioma", function () {
                idIdioma = $(this).val();
                console.log("entro");
                if (idIdioma != -1 && idIdioma > 0) {
                    cbIdioma(idIdioma);
                }
            })
})  