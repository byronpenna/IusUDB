function getArchivos(frm) {
    actualizarCatalogo(RAIZ + "/Repositorio/ajax_getArchivos", frm, function (data) {
        console.log("La data devuelta es:", data);
        if (data.estado) {
            var tr = "";
            if (data.archivos !== undefined && data.archivos != null && data.archivos.length > 0) {
                $.each(data.archivos, function (i,archivo) {
                    tr += getTrRepositorio(archivo);
                })
            } else {
                tr = getTrNull();
            }
            $(".tablaFichero tbody").empty().append(tr);
        }
    })
}
function getTrNull(){
    var tr = "\
        <tr>\
            <td colspan='5'>No hay archivo</td>\
        </tr>\
    ";
    return tr;
}
function getTrRepositorio(archivo) {
    var tr = "\
        <tr>\
            <td>El Salvador</td>\
            <td>Universidad Don Bosco</td>\
            <td>" + archivo._nombre + "." + archivo._archivoUsuario._extension._extension + "</td>\
            <td>Pdf</td>\
            <td>\
                <a href='#'>\
                    <img class='imgDownload' src='" + RAIZ + "/Content/images/views/Repositorio/descarga.png' />\
                </a>\
            </td>\
        </tr>\
    ";
    return tr;
}