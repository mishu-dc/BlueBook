
$(document).ready(function(){
    var serviceUrl = 'http://localhost:59821/api/brands';

    $("#submit").click(function(){
        var method = $('#method').val();
        $("#ul").empty();

        $.ajax({
            type: method,
            url: serviceUrl
        }).done(function (data) {
            $.each(data, function (key, value) {
                $("#ul").append('<li>' + value.Name + '</li>');
            });
        });
    }); 
});