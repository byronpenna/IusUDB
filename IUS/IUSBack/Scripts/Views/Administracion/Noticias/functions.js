// generics 
    function cleanActive() {
        $(".activeRichImage").each(function () {
            $(this).removeClass("activeRichImage");
            $(this).parent().removeAttr("contenteditable");
            $(this).css("display", "");
        })
        $(".areaImgRich").remove();
    }

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
    function validarInsert(frm) {
        var val = new Object();
        val.campos = {
            cbCategorias:   new Array(),
            cbIdioma:       new Array(),
            txtEtiquetas:   new Array(),
            txtHdIdPost:    new Array(),
            txtTitulo:      new Array(),
            editor:         new Array(),
        }
        val.general = new Array();
        console.log($.isArray(frm.cbCategorias));
        if (frm.cbCategorias === undefined ) { //!$.isArray(frm.cbCategorias)
            val.campos.cbCategorias.push("Este campo no puede ir vacio");
        }
        if (frm.txtTitulo === undefined || frm.txtTitulo == "") {
            val.campos.txtTitulo.push("Este campo no puede ir vacio");
        }
        if (frm.contenido === undefined || frm.contenido == "" || frm.contenido == "<br>") {
            val.campos.editor.push("Vamos ingresa algo aqui ¿como puede haber una noticia sin contenido? :| ");
        }
        val.estado = getEstadoVal(val);
        return val;
    }
    function updatePost(frm) {
        //frm = serializeToJson(formulario.serializeArray());
        //formulario.contenido = nicEditors.findEditor('editor').getContent();
        //formulario.tags = $(".txtEtiquetas").val();
        console.log("Formulario a enviar:", frm);
        actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_noticias_modificarPost", frm, function (data) {
            console.log("la data devuelta por el servidor es:", data);
            $("#div_carga").hide();
            if (data.estado) {
                tags = data.tags;
                txtTag = "";
                if (tags !== null) {
                    $.each(tags, function (i, val) {
                        if (i == 0) {
                            txtTag += val._strTag;
                        } else {
                            txtTag += "," + val._strTag;
                        }
                    });
                }
                $(".txtEtiquetas").val(txtTag);
                alert("Actualizado correctamente");
            } else {
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
        });
    }
    function frmNoticia(frm) {
        //frm = serializeToJson(formulario.serializeArray());
        //frm.contenido   = nicEditors.findEditor('editor').getContent();
        //frm.tags = $(".txtEtiquetas").tagsinput('items');
        //frm.cbCategorias = $(".cbCategorias").val();
        console.log("formulario a enviar", frm);
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