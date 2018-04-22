

$(document).ready(function () {
    $("form[name='form-ff']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            Code: "required",
            Name: "required",
            AddressLine1: "required",
            City: "required",
            State: "required",
            ZipCode: {
                required: true,
                digits: true
            },
            Email: {
                email: true
            },
            Phone: {
                phoneUS: true
            }
        },
    // Specify validation error messages
    messages: {
        Code: "Code is required",
        Name: "Name is required",
        AddressLine1: "AddressLine1 is required",
        City: "City is required",
        State: "State is required",
        ZipCode: "ZipCode is required",
    },
    // Make sure the form is submitted to the destination defined
    // in the "action" attribute of the form when valid
    submitHandler: function (form) {

    }
    });
});
