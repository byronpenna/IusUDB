$(document).ready(function () {
    $(document).on("click", ".btnEliminarCarpeta", function () {
        var tr = $(this).parents("tr");
        var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
        frm = { idCarpeta: tr.find(".txtHdIdCarpeta").val() }
        if (x) {
            btnEliminarCarpeta(frm, tr)
        }
    })
    
})