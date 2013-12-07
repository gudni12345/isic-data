
    $(function () {
        $("#search").popover({
            trigger: 'hover',
            'placement': 'top'
        });
    });

// Auka lyklaborð með sér íslenskum stöfum.   
$(function () {
    var isLayout = ['ÁÆÐÞÖÉÍÓÚÝ' + $.keypad.CLOSE];
    $('#isKeypad').keypad({
        keypadOnly: false, layout: isLayout, showOn: 'button',
        buttonImageOnly: true, buttonImage: '/Images/keypad.png'
    });
});


    //MODAL gluggi sem lýsir nánar leitunar möguleikum
    $(function () {
        $.ajaxSetup({ cache: false });
        $('#info').click(function () {                 // Open the Modal
            $('#dialogContent').load(this.href, function () {
                $('#dialogDiv').modal({
                    backdrop: 'static',
                    keyboard: true
                }, 'show');
                bindForm(this);
            });
            return false;
        });
    });

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {           // if data was saved successfully we close the modal.
                    $('#dialogDiv').modal('hide');
                    //Refresh: location.reload();

                } else {
                    $('#dialogContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

