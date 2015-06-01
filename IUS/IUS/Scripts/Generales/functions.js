﻿// constantes
    var RAIZ = "http://localhost:7196/";
// idiomas 
    function setIdiomaPreferido() {
        var idIdioma = $.cookie('IUSidIdioma');
        if (idIdioma !== undefined) {
            $(".cbIdioma option[value='" + idIdioma + "']").attr("selected", true);
        }
    }
// eventos por master page
    function cbIdioma(idIdioma) {
        frm = { idIdioma: idIdioma };
        console.log("formulario a enviar es: ", frm);
        actualizarCatalogo(RAIZ + "Home/sp_trl_getIdiomaFromIds", frm, function (data) {
            console.log("La respuesta del servidor es: ", data);
            if (data.estado) {
                $.removeCookie('IUSidioma')
                $.cookie("IUSidioma", data.lang);
                $.cookie("IUSidIdioma", data.idIdioma);
                location.reload();
            } else {
                alert("ocurrio un error");
            }
        });
    }
// ajax 
    function actualizarCatalogo(urlAjax, frm, callback) {
        $.ajax({
            url: urlAjax,
            type: 'POST',
            error: function (xhr, status, error) {
                console.log("entro a los errores");
                console.log(xhr);
                console.log(status);
                console.log(error);
            },
            data: {
                form: JSON.stringify(frm)
            },
            success: function (data) {
                callback(data);
            }
        });
    }
// serialize 
    function serializeToJson(a) {
        var o = {};
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }
    function serializeSection(section) {
        var frm = serializeToJson(section.find("input,select,textarea").serializeArray());
        return frm;
    }
    function serializeForm(frm) {
        var frm = serializeToJson(frm.serializeArray());
        return frm;
    }