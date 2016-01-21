// genericas
    // validacion 
        function validarComentario(frm){
            var estado = false;
            var val = new Object();
            val.campos = {
                txtNombre:          new Array(),
                txtEmail:           new Array(),
                txtAreaComentario:  new Array()
            }
            if (frm.txtNombre == "") {
                val.campos.txtNombre.push("Debe proporcionar un numero para poder comentar");
            }
            if (frm.txtEmail == "") {
                val.campos.txtEmail.push("Debe proporcionar un numero para poder comentar");
            }
            if (frm.txtAreaComentario == "") {
                val.campos.txtAreaComentario.push("Debe escribir algo para comentar");
            }

            val.estado = objArrIsEmpty(val.campos);
            return val;
        }
    // otras
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
                $("#frmComentario")[0].reset();
                div = getDivComentario(data.comentario);
                divSeccionComentarios = $(".comentariosSection");
                if (divSeccionComentarios.find(".noComentFound").length > 0) {
                    divSeccionComentarios.empty().append(div).show("slow");
                } else {
                    divSeccionComentarios.prepend(div).show("slow");
                }
            } else {
                if (data.error !== undefined && data.error != null && data.error._mostrar) {
                    printMessage($(".divPrintFrmComentario"), data.error.Message, false);
                } else {
                    printMessage($(".divPrintFrmComentario"), "No fue posible publicar comentario",false)
                }
                //alert();
            }
        });
    }