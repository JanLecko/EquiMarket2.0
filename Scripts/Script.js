$(document).ready(function () {

    // autocompleete
    $('*[data-autocomplete-url]')
        .each(function () {
            $(this).autocomplete({
                source: function (request, response) {
                    // define a function to call your Action (assuming UserController)
                    $.ajax({
                        url: $(this)[0].element.data("autocomplete-url"),
                        type: "POST",
                        dataType: "json",
                        // query will be the param used by your action method
                        data: { term: request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.TitleSK, value: item.ID };
                            }))
                        }
                    });
                },
                minLength: 1, // require at least one character from the user
                select: function (event, ui) {
                    //var name = $(this).attr("Name")
                    //$("input[name='" + name + "', type='hidden']").val(ui.item.value)
                    //$(this).val(ui.item.label)

                    var hiddenId = $(this).attr("data-autocomplete-valueelementid");
                    $("#" + hiddenId).val(ui.item.value);
                    $(this).val(ui.item.label);

                    return false;
                },
                messages: {
                    noResults: '',
                    results: function() {}
                },
                change: function (event, ui) {
                    var hiddenId = $(this).attr("data-autocomplete-valueelementid");
                    $("#" + hiddenId).val(ui.item.value);
                    $(this).val(ui.item.label);
                    return false;
                }
            }); 
        })

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    // jQuery dialog

    var dialogOptions = {
        modal: true,
        height: 700,
        width: 1000,
        draggable: false,
        resizable: false,
        buttons: {
            Close: function () {
                $(this).dialog("close");
            }
        }
    };

    $('.dialogOpen').click(function () {
        var $dialog = $('#' + $(this).attr('id') + '_map');// dialog ID based on click element ID
        //var dialogType = $(this).attr('dialogType');
        $dialog.dialog(dialogOptions); // pass the appropriate options object to the dialog call dialogOptions[dialogType]
        $(".ui-dialog-titlebar").hide();
        return false;
    });
});

