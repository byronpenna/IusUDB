// generics 
    function editarTamanioPersonalizado(valor) {
        div = $(".divTamanioPersonalizado");
        txt = div.find(".txtTamañoImagen");
        div.removeClass("hidden");
        div.find(".ckTamanio").prop("checked", true);
        txt.val(valor);
        txt.prop("disabled", false);
    }
    function mostrarTamanioPersonalizado() {
        div = $(".divTamanioPersonalizado");
        txt = div.find(".txtTamañoImagen");
        div.removeClass("hidden");
        div.find(".ckTamanio").prop("checked", false);
        txt.val(100);
        txt.prop("disabled", true);
    }
// acciones scripts 
    function frmNoticia(formulario) {
        frm = serializeToJson(formulario.serializeArray());
        console.log("formulario a enviar es: ", frm);
        frm.contenido = nicEditors.findEditor('editor').getContent();
        console.log("formulario a enviar es: ", frm);
        actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_noticias_publicarPost", frm, function (data) {
            $("#div_carga").hide();
            console.log("respuesta del servidor", data);
            if (data.estado) {
                alert("Noticia agregada correctamente");
            } else {
                alert("Ocurrio un error");
            }
        });
    };