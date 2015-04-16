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
    function btEliminarTraduccion(tr) {
        var frm = new Object();
        frm.idLlaveIdioma = tr.find(".txtHdIdLlaveIdioma").val();
        console.log("el formulario a enviar es",frm);
        actualizarCatalogo("/GestionIdiomaWebsite/sp_trl_eliminarLlaveIdioma", frm, function (data) {

        });
    }
    function btnActualizar(tr) {
        frm = serializeSection(tr);
        console.log("formulario a enviar es:", frm);
        actualizarCatalogo("/GestionIdiomaWebsite/sp_trl_actualizarLlaveIdioma", frm, function (data) {
            if (data.estado) {
                // quitar edit mode 
            } else {
                alert("ocurrio un error al intentar actualizar");
            }
        });
    }
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
            console.log("respuesta del servidor", data);
            var cb = new Object(); var selected = new Object();
            cb.llave =  tr.find(".cbEditLlave");   cb.idioma = tr.find(".cbEditIdioma");
            cb.pagina = tr.find(".cbEditPagina");
            if (data.estado) {
                // selected data ;
                selected.llave  = tr.find(".txtHdIdLlave").val();    selected.idioma = tr.find(".txtHdIdIdioma").val();
                selected.pagina = tr.find(".txtHdIdPagina").val();
                // obteniendo informacion
                optionsLlaves = fillSelectLlave(data.llaves, selected.llave);
                optionsIdioma = fillSelectIdioma(data.idiomas, selected.idioma);
                optionsPagina = fillSelectPagina(data.paginas, selected.pagina);
                txtTraduccion = $.trim(tr.find(".tdTxtTraduccion").text());
                // llenando para editar
                cb.llave.empty().append(optionsLlaves);
                cb.idioma.empty().append(optionsIdioma);
                cb.pagina.empty().append(optionsPagina);
                tr.find(".txtAreaEditTraduccion").empty().append(txtTraduccion);
                // reseteando chosen 
                resetChosenWithSelectedVal(cb.llave,selected.llave);
                resetChosenWithSelectedVal(cb.pagina,selected.pagina);
                resetChosenWithSelectedVal(cb.idioma,selected.idioma);
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