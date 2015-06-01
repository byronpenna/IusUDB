$(document).ready(function () {
    // eventos
        // tap(slide)
            $(document).on("swipe", ".imgSlider", function () {
                console.log("Hizo tap");
            })
        // click 
            $(document).on("click", ".navBtn", function () {
                direccion = $(this).attr("direccion"); //0 izquierda 1 derecha
                divSlider = $(this).parents(".slider");
                navBtn(divSlider, direccion);
            })
        

})  