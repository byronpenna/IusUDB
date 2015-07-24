// constantes
    //var RAIZ = "http://admacad.udb.edu.sv/IUS/";
    var RAIZ = "http://localhost:7196/";
    //var RAIZ = "http://168.243.3.62/ius/"
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
    function actualizarCatalogo(urlAjax, frm, callback,before) {
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
            beforeSend: function(){
                if (before !== undefined) {
                    before();
                }
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
    function getFrmSection(selector, parentSection) {
        var obj = new Object();
        obj.seccion = selector.parents(parentSection);
        obj.frm = serializeSection(obj.seccion);
        return obj;
    }
    function serializeSection(section) {
        var frm = serializeToJson(section.find("input,select,textarea").serializeArray());
        return frm;
    }
    function serializeForm(frm) {
        var frm = serializeToJson(frm.serializeArray());
        return frm;
    }