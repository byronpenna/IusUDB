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
// acciones scripts 
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