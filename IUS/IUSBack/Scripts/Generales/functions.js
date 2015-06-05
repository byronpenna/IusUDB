// validaciones 
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
// Constantes 
    var INPUTS  = "input,select,textarea";
    //var RAIZ = "http://168.243.3.62/iusback/";
    var RAIZ = "http://localhost:55869/";
    //console.log("raiz es: ", RAIZ);
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
    // datatable 
        // sirve para eliminar 
        function removeDataTable(table, tr) {
            oTable = table.DataTable();
            oTable.row(tr).remove().draw(false);
        }
        // sirve para editar 
        function updateAllDataTable(tb) {
            var table = tb.DataTable();
            table.rows().every(function () {
                var d = this.data();
                d.counter++; // update data source for the row
                this.invalidate(); // invalidate the data DataTables has cached for this row
            });
            table.draw();
        }
    function getEdit(tabla, objFind, val) {
        retorno = tabla.find(objFind + "[value='" + val + "']");
        return retorno;
    }
    function clearTr(tr){
        tr.find(INPUTS).each(function (i) {
            $(this).val("");
        });
    }
    function cambioBackgroundColorTr(selector,color,activeClass,colorFont) {
        $("" + selector).css("background", "none");
        if (colorFont !== undefined) {
            $("" + selector).css("color", colorFont.antes);
        }
        var classNombre = activeClass.substr(1, activeClass.length);
        $(activeClass).removeClass(classNombre);
        trSubMenu.css("background", color);
        if (colorFont !== undefined) {
            trSubMenu.css("color", colorFont.despues);
        }
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
    function accionActualizarGeneral(tabla, ajaxUrl, callback) {
        var x = confirm("¿Esta seguro que desea actualizar todo?");
        if (x) {
            actualizarGeneral(tabla, ajaxUrl, callback);
        }
    }
    function actualizarGeneral(tabla, ajaxUrl, callback) {
        var frm = new Array(); // formulario a enviar 
        tabla.find("tr.trEdit").each(function (i, val) {
            obj = serializeSection($(this));
            frm[i] = obj;
        });
        actualizarCatalogo(ajaxUrl, frm, function (data) {
            callback(data, frm);
        });
    }
    function cancelarControlGlobal() {
        if (!$(".controlGlobal").hasClass("hidden")) {
            $(".controlGlobal").addClass("hidden");
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
    function controlesEdit(mostrar, tr, editClass,normalClass) {
        if (editClass === undefined) {
            editClass = ".editMode";
        }
        if (normalClass === undefined) {
            normalClass = ".normalMode";
        }
        if (mostrar) {
            selectorMostrar = editClass;
            selectorOcultar = normalClass;
            // poniendole clase para saber que va editar
            tr.addClass("trEdit");
        } else {
            selectorMostrar = normalClass;
            selectorOcultar = editClass;
            tr.removeClass("trEdit");
        }
        if (!tr.find(selectorOcultar).hasClass("hidden")) {
            tr.find(selectorOcultar).addClass("hidden");
        }
        if (tr.find(selectorMostrar).hasClass("hidden")) {
            tr.find(selectorMostrar).removeClass("hidden");
        }

    }
    function cancelarEdicion(tr) {
        var x = confirm("¿Esta seguro que desea cancelar la edicion?");
        if (x) {
            clearTr(tr);
            controlesEdit(false, tr);
        }
    }
// generics
    function clockHora(txtHora) {
        arrHora = txtHora.split(":");
        arrHoraFinal = arrHora[2].split(" ");
        segundos = arrHoraFinal[0];
        ind = arrHoraFinal[1];
        tiempo = { hora: parseInt(arrHora[0]), minuto: parseInt(arrHora[1]), segundo: parseInt(segundos),indicador:ind }
        tiempo.segundo++;
        if (tiempo.segundo > 60) {
            tiempo.minuto++;
            tiempo.segundo = 0;
            if (tiempo.minuto > 60) {
                tiempo.minuto = 0;
                tiempo.hora++;
                if (tiempo.hora > 12) {
                    tiempo.hora = 1;
                    if (indicador == "p.m.") {
                        tiempo.indicador = "a.m.";
                    } else {
                        tiempo.indicador = "p.m.";
                    }
                }
            }
        }
        minutos = tiempo.minuto; horas = tiempo.hora; segundos = tiempo.segundo;
        if (minutos < 9) {
            minutos = "0" + minutos;
        }
        if (horas < 9) {
            horas = "0" + horas;
        }
        if (segundos < 9) {
            segundos = "0" + segundos;
        }
        txtTiempo = horas + ":" + minutos + ":" + segundos + " " + tiempo.indicador;
        return txtTiempo;
    }
    function putOnDivEditable(text) {
        var sel, range, html;
        if (window.getSelection) {
            sel = window.getSelection();
            if (sel.getRangeAt && sel.rangeCount) {
                range = sel.getRangeAt(0);
                range.deleteContents();
                range.insertNode(document.createTextNode(text));
            }
        } else if (document.selection && document.selection.createRange) {
            document.selection.createRange().text = text;
        }
    }
    function getWidthPercent(element) {
        ancho = element.css("width");
        var width = (100 * parseFloat(ancho) / parseFloat(element.parent().css('width')));
        return width;
    }
    function getImageFromInputFile(inputFile,callback) {
        var reader = new FileReader();
        var img;
        reader.readAsDataURL(inputFile);
        reader.onload = function (evt) {
            var imagen = new Image();
            imagen.src = $(this)[0].result;
            callback(imagen);
        }
    }
    function getObjFormData(files, frm) {

        error = new Object();
        var data = null;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }
                // pequeño error absurdo y torpe que disminuye la eficiencia
                /*if (frm !== undefined) {
                    data.append("frm", frm);
                } else {
                    data.append("frm", "soy el formulario");
                }*/
                
                if (frm !== undefined) {
                    data.append("form",JSON.stringify(frm));
                    console.log("formulario agregado", frm);
                }
            } else {
                error.message = "Esta caracteristica no esta disponible para su navegador\
                por favor actualicelo o utilice otro(google chrome)\
                ";
                throw error;
            }
        } else {
            error.message = "Seleccione archivos a cargar";
            throw error;
        }
        return data;
    }
    // conversiones 
        function fechaStandar(fecha) {
            /*
            arrDate = frm.dtFechaNacimiento.split("/");
            frm.dtFechaNacimiento = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2], arrDate[1], arrDate[0]));
            */
            // solo funciona para formato dd/mm/yy
            arrDate = fecha.split("/");
            fecha = $.datepicker.formatDate("yy-mm-dd", new Date(arrDate[2], arrDate[1] -1 , arrDate[0]));
            return fecha;
        }
        function horaConvert(hora) {
            var time = hora;
            var hours = Number(time.match(/^(\d+)/)[1]);
            var minutes = Number(time.match(/:(\d+)/)[1]);
            var AMPM = time.match(/\s(.*)$/)[1];
            if (AMPM == "p.m." && hours < 12) hours = hours + 12;
            if (AMPM == "a.m." && hours == 12) hours = hours - 12;
            var sHours = hours.toString();
            var sMinutes = minutes.toString();
            if (hours < 10) sHours = "0" + sHours;
            if (minutes < 10) sMinutes = "0" + sMinutes;
            //console.log("hora en formato 24: ", sHours + ":" + sMinutes);
            var hora24 = sHours + ":" + sMinutes;
            return hora24;
            //alert(sHours + ":" + sMinutes);
        }
        function valMinh(val) {
            if (parseInt(val) < 10) {
                val = "0" + val;
            }
            return val;
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
// para select 
    function getOptionSelect(select,val) {
        option = select.find("option[value='" + val + "']");
        return option;
    }
    function updateOptionSelect(select, val, text, chosen) {
        //select.find("option[value='" + val + "']").empty().append(text);
        option = getOptionSelect(select, val);
        option.empty().append(text);
        if (!(chosen === null)) {
            resetChosen(select);
        }
    }
    function addOptionSelect(select,val,text,chosen) {
        option = "<option value='" + val + "'>" + text + "</option>";
        select.append(option);
        if (!(chosen === null)) {
            resetChosen(select);
        }
    }
    function removeOptionSelect(select,val,chosen) {
        option = getOptionSelect(select, val);
        option.remove();
        if (!(chosen === null)) {
            resetChosen(select);
        }
    }
// cargaObjetos json 
    function actualizarCatalogo(urlAjax,frm,callback) {
        $.ajax({
            url: urlAjax,
            type: 'POST',
            error: function(xhr,status,error){
                console.log("entro a los errores");
                console.log(xhr);
                console.log(status);
                console.log(error);
            },
            data: {
                form: JSON.stringify(frm)
            },
            success:function(data){
                callback(data);
            }
        });
    }
    function accionAjaxWithImage(urlAjax,formData,callBack) {
        $.ajax({
            type:           "POST",
            url:            urlAjax,
            contentType:    false,
            processData:    false,
            data: formData,
            success: function (data) {
                callBack(data);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    }
    function cargarObjetoGeneral(urlAjax,frm,callBack) {
        $.ajax({
            url: urlAjax,
            type: 'POST',
            data: {
                form: JSON.stringify(frm)
            },
            success: function (data) {
                callBack(data);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
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