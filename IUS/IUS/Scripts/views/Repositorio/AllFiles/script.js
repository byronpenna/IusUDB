$(document).ready(function () {
    // eventos
        // click
            $(document).on("click", ".spIrBuscar", function () {
                var obj = getFrmSection($(this), ".divBusquedaRuta");
                spIrBuscar(obj.frm, obj.seccion);
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
            if ($(this).val() == "") {
                $(".folder").removeClass("hidden");
            } else {
                //if (!$(".folder").hasClass("hidden")) {
                    $(".folder").addClass("hidden");
                //}
                var folders = $(".folder .folderTitle:containsi(" + $(this).val() + ")");
                folders = folders.parents(".folder");
                //folders.css("background", "yellow");
                folders.removeClass("hidden");
                /*folders.each(function (i, folder) {
                    folder = folder.parents(".folder");
                    console.log("este es", folder);
                })*/
            }
        })
})