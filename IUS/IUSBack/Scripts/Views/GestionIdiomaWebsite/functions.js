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
    function addRowTable(llaveIdioma) {
        var tr = "\
            <tr>\
                <td class='hidden'>\
                    <input value="+llaveIdioma._idLlaveIdioma+" class='txtHdIdLlaveIdioma' name='txtHdIdLlaveIdioma'>\
                    <input value="+llaveIdioma._llave._pagina._idPagina+" class='txtHdIdPagina' name='txtHdIdPagina'>\
                    <input value="+llaveIdioma._llave._idLlave+" class='txtHdIdLlave' name='txtHdIdLlave'>\
                    <input value="+llaveIdioma._idioma._idIdioma+" class='txtHdIdIdioma' name='txtHdIdIdioma'>\
                </td>\
                <td>\
                    <div class='editMode hidden' >\
                        <select class='cbEdit cbEditPagina form-control' name='cbEditPagina'>\
                        </select>\
                    </div>\
                    <div class='normalMode tdTxtPagina'>\
                         "+llaveIdioma._llave._pagina._pagina+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='cbEdit cbEditLlave form-control' name='cbEditLlave'>\
                        </select>\
                    </div>\
                    <div class='normalMode tdTxtLlave'>\
                        "+llaveIdioma._llave._llave+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <select class='cbEdit cbEditIdioma' name='cbEditIdioma'></select>\
                    </div>\
                    <div class='normalMode tdTxtPagina'>\
                        "+llaveIdioma._idioma._idioma+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden'>\
                        <textarea class='form-control txtAreaEditTraduccion' name='txtAreaEditTraduccion'></textarea>\
                    </div>\
                    <div class='normalMode tdTxtTraduccion'>\
                        "+llaveIdioma._traduccion+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden' >\
                        <button class='btn btnEditMode btnActualizar' >Actualizar</button>\
                        <button class='btn btnEditMode btnCancelarEdit' >Cancelar</button>\
                    </div>\
                    <div class='normalMode'>\
                        <button class='btn btnEditarTraduccion'>Editar</button>\
                        <button class='btn btEliminarTraduccion'>Eliminar</button>\
                    </div>\
                </td>\
            </tr>\
            ";
        return tr;
    }
    function llenarCbLlaves(llaves,cbLlaves) {
        options = fillSelectLlave(llaves);
        cbLlaves.empty().append(options);
        resetChosen(cbLlaves);
    }
// actions functions
    function btEliminarTraduccion(tr) {
        var frm = new Object();
        frm.idLlaveIdioma = tr.find(".txtHdIdLlaveIdioma").val();

        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_eliminarLlaveIdioma", frm, function (data) {

            if (data.estado) {
                tr.remove();
            } else {
                alert("Ocurrio un error al tratar de eliminar registro");
            }
        });
    }
    function btnActualizar(tr) {
        frm = serializeSection(tr);
        
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_actualizarLlaveIdioma", frm, function (data) {
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
        frm.idLlaveIdioma = tr.find(".txtHdIdLlaveIdioma").val();
        
        //
        // llenar los controles
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/getObjetosTablita", frm, function (data) {

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
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_agregarLlaveIdioma", frm, function (data) {
            
            if (data.estado) {
                tr = addRowTable(data.llaveIdioma);
                $(".tbodyTablaTraducciones").append(tr);
                $(".txtAreaTraduccion").val("");
                llenarCbLlaves(data.llaves, $(".cbLlave"));
            } else {
                alert(data.error); // a partir de hoy los mensajes vendran del servidor
            }
        });
    }
    function cbIdioma(frm) {
        cbPagina(frm);
    }
    function cbPagina(frm) {
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_getLlaveFromPageAndIdioma", frm, function (data) {
            if (data.estado) {
                options = fillSelectLlave(data.Llaves);
                $(".cbLlave").empty().append(options);
                resetChosen($(".cbLlave"));
            } else {
                alert("ocurrio un error al cargar");
            }
        });
    }