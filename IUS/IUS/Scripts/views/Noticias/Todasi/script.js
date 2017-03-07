$(document).ready(function () {
    console.log("D:");
    $(document).on("click", ".aNum", function (e) {
        e.preventDefault();
        var url = this.href; // ir al link 
        url += "/" + $(".txtOp").val();
        console.log("url es ", url);
        //location.href = url;
    })
    $(document).on("click", ".spFiltro", function () {
        var id = $(this).attr("id");
        console.log("id es ", id);
        $(".spFiltro").removeClass("activeIUS");
        $(this).addClass("activeIUS");
        var frm = {

        }
        if (id == "spNoti") {
            frm.op = 2;
        } else if (id == "spEvento") {
            frm.op = 3;
        }
        frm.numero = $(".txtN").val()
        frm.pagina = $(".txtPage").val()
        $(".txtOp").val(frm.op);
        spFiltro(frm);
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