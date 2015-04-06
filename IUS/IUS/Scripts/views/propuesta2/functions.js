function slide(direccion) {
    var active = $(".active");
    console.log("direccion", direccion);
    var siguiente = -1;
    if (direccion == 1) {
        siguiente = active.next();
    } else {
        siguiente = active.prev();
    }
    console.log("siguiente antes:", siguiente.val());
    if (direccion == 1 && typeof siguiente.val() === 'undefined') {
        console.log("ira a la primera");
        siguiente = $(".sliderImg").first();
    } else if (direccion == 0 && typeof siguiente.val() === 'undefined') {
        console.log("Ira al final");
        siguiente = $(".sliderImg").last();
    }
    console.log("siguiente despues:", siguiente.val());
    siguiente.addClass("active");
    active.removeClass("active");
    $(".header").css("background", "url(" + siguiente.val() + ")");
    $(".header").css("background-size", "cover");
}