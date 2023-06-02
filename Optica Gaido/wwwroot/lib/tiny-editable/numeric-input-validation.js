
$.fn.numericInputExample = function () {
    'use strict';
    let table = $(this);

    table.find('td').on('change', function (evt) {
        let cell = $(this),
            column = cell.index();


        table.find('tbody tr').each(function () {
            let row = $(this);
        });

        if (column === 0 || column === 1) {
            return false; // changes can be rejected
        }
    }).on('validate', function (evt, value) {
        let cell = $(this),
            column = cell.index();

        if (column >= 2) {
            return !isNaN(parseFloat(value)) && isFinite(value);
        }
    });

    return this;
};
