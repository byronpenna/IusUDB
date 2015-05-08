$(document).ready(function () {
    // plugins 
        // tabs 
            $('#horizontalTab').responsiveTabs();
    // eventos 
        // submit
            $(document).on("submit", "#frmInstitucional", function (e) {
                e.preventDefault();
                frm = serializeToJson($(this).serializeArray());
                console.log("Formulario a enviar es: ", frm);
                frmInstitucional(frm);
            });
            $(document).on("submit","#frm",function(e){
                e.preventDefault();
                var file = $("#file1").get();
                console.log("file es:", file);
                /*
                $.ajax({
                    type: 'POST',
                    url: $("#frm").attr("action"),
                    form: file,
                    dataType: "application/json",
                    contentType: "application/octet-stream"
                });*/
                var formData = new FormData();
                var totalFiles = document.getElementById("file1").files.length;
                console.log("total files s:", totalFiles);
                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementById("file1").files[i];
                    formData.append("FileUpload", file);
                }
                console.log("form data es",formData)
                $.ajax({
                    type: "POST",
                    url: $(this).attr("action"),
                    form: formData,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        alert('succes!!');
                    },
                    error: function (error) {
                        alert("errror");
                    }
                });
                
            });
        // click
            $(document).on("click", ".iconQuitarValor", function () {
                var x = confirm("¿Esta seguro de quitar valor?");
                tr = $(this).parents("tr");
                if (x) {
                    iconQuitarValor(tr);
                }
                
            });
            $(document).on("click", ".btnAddValor", function () {
                frmSection = $(this).parents(".agregarValorSection");
                frm = serializeSection(frmSection);
                console.log("formulario a agregar", frm);
                btnAddValor(frm);
            })
});