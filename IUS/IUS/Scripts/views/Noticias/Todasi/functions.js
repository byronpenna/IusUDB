function getDivNoticia(noticiaEvento,tituloHover)
{
    var src = "",url="";
    if (noticiaEvento._idTipoEntrada == 1)
    {
        src = RAIZ + "/Noticias/getImageThumbnail/" + noticiaEvento._id;
        url = RAIZ + "/Noticias/Index/" + noticiaEvento._id;
    } else {
        src = $(".txtUrlBack").val() + "/Administracion/getImageThumbnailEvent/" + noticiaEvento._id;
        url = RAIZ + "/Evento/Index/" + noticiaEvento._id;
        
    }
	var div = "\
	<div class='row marginNull notiEve'>\
        <input class='txtHdTipoEntra' type='hidden' value='"+noticiaEvento._idTipoEntrada+"' />\
        <div class='col-lg-6 contentImgNotiEve hoverEventNoti'>\
            <div class='text-tituloNotiEve' style='display:none;'>"+tituloHover+"</div>\
            <div class='divHoverNotiEve' style='display:none;'>\
            </div>\
            <img class='imgNotiEve' src='"+src+"' />\
        </div>\
        <div class='col-lg-6 seccionContenidoNotiEve'>\
            <div class='contenido'>\
                <h4>"+noticiaEvento._titulo+"</h4>\
                <p>\
                    "+noticiaEvento._descripcion+"\
                </p>\
                <div class='divLinkNotiEve'>\
                    <a class='aLeerMas' href='"+url+"'>Leer más>>></a>\
                </div>\
            </div>\
        </div>\
    </div>";
    return div;
}
function spFiltro(frm)
{
    actualizarCatalogo(RAIZ + "/Noticias/getRecursosTodas", frm, function (data) {
        console.log("La data es para recursos todos: ", data);
        if(data.estado){
        	var divPut =""
        	if(data.noticiasEventos !== undefined && data.noticiasEventos != null){
        		$.each(data.noticiasEventos,function(i,noticiaEvento){
        			divPut += getDivNoticia(noticiaEvento);
        		})
        	}
        	$(".divNotiEvento").empty().append(divPut);
        }
        /*if(data.estado)
        {

        }*/
    })
}