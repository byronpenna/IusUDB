// iniciales
    function startSlider() {
        console.log("inicio el slider");
        setInterval(function () {
            $("#navRight").click();
        }, 5000);
    }
    function getNextImage(img, direccion) {
        var next;
        if (direccion == 1) {
            next = img.next();
            if (next.attr("src") === undefined) {
                //next = img.first();
                next = img.parents(".slider").find("img").first();
            }
        } else {
            next = img.prev();
            if (next.attr("src") === undefined) {
                //next = img.last();
                next = img.parents(".slider").find("img").last();
            }
        }
        return next;
    }
    var intervalo = null;
    function startSlider() {
        intervalo = setInterval(function () {
            var nav     = $(".navRight");
            direccion   = nav.attr("direccion"); //0 izquierda 1 derecha
            divSlider   = nav.parents(".slider");
            navBtn(divSlider, direccion);
            //nav.click();
        }, 3000);
    }
// acciones scripts 
    function navBtn(divSlider, direccion) {
        img = divSlider.find(".activeSliderImage");
        next = getNextImage(img, direccion);

        img.removeClass("activeSliderImage");
        img.addClass("hidden");
        next.addClass("activeSliderImage");
        next.removeClass("hidden");
    }
    