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
                        <textarea class='inputBack form-control txtAreaEditTraduccion' name='txtAreaEditTraduccion'></textarea>\
                    </div>\
                    <div class='normalMode tdTxtTraduccion'>\
                        "+llaveIdioma._traduccion+"\
                    </div>\
                </td>\
                <td>\
                    <div class='editMode hidden' >\
                        <div class='btn-group btn-block'>\
                            <button class='btn btn-default btn-sm btnBack col-lg-6 btnEditMode btnActualizar' >Actualizar</button>\
                            <button class='btn btn-default btn-sm btnBack col-lg-6 btnEditMode btnCancelarEdit' >Cancelar</button>\
                        </div>\
                    </div>\
                    <div class='normalMode'>\
                        <div class='btn-group btn-block'>\
                            <button class='btn btn-default btnBack col-lg-6 btn-sm btnEditarTraduccion'>Editar</button>\
                            <button class='btn btn-default btnBack col-lg-6 btn-sm btEliminarTraduccion'>Eliminar</button>\
                        </div>\
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
    function valAgregarLlave(frm) {
        var estado = false;
        var val = new Object();
        val.campos = {
            cbLlave:            new Array(),
            cbPagina:           new Array(),
            cbIdioma:           new Array(),
            txtAreaTraduccion:  new Array()
        }
        if (frm.idLlave === null || frm.idLlave == -1 ) {
            val.campos.cbLlave.push("Debe seleccionar una llave");
        }
        /*
        if (frm.cbPagina == -1) {
            val.campos.cbPagina.push("Debe seleccionar una pagina");
        }*/
        if (frm.idIdioma == -1) {
            val.campos.cbPagina.push("Debe seleccionar una pagina");
        }
        if (frm.traduccion == "") {
            val.campos.txtAreaTraduccion.push("La traduccion no puede ir vacia");
        }
        val.estado = objArrIsEmpty(val.campos);
        return val;
    }
    function btEliminarTraduccion(tr) {
        var frm = new Object();
        frm.idLlaveIdioma = tr.find(".txtHdIdLlaveIdioma").val();
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_eliminarLlaveIdioma", frm, function (data) {
            if (data.estado) {
                table = tr.parents("table");
                //tr.remove();
                removeDataTable(table, tr);
            } else {
                alert("Ocurrio un error al tratar de eliminar registro");
            }
        });
    }
    function actualizarTrTabla(tr,llaveIdioma) {

    }
    //******************************
    function btnActualizar(tr) {
        frm = serializeSection(tr);
        console.log("Formulario a actualizar",frm);
        actualizarCatalogo(RAIZ + "/GestionIdiomaWebsite/sp_trl_actualizarLlaveIdioma", frm, function (data) {
            console.log("La respuesta del servidor es: ", data);
            if (data.estado) {
                // quitar edit mode 
                controlesEdit(false, tr);
                tr.find(".tdTxtPagina").empty().append(tr.find(".cbEditIdioma option:selected").text());
                console.log(tr.find(".cbEditIdioma").val());
                tr.find(".txtHdIdIdioma").val(tr.find(".cbEditIdioma").val());
                tr.find(".tdTxtTraduccion").empty().append(tr.find(".txtAreaEditTraduccion").val().replace(/\n/g,"<br>"));
                updateAllDataTable($(".tableLlaveIdioma"));
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
        cb = tr.find(".cbEdit");
        cb.chosen({ width: '100%' });
        var frm = new Object();
        frm.idLlaveIdioma = tr.find(".txtHdIdLlaveIdioma").val();
        
        //
        // llenar los controles
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/getObjetosTablita", frm, function (data) {
            console.log("Los objetos de la tablita son", data);
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
                var txtTraduccion = $.trim(tr.find(".tdTxtTraduccion").html());
                
                txtTraduccion = txtTraduccion.replace(/<br>/g, " \n ");
                console.log("para textarea*************************")
                console.log( txtTraduccion);
                console.log("**********")
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
        console.log("de aqui se va a agregar");
        actualizarCatalogo(RAIZ+"/GestionIdiomaWebsite/sp_trl_agregarLlaveIdioma", frm, function (data) {
            if (data.estado) {
                tr = addRowTable(data.llaveIdioma);
                //$(".tbodyTablaTraducciones").append(tr);
                $(".tableLlaveIdioma").dataTable().fnAddTr($(tr)[0]);
                $(".txtAreaTraduccion").val("");
                llenarCbLlaves(data.llaves, $(".cbLlave"));
                printMessage($(".divMensajesAgregar"), "Llave agregada correctamente", true);
            } else {
                alert(data.error); // a partir de hoy los mensajes vendran del servidor
            }
        });
    }
    function cbIdioma(frm) {
        if (frm.idPaginaFront != null && frm.idIdioma != null && frm.idPaginaFront != -1 && frm.idIdioma != -1) {
            cbPagina(frm);
        } else {
            console.log("No paso la validacion");
        }
        
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