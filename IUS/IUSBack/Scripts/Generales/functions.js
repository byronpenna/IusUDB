// plugins 
    // chosen
        function resetChosen(chosen) {
            //chosen.val('').trigger("liszt:updated");
            chosen.val('').trigger("chosen:updated");
        }
        function resetChosenWithSelectedVal(chosen,selectedVal) {
            chosen.val(selectedVal).trigger("chosen:updated");
        }
// tabla
    function cambioBackgroundColorTr(selector,color,activeClass) {
        $("" + selector + selector).css("background", "none");
        var classNombre = activeClass.substr(1, activeClass.length);
        $(activeClass).removeClass(classNombre);
        trSubMenu.css("background",color);
        trSubMenu.addClass(classNombre); // importantisimo esto debe ser dinamico de active clas pero quitandole primer caracter
    }
    function cancelarGlobal() {
        var x = confirm("¿Esta seguro que desea cancelar todo?");
        if (x) {
            $(".editMode").addClass("hidden");
            $(".normalMode").removeClass("hidden");
        }
    }
    function estadoControlGlobal() {
        if ($(".editMode").length == $(".editMode.hidden").length) {
            return true;
        } else {
            return false;
        }
    }
    function cambiarEstadoControlGlobal() {
        estadoControl = estadoControlGlobal();
        if (!estadoControl) {
            $(".controlGlobal").removeClass("hidden");
        } else {
            $(".controlGlobal").addClass("hidden");
        }
    }
    function controlesEdit(mostrar, tr) {
        if (mostrar) {
            selectorMostrar = ".editMode";
            selectorOcultar = ".normalMode";
            // poniendole clase para saber que va editar
            tr.addClass("trEdit");
        } else {
            selectorMostrar = ".normalMode";
            selectorOcultar = ".editMode";
            tr.removeClass("trEdit");
        }
        if (!tr.find(selectorOcultar).hasClass("hidden")) {
            tr.find(selectorOcultar).addClass("hidden");
        }
        if (tr.find(selectorMostrar).hasClass("hidden")) {
            tr.find(selectorMostrar).removeClass("hidden");
        }

    }
// updates 
    function actualizarCatalogo(urlAjax,frm,callback) {
        $.ajax({
            url: urlAjax,
            type: 'POST',
            data: {
                form: JSON.stringify(frm)
            },
            success:function(data){
                callback(data);
            }
        });
    }
// generics
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
// cargaObjetos json 
    function cargarObjetoGeneral(urlAjax,frm,callBack) {
        $.ajax({
            url: urlAjax,
            type: 'POST',
            data: {
                form: JSON.stringify(frm)
            },
            success: function (data) {
                callBack(data);
            }
        });
    }
    function cargarObjetoPersonas(callBack) {
        // funcion devuelve un objecto json con personas
        $.ajax({
            url: 'GestionPersonas/getJSONPersonas',
            type: 'POST',
            data: {},
            success: function (data) {
                callBack(data);
            }
        })
    }
// inputs 
    function comboAddOpcion(combo, opcion, selected) {
        txtActive = "";
        if (typeof(selected) != "undefined" && opcion.value == selected) {
            txtActive = "selected";
        }
        combo.append("<option value='"+opcion.value+"' "+txtActive+">"+opcion.text+"</option>");
    }