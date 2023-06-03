
$.fn.numericInputExample = function () {
    'use strict';
    let table = $(this);

    table.find('td').on('change', function (evt) {
        let cell = $(this),
            column = cell.index();

        table.find('tbody tr').each(function () {
            let row = $(this);
        });

        if (!cell.hasClass('validateCol')) {
            return false;
        }
        
    }).on('validate', function (evt, value) {
        let cell = $(this);

        if (cell.hasClass('validateCol')) {
            return value === "" || (!isNaN(parseFloat(value)) && isFinite(value));
        }
        return false;
    });

    return this;
};
