function btnBusquedaPerfil(frm) {
    actualizarCatalogo(RAIZ + "/RecursosHumanos/sp_rrhh_buscarPersonas", frm, function (data) {
        console.log("La data devuelta es",data);
    })
}