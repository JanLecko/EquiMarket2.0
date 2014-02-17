

//$(document).ready(function () {
//    $('*[data-autocomplete-url]')
//        .each(function () {
//            $(this).autocomplete({
//                dataType:"json",
//                source: $(this).data("autocomplete-url"),
//                messages: {
//                    noResults: '',
//                    results: function() {}
//                },
//                response: function (event, ui) {
//                    $.map(ui.content, function (breed) {
//                        ui.content.push({ label: breed.TitleSK, value: breed.ID });
//                    });
//                },
//                change: function (event, ui) {
//                    if (!ui.item) {
//                        this.value = '';
//                    } else {
                        
//                    }
//                }
//            });
//        });
//});


$(document).ready(function () {
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
});

