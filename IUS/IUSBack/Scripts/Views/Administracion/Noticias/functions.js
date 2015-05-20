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
    function updatePost(formulario) {
        frm = serializeToJson(formulario.serializeArray());
        frm.contenido = nicEditors.findEditor('editor').getContent();
        frm.tags = $(".txtEtiquetas").val();
        console.log("Formulario a enviar:", frm);
        actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_noticias_modificarPost", frm, function (data) {
            console.log("la data devuelta por el servidor es:", data);
            $("#div_carga").hide();
            if (data.estado) {
                tags = data.tags;
                txtTag = "";
                $.each(tags, function (i, val) {
                    if (i == 0) {
                        txtTag += val._strTag;
                    } else {
                        txtTag += "," + val._strTag;
                    }
                });
                $(".txtEtiquetas").val(txtTag);
                alert("Actualizado correctamente");
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function frmNoticia(formulario) {
        frm = serializeToJson(formulario.serializeArray());
        frm.contenido   = nicEditors.findEditor('editor').getContent();
        frm.tags        = $(".txtEtiquetas").tagsinput('items');
        actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_noticias_publicarPost", frm, function (data) {
            $("#div_carga").hide();
            console.log("respuesta del servidor", data);
            if (data.estado) {
                alert("Noticia agregada correctamente");
                window.location = RAIZ + "Noticias";
            } else {
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
        });
    };