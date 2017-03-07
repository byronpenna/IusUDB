// funciones 
    function abrirModal(e, callback,elemento) {
        e.preventDefault();
        $(".modalContenido").hide();
        //$(".contenedorAjaxModalNew").hide();
        if (elemento === undefined) {
            elemento = $(".divUpload");
        }
        elemento.fadeIn(400, callback);
    }
    function inicialSearch() {
        var buscando = $(".txtHdBuscando").val();
        var img = "";
        if (buscando == 0) {
            buscando = 1;
            img = $(".txtHdImgBuscando").val();
        } else {
            buscando = 0;
            img = $(".txtHdImgBuscar").val();

        }
        console.log("img es: ", img);
        $(".imgSearchRepo").attr("src", img);
        var frm = {
            buscando: buscando
        }
        return frm;
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
    function getFrmBuscar() {
        var frm = { txtRuta: $(".txtDireccion").val() }
        if (frm.txtRuta.slice(-1) != "/") {
            frm.txtRuta += "/";
        }
        return frm;
    }
// eventos
    // keyup
        $(document).on("keyup", ".inputSearchRepo", function (e) {
            var charCode = e.which;
            if (charCode == 27) { // tecla esc cancela todo
                $(this).val("");
                
            }
            console.log("Key up es ", $(this).val());
            buscarEnCarpeta($(this).val());
            
        })
        $(document).on("keyup", ".txtDireccion", function (e) {
            var charCode = e.which;
            console.log("Char code ", charCode);
            if (charCode == 13) { // enter
                var frm = getFrmBuscar();
                spIrBuscar(frm);
            }
        })
    // click
        $(document).on("click", ".contenedorUpload", function (e) {
            e.stopPropagation();
        });
        $(document).on("click", ".divUpload", function (e) {
            console.log("Click en divUpload");
            if ($(".txtHdRecargar").val() == 1) {
                location.reload();
            }
            $(this).fadeOut();

        })
        $(document).on("click", ".closeModal", function (e) {
            $(".divUpload").click();
        })
        $(document).on("click", ".icoNuevaCarpeta", function (e) {
            console.log("Hay que abrir ");
            
            abrirModal(e, function () {
                $(".divNuevaCarpetaModal").show();
            }, $(".divNewModal"));
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
            /*var frm = { txtRuta: $(".txtDireccion").val() }
            if (frm.txtRuta.slice(-1) != "/") {
                frm.txtRuta += "/";
            }*/
            var frm = getFrmBuscar(frm);
            spIrBuscar(frm);
        })
        $(document).on("click", ".lkbSubMenu", function (e) {
            window.location = $(this).attr("href");
        })