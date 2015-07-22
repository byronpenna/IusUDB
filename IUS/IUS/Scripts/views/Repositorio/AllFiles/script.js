$(document).ready(function () {
    // eventos
        // change
            $(document).on("change", ".rdBusqueda", function () {
                if ($(this).val() == 1) {
                    $(".folder").removeClass("hidden");
                } else {
                    buscarCarpeta($(".txtBusqueda").val())
                }

            });
        // click
            $(document).on("click", ".spIrBuscar", function () {
                var obj = getFrmSection($(this), ".divBusquedaRuta");
                spIrBuscar(obj.frm, obj.seccion);
            })
            $(document).on("click", ".btnBuscarCarpeta", function () {
                if (!$(this).hasClass("btnBuscando")) {
                    var frm = {
                        idCategoria: $(".txtHdTipoCategoria").val(),
                        nombre: $(".txtBusqueda").val()
                    }
                    console.log("Form a enviar", frm);
                    btnBuscarCarpeta(frm, $(this));
                } else {
                    //console.log("Cancela busqueda");
                    cancelarBusqueda();
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
                if ($(".rdBusqueda:checked").val() == 0) {
                    if ($(this).val() == "") {
                        $(".folder").removeClass("hidden");
                    } else {
                        buscarCarpeta($(this).val());
                    }
                }  
            })
})