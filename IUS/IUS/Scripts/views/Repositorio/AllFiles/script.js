$(document).ready(function () {
    // eventos
        // change
            $(document).on("change", ".rdBusqueda", function () {
                if ($(this).val() == 1) {
                    $(".folder").removeClass("hidden");
                    $(".btnBuscarCarpeta").removeClass("visiHidden");
                } else {
                    buscarCarpeta($(".txtBusqueda").val())
                    $(".btnBuscarCarpeta").addClass("visiHidden");
                }

            });
        // click
            $(document).on("click", ".controlVista", function (e) {
                e.preventDefault();
                var seccion = null;
                if ($(this).hasClass("icoVistaLista")) {
                    accion = "lista";
                    seccion = $(".targetListView");
                } else if ($(this).hasClass("iconoVistaCuadricula")) {
                    accion = "cuadricula";
                    seccion = $(".cuadricula");
                }
                
                if (isSearch()) {
                    var frm = {
                        idCategoria: $(".txtHdTipoCategoria").val(),
                        nombre: $(".txtBusqueda").val()
                    }
                    console.log("Buscaras en carpeta||||||", frm);
                    btnBuscarCarpeta(frm, seccion, accion, function () {
                        vistaActiva(accion);
                    });

                } else {
                    var frm = {
                        idCategoria: $(".txtHdTipoCategoria").val(),
                        idCarpeta: $(".txtHdIdCarpetaActual").val()
                    }
                    console.log("Buscaras en lista||||||", frm);
                    icoVistaLista(frm, accion);
                }
                
                
            })
            $(document).on("click", ".spIrBuscar", function () {
                var obj = getFrmSection($(this), ".divBusquedaRuta");
                spIrBuscar(obj.frm, obj.seccion);
            })
            //*********************************************************
            $(document).on("click", ".btnBuscarCarpeta", function () {
                var vistaActual = getVistaActual();
                var seccion = null;
                if (vistaActual == "cuadricula") {
                    seccion = $(".cuadricula");
                } else if (vistaActual == "lista") {
                    seccion = $(".targetListView");
                }
                if ($(".rdBusqueda:checked").val() == 1) {
                    if (!$(this).hasClass("btnBuscando")) {
                        var frm = {
                            idCategoria: $(".txtHdTipoCategoria").val(),
                            nombre: $(".txtBusqueda").val()
                        }
                        btnBuscarCarpeta(frm,seccion,vistaActual);//$(this)
                    } else {
                        //console.log("Cancela busqueda");
                        var frm = {
                            idCarpeta: $(".txtHdIdCarpetaActual").val(),
                            idCategoria: $(".txtHdTipoCategoria").val()
                        }
                        console.log("formulario a enviar es:", frm);
                        cancelarBusqueda(frm,seccion,vistaActual);
                    }
                }
            })
        // keyup    
            $(document).on("keyup", ".txtRutaPublica", function (e) {
                var charCode = e.which;
                if (charCode == 13) {// tecla enter
                    var obj = getFrmSection($(this), ".divBusquedaRuta");
                    spIrBuscar(obj.frm, obj.seccion);
                }
            })
            $(document).on("keyup", ".txtBusqueda", function (e) {
                console.log(e.which);
                if ($(".rdBusqueda:checked").val() == 0) {

                    if ($(this).val() == "") {
                        $(".folder").removeClass("hidden");
                    } else {
                        buscarCarpeta($(this).val());
                    }
                } else if($(".rdBusqueda:checked").val() == 1) {
                    if(e.which == 13){
                        $(".btnBuscarCarpeta").click();
                    }
                }
            })
})