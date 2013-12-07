  
    $(document).ready(function () {
        $("#typeaheadFather").typeahead({
            name: "Father", 
            remote: 'Registerdog/FetchFathers?q=%QUERY',
            limit: 3,
            valueKey: "Reg",
            template: ['<p class="repo-reg">{{Reg}}</p>', '<p class="repo-name">{{Name}}</p>',
            ].join(''),
            engine: Hogan
        }).on("typeahead:selected typeahead:autocompleted",
            function (e, datum) {
                $('#returnFatherId').val(datum.Id);
                $('#fatherName').val(datum.Name);

            }
        );
    });

$(document).ready(function () {
    $("#typeaheadMother").typeahead({
        name: "Mother",
        remote: 'Registerdog/FetchMothers?q=%QUERY',
        limit: 3,
        valueKey: "Reg",
        template: ['<p class="repo-reg">{{Reg}}</p>', '<p class="repo-name">{{Name}}</p>',].join(''),
        engine: Hogan
    }).on("typeahead:selected typeahead:autocompleted",
        function (e, datum) {
            $('#returnMotherId').val(datum.Id);
            $('#motherName').val(datum.Name);

        }
    );
});


//MODAL for Inbreeding info
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



