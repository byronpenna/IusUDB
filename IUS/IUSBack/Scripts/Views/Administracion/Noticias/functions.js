﻿function editarTamanioPersonalizado(valor) {
    div = $(".divTamanioPersonalizado");
    txt = div.find(".txtTamañoImagen");
    div.removeClass("hidden");
    div.find(".ckTamanio").prop("checked", true);
    txt.val(valor);
    txt.prop("disabled", false);
}
function mostrarTamanioPersonalizado() {
    div = $(".divTamanioPersonalizado");
    txt = div.find(".txtTamañoImagen");
    div.removeClass("hidden");
    div.find(".ckTamanio").prop("checked", false);
    txt.val(100);
    txt.prop("disabled", true);
}