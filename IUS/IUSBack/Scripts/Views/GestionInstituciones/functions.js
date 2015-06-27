// genericas 
    function getTrInstituciones(institucion) {
        tr = "\
            <tr>\
                <td>\
                    <input class='txtHdIdInstitucion' name='txtHdIdInstitucion' value='"+institucion.idInstitucion+"'>\
                </td>\
                <td>"+institucion._nombre+"</td>\
                <td>"+institucion._direccion+"</td>\
                <td>"+institucion._pais._pais+"</td>\
                <td>\
                    <button class='btn'>Editar</button>\
                    <button class='btn btnDeleteInstitucion'>Eliminar</button>\
                </td>\
            </tr>\
        ";
        return tr;
    }
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
                tr = getTrInstituciones(data.institucion);
                $(".tbInstituciones").dataTable().fnAddTr($(tr)[0]);
            }
        })
    }