// iniciales
    function getNextImage(img,direccion) {
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
    function startSlider() {
        setInterval(function () {
            $(".navRight").click();
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
    