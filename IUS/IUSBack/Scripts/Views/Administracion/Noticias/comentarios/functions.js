// script 
    function btnEliminarComentario(frm,tr) {
        actualizarCatalogo(RAIZ + "/ComentarioNoticia/sp_frontUi_noticias_back_delComentarioPost", frm, function (data) {
            console.log(data);
            if (data.estado) {
                tr.remove();
            }
        })
    }