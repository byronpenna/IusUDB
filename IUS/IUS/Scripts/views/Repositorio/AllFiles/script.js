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
                console.log("entro")
                var frm = {
                    idCategoria: $(".txtHdTipoCategoria").val(),
                    idCarpeta: $(".txtHdIdCarpetaActual").val()
                }
                if ($(this).hasClass("icoVistaLista")) {
                    accion = "lista";
                } else if ($(this).hasClass("iconoVistaCuadricula")) {
                    accion = "cuadricula";
                }
                icoVistaLista(frm, accion);
            })
            $(document).on("click", ".spIrBuscar", function () {
                var obj = getFrmSection($(this), ".divBusquedaRuta");
                spIrBuscar(obj.frm, obj.seccion);
            })
            $(document).on("click", ".btnBuscarCarpeta", function () {
                if ($(".rdBusqueda:checked").val() == 1) {
                    if (!$(this).hasClass("btnBuscando")) {
                        var frm = {
                            idCategoria: $(".txtHdTipoCategoria").val(),
                            nombre: $(".txtBusqueda").val()
                        }
                        console.log("Form a enviar", frm);
                        btnBuscarCarpeta(frm, $(this));
                    } else {
                        //console.log("Cancela busqueda");
                        var frm = {
                            idCarpeta: $(".txtHdIdCarpetaActual").val(),
                            idCategoria: $(".txtHdTipoCategoria").val()
                        }
                        console.log("formulario a enviar es:", frm);
                        cancelarBusqueda(frm);
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