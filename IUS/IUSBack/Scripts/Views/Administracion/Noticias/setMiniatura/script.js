$(document).ready(function () {
    // eventos 
        // submit
            $(document).on("submit", "#frmMiniatura", function (e) {
                var files = $("#flMiniatura")[0].files;
                frm = serializeToJson($(this).serializeArray());
                data = getObjFormData(files,frm);
                e.preventDefault();
                //console.log("la data e enviar es ",data);
                frmMiniatura(data, $(this).attr("action"));
            })
})