// funciones 
    function abrirModal(e, callback) {
        e.preventDefault();
        $(".modalContenido").hide();
        $(".divUpload").fadeIn(400, callback);

    }
    function buscarEnCarpeta(txt) {
        var folder = $(".trRepo");
        var parents = ".trRepo";
        if (txt == "") {
            //$(".folders .folder").removeClass("hidden");
            folder.removeClass("hidden");
        } else {
            folder.addClass("hidden");
            var folders = folder.find(".spNombre:containsi(" + txt + ")");
            folders = folders.parents(parents);
            folders.removeClass("hidden");
        }
    }
// eventos
    // keyup
        $(document).on("keyup", ".inputSearch", function (e) {
            var charCode = e.which;
            if (charCode == 27) { // tecla esc cancela todo
                $(this).val("");
                
            }
            console.log("Key up es ", $(this).val());
            buscarEnCarpeta($(this).val());
            
        })
    // click
        $(document).on("click", ".contenedorUpload", function (e) {
            e.stopPropagation();
        });
        $(document).on("click", ".divUpload", function (e) {
            $(this).fadeOut();
        })
        $(document).on("click", ".closeModal", function (e) {
            $(".divUpload").click();
        })
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