$(function () {


    $("#Forms").validate({
        rules: {
         

            Name: {
                required: true,
                minlength: 3,
                lettersonly: true
            },
            Surname: {
                required: true,
                minlength: 3,
                lettersonly: true
            },
            Email: {
                required: true,
                minlength: 7,
                
            },
                Password: {
                required: true,
                minlength: 4,
                check: true,
            }
        },
        messages: {
            Name: {
                required: "Adı daxil edin",
                minlength: "Minimum simvol sayı 3",
                lettersonly: "Yalnız hərflərdən istifadə edə bilərsiniz"
            },
            Surname: {
                required: "Soyadı daxil edin",
                minlength: "Minimum simvol sayı 3",
                lettersonly: "Yalnız hərflərdən istifadə edə bilərsiniz"
            },
            Email: {
                required: "Email",
                minlength: "Minimum simvol sayı 7",
              
            },
            
            Password: {
                required: "Şifrəni daxil edin",
                minlength: "Minimum simvol sayı 4",
                check: " Yalnız Hərflər rəqəmlər və !\-@._* simvollarından istifadə edə bilərsiniz "
            },

        },

    });
    $.validator.addMethod("check",
        function (value, element) {
            return /^[A-Za-z0-9\d=!\-@._*]+$/.test(value);
        });
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return /^[A-Za-z]+$/.test(value);
    });
});