// genericas
    function getDivComentario(comentario) {
        div = "<div class='divComentario'>\
                <h3>" + comentario._nombre + "</h3>\
                <p>\
                    " + comentario._comentario + "\
                    <div class='spanDate'>" + comentario.getTxtFecha + "</div>\
                </p>\
            </div>";
        return div;
    }
// acciones script
    function frmComentario(frm) {
        console.log("formulario a enviar es:", frm);
        actualizarCatalogo(RAIZ + "Noticias/sp_frontUi_noticias_ponerComentario", frm, function (data) {
            console.log("La respuesta del servidor es:", data);
            if (data.estado) {
                div = getDivComentario(data.comentario);
                divSeccionComentarios = $(".comentariosSection");
                if (divSeccionComentarios.find(".noComentFound").length > 0) {
                    divSeccionComentarios.empty().append(div).show("slow");
                } else {
                    divSeccionComentarios.prepend(div).show("slow");
                }
            } else {
                alert("No fue posible publicar comentario");
            }
        });
    }