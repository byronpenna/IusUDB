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
    function cbPagina(frm) {
        console.log("la data a enviar al servidor es:", frm);
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