function storeCoords(c) {
    $(".x").val(c.x);
    $(".y").val(c.y);
    $(".imgAlto").val(c.h);
    $(".imgAncho").val(c.w);
};
function setFrmCoords(frm, imgQuery) {
    frm.imgAlto = frm.imgAlto / imgQuery.width();
    frm.imgAncho = frm.imgAncho / imgQuery.height();
    frm.x = frm.x / imgQuery.width();
    frm.y = frm.y / imgQuery.height();
    return frm;
}