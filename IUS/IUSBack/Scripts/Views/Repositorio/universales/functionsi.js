// funciones 
    function abrirModal(e, callback) {
        e.preventDefault();
        $(".modalContenido").hide();
        $(".divUpload").fadeIn(400, callback);

    }
// eventos
    // click
        $(document).on("click", ".icoNuevaCarpeta", function (e) {
            console.log("Hay que abrir ");
            abrirModal(e, function () {
                $(".divNuevaCarpetaModal").show();
            });
        })
        $(document).on("click", ".NewActionLi", function () {
            var elemento = $(this).find(".ulSub");
            if (elemento.is(":visible")) {
                elemento.hide();
                console.log("Mostrar");
            } else {
                //$(this).find(".ulSub").hide();
                elemento.show();
                console.log("Ocultar");
            }
        })
        $(document).on("click", ".spIrBuscar", function () {
            frm = { txtRuta: $(".txtDireccion").val() }
            if (frm.txtRuta.slice(-1) != "/") {
                frm.txtRuta += "/";
            }
            spIrBuscar(frm);
        })
        $(document).on("click", ".lkbSubMenu", function (e) {
            window.location = $(this).attr("href");
        })