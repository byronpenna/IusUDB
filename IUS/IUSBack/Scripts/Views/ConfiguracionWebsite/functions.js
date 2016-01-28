// genericas
        function releaseCheck() {
            this.setOptions({ setSelect: [0, 0, 700, 300] });
        }
    // para jCrop
        function storeCoords(c) {
            $(".x").val(c.x);
            $(".y").val(c.y);
            $(".imgAlto").val(c.h);
            $(".imgAncho").val(c.w);
        };
        function inicialFoto() {
            $(".x").val(0);
            $(".y").val(0);
            $(".imgAlto").val(0);
            $(".imgAncho").val(0);
        }
    // para subida
        function getFrmSlide() {
            var imgElement  = $(".imgSlide");
            var frm         = new Object();
            frm             = serializeSection($(".divCorte"));
            frm.imgAlto     = frm.imgAlto / imgElement.height();
            frm.imgAncho    = frm.imgAncho / imgElement.width();
            frm.x           = frm.x / imgElement.width();
            frm.y           = frm.y / imgElement.height();
            return frm;
        }
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
            <img src='"+image._strImagen+"' class='fullSize' />\
            <button class='btn btnEliminarImage'>\
                Eliminar\
            </button>\
            <button class='btn btnDeshabilitarSliderImage' estado='"+image._estado+"'>\
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
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
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
                //alert("Ocurrio un error");
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
        });
    }
    function formularioSubir(formData, url,section,imagen) {
        accionAjaxWithImage(url,data, function (data) {
            //console.log("La respuesta del servidor para frm es:",data);
            if (data.estado) {
                alert("Imagen ingresada correctamente");
                imageFromServer             = data.archivos;
                imageFromServer._strImagen  = imagen.src;
                div = getDivImageSlider(imageFromServer);
                if ($(".divImgSlider").find(".noImageSection").length > 0) {
                    $(".divImgSlider").empty().prepend(div);
                } else {
                    $(".divImgSlider").prepend(div);
                }
                section[0].reset();
            } else {
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
            $("#div_carga").hide();
        })
    }
    function iconQuitarValor(tr) {
        frm = serializeSection(tr);
        console.log("formulario a enviar", frm);
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_eliminarValoresConfig", frm, function (data) {
            if (data.estado) {
                tr.remove(); // por el momento que no es dataTable
            } else {
                //console.log("el error es: ", data.error);
                //alert("ocurrio un error");
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
        });
    }
    function validacionAddValor(frm) {
        // validacion 
        var val = new Object();
        val.campos = {
            txtValores: new Array()
        }
        if (frm.txtValores == "") {
            val.campos.txtValores.push("El valor no puede ser vacio");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function btnAddValor(frm,seccion) {
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
                printMessage(seccion, "Valor agregado correctamente", true);
            } else {
                //console.log("obj error es: ", data.error);
                //alert("ocurrio un error");
                /*
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }*/
                if (data.error._mostrar) {
                    printMessage(seccion, data.error.Message, false);
                }
            }
        });
    }
    function frmInstitucional(frm) {
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_actualizarInfoConfig", frm, function (data) {
            console.log("La respuesta del servidor es: ", data);
            if (data.estado) {
                //alert("Actualizado correctamente");
                printMessage($(".divInfoInstitucional"), "Actualizado correctamente", true);
                configuracion = data.configuracion;
                $("#txtAreaVision").val(configuracion._vision);
                $("#txtAreaMision").val(configuracion._mision);
                $("#txtAreaHistoria").val(configuracion._historia);
            } else {
                //console.log("error", data.error);
                //alert("Ocurrio un error");
                if (data.error !== undefined) {
                    alert(data.error.Message);
                } else {
                    alert("Ocurrio un error");
                }
            }
        })
    }