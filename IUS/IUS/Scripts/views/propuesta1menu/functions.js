function slide(direcccion) {
    var active      = $(".active");
    var siguiente   = -1;

    if (direcccion == 1) {
        siguiente = active.next();
    } else {
        siguiente = active.prev();
    }
    console.log("siguiente antes:", siguiente.attr("src"));
    if(direcccion == 1 && typeof siguiente.attr("src") === 'undefined'){
        console.log("ira a la primera");
        siguiente = $(".imgSlider").first();
    } else if(direcccion == 0 && typeof siguiente.attr("src") === 'undefined'){
        console.log("Ira al final");
        siguiente = $(".imgSlider").last();
    }
    console.log("siguiente es:",siguiente.attr("src"));
    siguiente.addClass("active");
    active.removeClass("active");
}