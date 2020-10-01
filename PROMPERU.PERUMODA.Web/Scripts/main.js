// grecaptcha.ready(function() {
//   grecaptcha.execute('6LfD37cZAAAAAJG3g0pL0t1DdmSAlny3DP2_EbJG', {action: 'homepage'}).then(function(token) {
//     console.log( token );
//     $('#btnParticipar').show();
//   });
// });

$(document).ready(function () {
    $('.tabs').tabs();
    $('select').formSelect();

    $('#next').click(function (e) {
        e.preventDefault();
        $('html,body').animate({
                scrollTop: $(".inicio").offset().top
            },
            'slow');
    });

    $('.fade').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: false,
        arrows: false,
        infinite: true,
        autoplay: true,
        autoplaySpeed: 4000,
        speed: 500,
        infinite: true,
        fade: true,
        cssEase: 'linear'
    });


    $('#tycAbrir').click(function () {
        $('#tlegales').fadeIn(300);
    });
    $('#tycAbrir').click(function () {
        $('#tlegalesEng').fadeIn(300);
    });
    $('#close img').click(function () {
        $('#tlegales, #tlegalesEng').fadeOut(300);
        console.log('click')
    });
    $('.suscribeAbrir').click(function () {
        $('#suscribe').fadeIn(300);
    });
    $('#close img').click(function () {
        $('#suscribe').fadeOut(300);
        console.log('click')
    });

    $("select.diasSelect").change(function () {
        $(this).find("option:selected").each(function () {
            var optionValue = $(this).attr("value");
            if (optionValue) {
                $(".table-content").not("#" + optionValue).removeClass('active').hide();
                $("#" + optionValue).addClass('active').show();
            } else {
                $(".table-content").removeClass('active').hide();
            }
        });
    }).change();

});

$(function () {
    $('#btnRegistrarUsuario').click(function () {

        let atLeastOneIsChecked = false;

        let nombre = $("#txtNombres").val();
        let apellido = $("#txtApellidos").val();
        let email = $("#txtCorreoElectronico").val();
        let tipoDoc = $("#ddlTipoDocumento").val();
        let dni = $("#txtNumeroDocumento").val();
        let cel = $("#txtNumeroTelefono").val();
        let pais = $("#ddlPais").val();
        let region = $("#txtRegion").val();
        let tipoUsuario = $("#ddlTipoUsuario").val();

        $("[name=Bloques]").each(function () {
            if ($(this).is(":checked")) {
                atLeastOneIsChecked = true;

                return false;
            }
        });

        let tyc = $("#chkTerminosCondiciones").prop("checked");
       

        if (nombre.length === 0) {
            mostrarError("lbNombres", "Ingresa tus nombres.");
            return false;
        } else if (apellido.length === 0) {
            mostrarError("lbApellidos", "Ingresa tus apellidos.");
            return false;
        } else if (!verifEmail(email)) {
            mostrarError("lbEmail", "Ingresa un email válido.");
            return false;
        } else if (cel.length !== 9 || cel.slice(0, 1) !== "9") {
            mostrarError("lbCelular", "Ingresa un celular válido.");
            return false;
        } else if (tipoDoc === "") {
            mostrarError("lbTipoDoc", "Elige el tipo de documento.");
            return false;
        } else if ((tipoDoc === "DNI" && dni.length !== 8) || (tipoDoc !== "DNI" && dni.length < 8)) {
            mostrarError("lbDNI", "Ingresa un número de documento válido.");
            return false;
        } else if (pais === "") {
            mostrarError("lbPais", "Seleccione su país");
            return false;
        } else if (region.length === 0) {
            mostrarError("lbRegion", "Ingrese su región")
            return false;
        } else if (tipoUsuario === "") {
            mostrarError("lbGrupo", "Seleccione tipo de usuario");
            return false;
        } else if (!tyc) {
            mostrarError("lbTYC", "Acepta los términos y condiciones.");
            return false;
        } else if (!atLeastOneIsChecked) {
            mostrarError("lbBloques", "Seleccione al menos un bloque.")
            return false;
        } else {
            try {
                $("#btnRegistrarUsuario").hide();
                $('#frmRegistro').submit();
            } catch (ex) {
                return false;
            }
        }
    });
});