$(document).ready(function () {
    $(document).on("click", ".aNum", function () {
        location.href = this.href; // ir al link 
    })
    $(document).on("click", ".aSiguiente", function (e) {
        //console.log("bla bla ");
        e.preventDefault();
        var elemento = $(".numActive").next().next();
        var x = elemento.hasClass("aNum");
        elemento.css("backgroud", "red");
        console.log("bla bla ", elemento);
        if (x) {
            elemento.click();
        }
    })
})