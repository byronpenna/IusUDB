﻿$(document).ready(function () {
    // eventos
            startSlider();
        // tap(slide)
            $(document).on("swipe", ".imgSlider", function () {
                console.log("Hizo tap");
            })
        // click 
            $(document).on("click", ".navBtn", function () {
                if (intervalo != null) {
                    clearInterval(intervalo);
                }
                direccion = $(this).attr("direccion"); //0 izquierda 1 derecha
                divSlider = $(this).parents(".slider");
                navBtn(divSlider, direccion);
                startSlider();
            })
})  