/********************************************************************************
FUNÇÕES USADAS EM TODO O CMS
********************************************************************************/

// msg padrao para o componente jquery bloack ui
var _msgBlockUI = '<div class="carregandoBlockUI"></div>';

//desabilita o botao de submit ao clicar
function disableSubmitButton(obj, grupo) {
    if (grupo == null) grupo = '';

    if (typeof (Page_ClientValidate) == 'function') {
        if (Page_ClientValidate(grupo) == false) {
            return false;
        }
        obj.disabled = true;
    }
}

// Colocar formatação de data e calendário do jquery UI no campo
// É necessário que o plugin do jquery mask esteja inserido na página
function campoData(campo) {
    jQuery(campo).datepicker($.datepicker.regional["pt-BR"]);
    jQuery(campo).mask("99/99/9999");
}

//Coloca limite de caracteres em um campo. 
//Precisa que o script do plugin de maxlenght esteja inserido na página
// campo = id do campo onde será aplicado
// objRetorno = div/span onde será exibido os caracteres restantes
function limiteCaracteres(campo, objRetorno) {
    jQuery(campo).maxlength({
        'feedback': objRetorno
    });
}


// Função para abrir a modal do jquery UI
function criarModal(selector, w, h, destroy, resizable) {

    if (destroy) {
        $(selector).dialog({
            width: w,
            height: h,
            modal: true,
            resizable: resizable,
            close: function (event, ui) {
                $(selector).dialog("destroy");
            }
        });
    } else {
        $(selector).dialog({
            width: w,
            height: h,
            modal: true,
            resizable: resizable
        });
    }
}

// Função para dar scrool até o topo da página quando houver erro para que o usuário possa ver a mensagem de erro.
function toTheTop() {
    $('html, body').animate({ scrollTop: 0 }, 300);
}


/// Função para permitir que o usuário digite somente numero em um textbox. Recebe a classe CSS do campo
function soNumerosInteiros(classeCSS) {
    $("." + classeCSS).keydown(function (event) {
        if (event.shiftKey) {
            event.preventDefault();
        }

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (event.keyCode < 95) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    event.preventDefault();
                }
            }
            else {
                if (event.keyCode < 96 || event.keyCode > 105) {
                    event.preventDefault();
                }
            }
        }
    });
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// msg de erro flutuante
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    $(window).scroll(function () {
        offset = $(document).scrollTop();
        $(".dv-msg-erro").css({ top: offset });
    });

    // ao clicar em fechar o erro
    $(".dv-msg-erro .close").bind("click", function () {
        $(".dv-msg-erro").hide();
        $(".dv-msg-erro .mensagem").html("");
    });
});

function showMsgErro(erro) {
    // $(".dv-msg-erro").animate({ top: $(document).scrollTop() }, { duration: 0, queue: false });
    $(".dv-msg-erro .mensagem").html(erro);
    $(".dv-msg-erro").css({ top: $(document).scrollTop() });
    $(".dv-msg-erro").removeClass("dv-hide").show();
}

// copia o texto de um campo para outro
// somente se o destino estiver em branco
function copiarTexto(from_, to_, validarCampoVazio, msgCampoVazio) {
    $(from_).change(function () {
        if (validarCampoVazio) {
            if ($(to_).attr("value").length > 0) {
                if (confirm(msgCampoVazio)) {
                    $(to_).attr("value", $(this).attr("value"));
                }
            } else {
                $(to_).attr("value", $(this).attr("value"));
            }
        } else {
            $(to_).attr("value", $(this).attr("value"));
        }
    });
}