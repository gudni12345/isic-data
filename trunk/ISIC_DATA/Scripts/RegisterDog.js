
//$("regform").validate().form();

//$.validator.setDefaults({ ignore: null });

// Added client validation. Required in the regform. Litter model doesn't have the requirement for those fields for import purpose.
/*
$('#regform').validate({
    ignore: [],
    onkeyup: false,
    rules: {
        dateOfBirth: { required: true , class: "input-validation-error" },
        returnBreederId: { required: true, class: "input-validation-error" }
    },
    messages: {
        dateOfBirth: { required: "Date of Birth is required..." },
        returnBreederId: { required: "Breeder is required..." }
    }
});
*/




//MODAL for adding breeder
$(function () {
    $.ajaxSetup({ cache: false });
    $('#btnCreate').click(function () {                 // Open the Modal
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


// DatePicker
$(document).ready(function () {
    $.validator.methods["date"] = function (value, element) { return true; }
    $(".datePicker").datepicker({
        format: 'dd/mm/yyyy',
        todayBtn: true,
        orientation: "top auto",
        autoclose: true
    });
});




// Type-a-head for Breeder
$(document).ready(function () {
    $('#Breeder_typeahead').typeahead({
        name: "Breeder",
        remote: 'Registerdog/FetchBreeders?q=%QUERY',
        limit: 5,
        valueKey: "Name",
    }).on("typeahead:selected typeahead:autocompleted",
        function (e, datum) {
            $('#returnBreederId').val(datum.Id);
        }
    );
});
// Type-a-head for Father
$(document).ready(function () {
    $("#typeaheadFather").typeahead({
        name: "Father",
        remote: 'Registerdog/FetchFathers?q=%QUERY',
        limit: 5,
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
// Type-a-head for Mother
$(document).ready(function () {
    $("#typeaheadMother").typeahead({
        name: "Mother",
        remote: 'Registerdog/FetchMothers?q=%QUERY',
        limit: 5,
        valueKey: "Reg",
        template: ['<p class="repo-reg">{{Reg}}</p>', '<p class="repo-name">{{Name}}</p>', ].join(''),
        engine: Hogan
    }).on("typeahead:selected typeahead:autocompleted",
        function (e, datum) {
            $('#returnMotherId').val(datum.Id);
            $('#motherName').val(datum.Name);

        }
    );
});





var numOfDogs = 1;

function createViewModel() {

    var createPosition = function () {
        return {
            name: ko.observable(),
            reg: ko.observable(),
            sex: ko.observable(),
            colorId: ko.observable(),
        };
    };

    var addPosition = function () {
        if (numOfDogs < 15) {
            positions.push(createPosition());
            numOfDogs++;
        }
    };

    var removePosition = function () {
        if (numOfDogs > 1) {
            positions.pop();
            numOfDogs = numOfDogs - 1;
        }
    };

    var birth = ko.observable();
    var fatherName = ko.observable();
    var positions = ko.observableArray([createPosition()]);

    return {
        birth: birth,
        fatherName: fatherName,
        positions: positions,
        addPosition: addPosition,
        removePosition: removePosition
    };

}

$(document).ready(function () {
    var viewModel = createViewModel();
    ko.applyBindings(viewModel);
});
