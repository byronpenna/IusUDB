$(document).ready(function () {
    //console.log("Fue un click");
    // Eventos
        // click
            $(document).on("click", ".bloqueLink", function () {
                window.location.href = $(this).find(".txtHdUrlLink").val();
            })
            $(document).on("click", ".navBtn", function () {
                if (intervalo != null) {
                    clearInterval(intervalo);
                }
                direccion = $(this).attr("direccion"); //0 izquierda 1 derecha
                divSlider = $(".slider");//$(this).parents(".slider");
                navBtn(divSlider, direccion);
                startSlider();
            })
})