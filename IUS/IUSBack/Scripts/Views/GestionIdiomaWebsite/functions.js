// generics functions
    function fillSelectLlave(llaves,selected) {
        options = "";
        if (!(llaves === null)) {
            $.each(llaves, function (i, val) {
                txtActive = "";
                if (typeof (selected) != "undefined" && val._idLlave == selected) {
                    txtActive = "selected";
                }
                options += "<option value='" + val._idLlave + "' " + txtActive + ">" + val._llave + "</option>"
            });
        } else {
            options = "<option value='-1' disabled >No hay llaves disponibles</option>";
        }
        return options;
    }
    function fillSelectPagina(paginas,selected) {
        options = "";
        if (!(paginas === null)) {
            $.each(paginas, function (i,val) {
                txtActive = "";
                if (typeof (selected) != "undefined" && paginas._idPagina == selected) {
                    txtActive = "selected";
                }
                options += "<option value='" + val._idPagina + "'>" + val._pagina + "</option>"
            })
        } else {
            options = "<option value='-1'>No hay idiomas disponibles</option>"
        }
        return options;
    }
    function fillSelectIdioma(idiomas,selected) {
        options = "";
        if (!(idiomas === null)) {
            $.each(idiomas, function (i,val) {
                txtActive = "";
                if (typeof (selected) != "undefined" && val._idIdioma == selected) {
                    txtActive = "selected";
                }
                options += "<option value='" + val._idIdioma + "'>" + val._idioma + "</option>";
            });
        } else {
            options = "<option value='-1' disabled>No hay idiomas disponibles</option>";
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
        frm.idPagina = tr.find(".txtHdIdPagina").val();
        console.log("El formulario a enviar es", frm);
        //
        // llenar los controles
        actualizarCatalogo("/GestionIdiomaWebsite/getObjetosTablita", frm, function (data) {
            console.log("respuesta del servidor",data);
            if (data.estado) {
                optionsLlaves = fillSelectLlave(data.llaves, tr.find(".txtHdIdLlave").val());
                optionsIdioma = fillSelectIdioma(data.idiomas, tr.find(".txtHdIdIdioma").val());
                optionsPagina = fillSelectPagina(data.paginas, tr.find(".txtHdIdPagina").val());
                console.log("options pagina es:", optionsPagina);
                tr.find(".cbEditLlave").empty().append(optionsLlaves);
                tr.find(".cbEditIdioma").empty().append(optionsIdioma);
                tr.find(".cbEditPagina").empty().append(optionsPagina);
                $(".cbEdit").chosen({ width: '100%' });
                //resetChosen($(".cbEditLlave"));
            }
        });
        
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