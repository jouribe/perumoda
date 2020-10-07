var lastSMS = 0;
var smsValid = false,
    dniValid = false,
    emailValid = false;

function Volver() {
    $("#form-total").steps("previous");
    lastSMS = 0;
    smsValid = false;
    dniValid = false;
    emailValid = false;

    $("#celular").focus();
    $("a[href$='next']").show();
}

function CodigoSMS() {
    enviarSMS($("#nombres").val(), $("#celular").val(), true);
}

function mostrarError(control, mensaje) {
    $("#" + control).show();
    $("#" + control).html(mensaje);
}

function ocultarErrores() {
    $("#lbNombres").hide();
    $("#lbApellidos").hide();
    $("#lbDNI").hide();
    $("#lbCelular").hide();
    $("#lbEmail").hide();
    $("#lbTYC").hide();
    $("#lbTipoDoc").hide();
    $("#lbContacto").hide();
    $("#lbPais").hide();
    $("#lbRegion").hide();
    $("#lbBloques").hide();
    $("#lbGrupo").hide();
}

function verifL(n) {
    permitidos = /[^áàäâªÁÀÂÄéèëêÉÈÊËíìïîÍÌÏÎóòöôÓÒÖÔ'úùüûÚÙÛÜabcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ .]/;
    if (permitidos.test(n.value.slice(n.value.length - 1))) {
        //n.value = n.value.slice(0, n.value.length - 1)
        n.value = "";
        //n.focus();
    }
}

function verifTelefono(n) {
    permitidos = /[^áàäâªÁÀÂÄéèëêÉÈÊËíìïîÍÌÏÎóòöôÓÒÖÔ'úùüûÚÙÛÜabcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ .]/;
    if (!permitidos.test(n.value.slice(n.value.length - 1))) {
        //n.value = n.value.slice(0, n.value.length - 1)
        n.value = "";
        //n.focus();
    }
    if (n.value.length > 15) {
        n.value = n.value.slice(0, 15);
    }
    if (n.value == "111111111" || n.value == "222222222" || n.value == "333333333" || n.value == "444444444" || n.value == "555555555" || n.value == "666666666" || n.value == "777777777" || n.value == "888888888" || n.value == "999999999" || n.value == "000000000" || n.value == "123456789" || n.value == "987654321") {
        n.value = "";
    }
}

function verifDNI(n) {
    permitidos = /[^áàäâªÁÀÂÄéèëêÉÈÊËíìïîÍÌÏÎóòöôÓÒÖÔ'úùüûÚÙÛÜabcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ .]/;
    if (!permitidos.test(n.value.slice(n.value.length - 1))) {
        //n.value = n.value.slice(0, n.value.length - 1)
        n.value = "";
        // n.focus();
    }
    if (n.value.length > 15) {
        n.value = n.value.slice(0, 15);
    }
}

function verifEmail(n) {
    if (/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(n)) {
        return true;
    } else {
        return false;
    }
}

function validarDNI(dni, nombre, apellido, email, celular, origen, tipoDoc, canalO) {

    $.ajax({
            url: 'assets/php/lib/servicios.php',
            type: 'POST',
            data: {
                'dni': dni,
                'nombres': nombre,
                'apellidos': apellido,
                'origen': origen,
                'tipoDoc': tipoDoc,
                'canalO': canalO,
                'email': email,
                'celular': celular,
                'utm_source': findGetParameter("utm_source"),
                'utm_medium': findGetParameter("utm_medium"),
                'utm_campaign': findGetParameter("utm_campaign"),
                'utm_term': findGetParameter("utm_term"),
                'utm_content': findGetParameter("utm_content")
            },
        })
        .done(function(respuesta) {
            //console.log(respuesta);
            let resultado = JSON.parse(respuesta);
            if (resultado.status === 1) {
                dniValid = true;
                //validarEmail(nombre, email, celular);				
                window.open('gracias.html', '_self')
            } else {
                dniValid = false;
                mostrarError("lbDNI", resultado.mensaje);
                $("a[href$='next']").show();
                return false;
            }
        })
        .fail(function() {
            //console.log("error");
            dniValid = false;
            $("a[href$='next']").show();
            return false
        });
}


function findGetParameter(parameterName) {
    var result = null,
        tmp = [];
    location.search
        .substr(1)
        .split("&")
        .forEach(function(item) {
            tmp = item.split("=");
            if (tmp[0] === parameterName) result = decodeURIComponent(tmp[1]);
        });
    return result;
}