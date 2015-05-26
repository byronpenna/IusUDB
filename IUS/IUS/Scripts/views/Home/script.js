$(document).ready(function () {
    // iniciales
        setIdiomaPreferido();
    // eventos
        // click 
            $(document).on("click", ".navBtn", function () {
                direccion = $(this).attr("direccion"); //0 izquierda 1 derecha
                divSlider = $(this).parents(".slider");
                navBtn(divSlider, direccion);
            })
        // change
            $(document).on("change", ".cbIdioma", function () {
                idIdioma = $(this).val();
                console.log("entro");
                if (idIdioma != -1 && idIdioma > 0) {
                    cbIdioma(idIdioma);
                }
            })

})  