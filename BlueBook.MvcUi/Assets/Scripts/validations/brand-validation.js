

$(document).ready(function () {
    $("form[name='form-brand']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            Code: "required",
            Name: "required"
        },
        // Specify validation error messages
        messages: {
            Code: "Please enter Code",
            Name: "Please enter Name"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            
        }
    });
});
