$(document).ready(function () {
    // eventos 
        $(document).on("click", ".btnEliminarComentario", function () {
            var frm = {
                idComentario: $(".txtHdIdComentario").val()
            }
            var tr = $(this).parents("tr");
            btnEliminarComentario(frm,tr);
        })
})