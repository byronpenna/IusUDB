// tabla 
    function getTrValor(valor) {
        tr = "\
            <tr>\
                <td>"+valor._valor+"</td>\
                <td>\
                    <i class='fa fa-times pointer iconQuitarValor'></i>\
                </td>\
            </tr>\
        ";
        return tr;
    }
// acciones scripts 
    function btnAddValor(frm) {
        
        actualizarCatalogo(RAIZ + "/ConfiguracionWebsite/sp_adminfe_agregarValoresConfig", frm, function (data) {
            console.log("la respuesta es: ", data);
            if (data.estado) {
                tbody = $(".tableValores").find("tbody");
                newTr = getTrValor(data.valor);
                if (tbody.find(".trNoValor").length == 0) {
                    tbody.prepend(newTr);
                } else {
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