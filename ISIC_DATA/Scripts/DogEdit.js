
    // Disable client date validation.  Browser regional setting could cause problems.
    $(function () {
        $.validator.methods["date"] = function (value, element) { return true; }
    });

$(function ()
{
    $("#newreg").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#hd").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#inbreeding").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#size").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#hair").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#inbreeding").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#hd2").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#comment").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#color").popover({
        trigger: 'hover',
        'placement': 'top'
    });
    $("#photo").popover({
        trigger: 'hover',
        'placement': 'top'
    });
});

// Typeahead for the owner
$(document).ready(function () {
    $('#Owner_typeahead').typeahead({
        name: "Owner",
        remote: '/RegisterDog/FetchOwners?q=%QUERY',
        limit: 8,
        valueKey: "Name",
    }).on("typeahead:selected typeahead:autocompleted",
        function (e, datum) {
            $('#returnOwnerId').val(datum.Id);
        }
    );
});

//MODAL for adding owner
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
// Uploading picture
$(document)
    .on('change', '.btn-file :file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });
$(document).ready(function () {
    $('.btn-file :file').on('fileselect', function (event, numFiles, label) {
        var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }
    });
});

/*
<!-- The template to display files available for upload -->
<script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
            <tr class="template-upload fade">
                <td>
                    <span class="preview"></span>
                </td>
                <td>
                    <p class="name">{%=file.name%}</p>
{% if (file.error) { %}
                        <div><span class="label label-important">Error</span> {%=file.error%}</div>
{% } %}
                </td>
                <td>
                    <p class="size">{%=o.formatFileSize(file.size)%}</p>
{% if (!o.files.error) { %}
                        <div class="progress progress-success progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0"><div class="bar" style="width:0%;"></div></div>
{% } %}
                </td>
                <td>
{% if (!o.files.error && !i && !o.options.autoUpload) { %}
                        <button class="btn btn-primary start">
                            <i class="icon-upload icon-white"></i>
                            <span>Start</span>
                        </button>
{% } %}
{% if (!i) { %}
                        <button class="btn btn-warning cancel">
                            <i class="icon-ban-circle icon-white"></i>
                            <span>Cancel</span>
                        </button>
{% } %}
                </td>
            </tr>
{% } %}
</script>

<!-- The template to display files available for download -->
<script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
            <tr class="template-download fade">
                <td>
                    <span class="preview">
{% if (file.thumbnail_url) { %}
                            <a href="{%=file.url%}" title="{%=file.name%}" data-gallery="gallery" download="{%=file.name%}"><img src="{%=file.thumbnail_url%}"></a>
{% } %}
                    </span>
                </td>
                <td>
                    <p class="name">
                        <a href="{%=file.url%}" title="{%=file.name%}" data-gallery="{%=file.thumbnail_url&&'gallery'%}" download="{%=file.name%}">{%=file.name%}</a>
                    </p>
{% if (file.error) { %}
                        <div><span class="label label-important">Error</span> {%=file.error%}</div>
{% } %}
                </td>
                <td>
                    <span class="size">{%=o.formatFileSize(file.size)%}</span>
                </td>
                <td>
                    <button class="btn btn-danger delete" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}"{% if (file.delete_with_credentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>
                        <i class="icon-trash icon-white"></i>
                        <span>Delete</span>
                    </button>
                    <input type="checkbox" name="delete" value="1" class="toggle">
                </td>
            </tr>
{% } %}
*/
