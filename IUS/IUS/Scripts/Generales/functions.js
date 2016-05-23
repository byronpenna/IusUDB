// constantes
    // configuracion 
        //var RAIZ = "http://admacad.udb.edu.sv/IUS/";
        var RAIZ = $(".txtHdIus").val(); //"http://localhost:7196/";
        //var RAIZ = "http://168.243.3.62/ius/"

        //var RAIZ_BACK = RAIZ + "iusback/";
        var RAIZ_BACK = $(".txtHdIusback").val(); "http://localhost:55869/";
        //var RAIZ_BACK = "http://168.243.3.62/iusBack/"
        //var RAIZ_BACK = "http://admacad.udb.edu.sv/IUSBack";
    // clases 
        var SUCCESS_CLASS = "successMessage", FAIL_CLASS = "failMessage";
        var IMG_GENERALES = RAIZ + "/Content/images/generales/";
// mensajeria 
    function getSpanMessage(clase, txt) {
        return "<span class='" + clase + "'>" + txt + "</span>";
    }
    function printMessage(div, txt, success) {
        var clase = "";
        if (success) {
            clase = SUCCESS_CLASS;
        } else {
            clase = FAIL_CLASS;
        }
        div.empty().append(getSpanMessage(clase, txt));//"<span class='" + clase + "'>" + txt + "</span>"
        div.fadeIn("slow");
        setTimeout(function () {
            //alert("Hello");
            div.fadeOut("slow");

        }, 2000);
    }
// validacion
    function soloNumeros() {
        exp = "[0-9.]";
        return exp;
    }
    function soloLetras() {
        exp = "[a-z A-Zñáéíóú]";
        return exp;
    }
    function test(exp, str) {
        var patt = new RegExp(exp);
        var res = patt.test(str);
        return res;
    }
    
    function objArrIsEmpty(obj) {
        var empty = true;
        $.each(obj, function (i, val) {
            if (val.length > 0) {
                empty = false;
                return false;
            }
        })
        return empty;
    }
// idiomas 
    function setIdiomaPreferido() {
        var idIdioma = $.cookie('IUSidIdioma');
        console.log("id idioma global",idIdioma)
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
                $.cookie("IUSidioma", data.lang, { path: '/' });
                $.cookie("IUSidIdioma", data.idIdioma, { path: '/' });
                //localStorage.key()
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
                consolhttp://localhost:7196/Scriptse.log("entro a los errores");
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