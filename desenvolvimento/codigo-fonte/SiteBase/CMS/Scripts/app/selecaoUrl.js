function abrirSelecaoUrl(chave) {
    $("#selecaoUrl" + chave).dialog('open');
}

function abrirPopUpSelecaoUrl(inputID) {
    window.open(appUrl_ + 'UserControls/conteudo/SelecaoURL.aspx?destino=input&inputID=' + inputID, 'buscalURL', 'width=800,height=400,location=0,menubar=0,status=1,titlebar=0,toolbar=0,scrollbars=1');
}


function fecharSelecaoUrl(chave) {
    $("#selecaoUrl" + chave).dialog('close');
}

function formatarData() {
    try {
        $('.mask-date-sel-url').mask('99/99/9999');
    } catch (e) { }
}

$(document).ready(function () {
    formatarData();
});

function setarValorInput(inputID, url) {

    if (url.length > 0) {
        url = url.substring(7, url.length);
    }

    this.window.opener.$("#" + inputID).val(url);
    this.window.close();
}