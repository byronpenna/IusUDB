// generics functions
    function fillSelectLlave(llaves) {
        options = "";
        if (!(llaves === null)) {
            $.each(llaves, function (i, val) {
                options += "<option value='" + val._idLlave + "'>" + val._llave+ "</option>"
            });
        } else {
            options = "<option value='-1' disabled >No hay llaves disponibles</option>";
        }
        return options;
    }
// actions functions
    /*
    function btnEditarTraduccion(tr) {
        frm = serializeToJson(tr.find("input,select").serializeArray());
        console.log("")
    }*/
    function btnCancelarEdit(tr) {
        controlesEdit(false, tr);
    }
    function btnEditarTraduccion(tr) {
        controlesEdit(true, tr);
        var frm = new Object();
        $(".cbEdit").chosen();
        frm.idPagina = tr.find(".txtHdIdPagina").val();
        console.log("El formulario a enviar es", frm);
        // llenar los controles

    }
    function btnAgregarLlave(frm) {
        /*actualizarCatalogo("/GestionIdiomaWebsite/sp_trl_getLlaveFromPage", frm, function (data) {
            if (data.estado) {

            } else {
                alert(data.mensaje); // a partir de hoy los mensajes vendran del servidor
            }
        });*/
    }
    function cbPagina(frm) {
        actualizarCatalogo("/GestionIdiomaWebsite/sp_trl_getLlaveFromPage", frm, function (data) {
            console.log("la data del servidor es:",data);
            if (data.estado) {
                options = fillSelectLlave(data.Llaves);
                $(".cbLlave").empty().append(options);
                resetChosen($(".cbLlave"));
            } else {
                alert("ocurrio un error al cargar");
            }
        });
    }