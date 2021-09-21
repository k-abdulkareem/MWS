var el = document.getElementsByClassName("SignUpBtn");

for (var i = 0; i < el.length; i++) {
    el[i].addEventListener('click', function (e) {
       $('div#SignUpContent').empty();
        e.preventDefault();
        var operation = "SignUp";
        var SignUp = {
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            Email: $("#Email").val(),
            Password: $("#Password").val(),
            ConfirmPassword: $("#ConfirmPassword").val()
        };
        var SignIn = {
            Email: $("#Email").val(),
            Password: $("#Password").val(),
        };

        var ViewModel = { SignIniewModel: SignIn, SignUpViewModel: SignUp }
        var dataobj = { model: ViewModel, operation: "SignUp", ReturnUrl = $("#ReturnUrl").val() };
        $.ajax({
            //url: $('#SignUpContent').data('url'),
            //url: "Home/Index",
            type: 'POST',
            dataType: "html",
            data: dataobj,

            success: function (response, status, xhr) {
                var ct = xhr.getResponseHeader("content-type") || "";
                if (ct.indexOf('html') > -1) {
                    console.log("success");
                    $('div#SignUpContent').html(response);
                }
                if (ct.indexOf('json') > -1) {
                    window.location.href = JSON.parse(response);
                }
            },
          
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);


                $('#SignUpContent').empty();
            }
        });
    });

}
