
$(document).ready(function () {
    $("form[name='form-distritubor']").validate({
        rules: {
            Code: "required",
            Name: "required",
            Address:"required"
        },
        messages:{
            Code: "Code is required",
            Name: "Name is required",
            Address:"Address is required"
        }
    });
});