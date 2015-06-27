// acciones script
    function btnDeleteInstitucion(frm,tr) {
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_deleteInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {
                table = $(".tbInstituciones");
                removeDataTable(table, tr);
            }
        });
    }
    function btnAddInstitucion(frm) {
        actualizarCatalogo(RAIZ + "/GestionInstituciones/sp_frontui_insertInstitucion", frm, function (data) {
            console.log(data);
            if (data.estado) {

            }
        })
    }