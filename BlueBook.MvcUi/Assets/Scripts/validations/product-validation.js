$(document).ready(function () {
    $("form[name='form-product']").validate({
        rules: {
            Code: "required",
            Name: "required",
            Brand: "required",
            Price: {
                required: true,
                number: true
            }
        },
        messages: {
            Code: 'Code is required',
            Name: 'Name is required',
            Brand: 'Brand is required',
            Price:'Valid price is required'
        }
    });
});