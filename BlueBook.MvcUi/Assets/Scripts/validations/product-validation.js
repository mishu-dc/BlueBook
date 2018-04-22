$(document).ready(function () {
    $("form[name='form-product']").validate({
        rules: {
            Code: "required",
            Name: "required",
            Brand: "required",
            Price: "required"
        },
        messages: {
            Code: 'Code is required',
            Name: 'Name is required',
            Brand: 'Brand is required',
            Price:'Price is required'
        }
    });

    $('.numbersOnly').keyup(function () {
        if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        }
    });
});