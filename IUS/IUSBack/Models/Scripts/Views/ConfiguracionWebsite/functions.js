// tabla 
    function getTrValor(valor) {
        tr = "\
            <tr>\
                <td class='hidden'>\
                    <input type='text' name='txtIdValor' class='txtIdValor' value='" + valor._idValor + "'>\
                </td>\
                <td>"+valor._valor+"</td>\
                <td>\
                    <i class='fa fa-times pointer iconQuitarValor'></i>\
                </td>\
            </tr>\
        ";
        return tr;
    }
    function getDivImageSlider(image){
        div = "\
        <div class='col-lg-6 divImgIndividual'>\
            <input type='hidden' name='txtHdIdSliderImage' class='txtHdIdSliderImage' value='"+image._idSliderImage+"' />\
            <img src='data:image/png;base64,"+image._strImagen+"' class='fullSize' />\
            <button class='btn btn-danger btnEliminarImage'>\
                Eliminar\
            </button>\
            <button class='btn btn-warning btnDeshabilitarSliderImage' estado='"+image._estado+"'>\
                "+image.textoEstado+"\
            </button>\
        </div>";
        return div;
    }
// acciones scripts
    function btnEliminarImage(section) {
        frm = serializeSection(section);
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_eliminarImagenSlider", frm, function (data) {
            console.log("respuesta del servidor es: ", data);
            if (data.estado) {
                section.remove();
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function btnDeshabilitarSliderImage(section,btn) {
        form = serializeSection(section);
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_cambiarEstado", form, function (data) {
            console.log("respuesta de servidor es: ", data);
            if (data.estado) {
                // cosa para cambiar
                btn.empty().text(data.image.textoEstado);
                btn.attr("estado", data.image._estado);
            } else {
                alert("Ocurrio un error");
            }
        });
    }
    function formularioSubir(formData, url,section) {
        accionAjaxWithImage(url,data, function (data) {
            console.log("La respuesta del servidor para frm es:",data);
            if (data.estado) {
                alert("Imagen ingresada correctamente");
                div = getDivImageSlider(data.archivos);
                console.log("div a agregar es:", div);
                $(".divImgSlider").prepend(div);
                section[0].reset();
            }
        })
    }
    function iconQuitarValor(tr) {
        frm = serializeSection(tr);
        console.log("formulario a enviar", frm);
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_eliminarValoresConfig", frm, function (data) {
            if (data.estado) {
                tr.remove(); // por el momento que no es dataTable

            } else {
                console.log("el error es: ", data.error);
                alert("ocurrio un error");
            }
        });
    }
    function btnAddValor(frm) {
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_agregarValoresConfig", frm, function (data) {
            console.log("la respuesta es: ", data);
            if (data.estado) {
                tbody = $(".tableValores").find("tbody");
                newTr = getTrValor(data.valor);
                if (tbody.find(".trNoValor").length == 0) {
                    tbody.prepend(newTr);
                    $(".txtValores").val("");
                } else {
                    $(".txtValores").val("");
                    tbody.empty().append(newTr);
                }
            } else {
                console.log("obj error es: ", data.error);
                alert("ocurrio un error");
            }
        });
    }
    function frmInstitucional(frm) {
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_actualizarInfoConfig", frm, function (data) {
            console.log("La respuesta del servidor es: ", data);
            if (data.estado) {
                alert("Actualizado correctamente");
                configuracion = data.configuracion;
                $("#txtAreaVision").val(configuracion._vision);
                $("#txtAreaMision").val(configuracion._mision);
                $("#txtAreaHistoria").val(configuracion._historia);
            } else {
                console.log("error", data.error);
                alert("Ocurrio un error");
            }
        })
    }