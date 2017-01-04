$(document).ready(function () {
    
    
    // eliminar 
        $(document).on("click", ".btnEliminarArchivo", function () {
            var x = confirm("¿Esta seguro que desea dejar de compartir este archivo?");
            if (x) {
                var tr = $(this).parents("tr");
                var frm = {
                    idArchivoPublico: tr.find(".txtHdIdArchivoPublico").val()
                }
                console.log("Formulario a enviar es: ", frm); 
                btnEliminarArchivo(frm, tr);
            }
        })
        $(document).on("click", ".btnEliminarCarpeta", function () {
            var tr = $(this).parents("tr");
            var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
            frm = { idCarpeta: tr.find(".txtHdIdCarpeta").val() }
            if (x) {
                btnEliminarCarpeta(frm, tr)
            }
        })
    
})