(function ($) {

    function valueChange(e) {
        console.log('display-if: valueChange', e.currentTarget.name);
        const target = $(e.target);
        var name = target.attr('data-display-if-toggle');

        // If the input type is radio we need to hide all elements connected to each other radios in that groups.
        if (target.attr('type') === 'radio') {

            const radioGroup = target.attr('name');
            $(`input[name="${radioGroup}"][data-display-if-toggle]:not(:checked)`).each((i, e) => {
                const radio = $(e);
                const toggleName = radio.attr('data-display-if-toggle');
                let toggleValue = radio.val();
                if (!radio.is(':checked')) {
                    toggleValue = '';
                }
                toggleSwitchElements(toggleName, toggleValue);
            });

            const checked = $(`input[name="${radioGroup}"][data-display-if-toggle]:checked`);
            if (checked.length) {
                const radio = $(checked[0]);
                const toggleName = radio.attr('data-display-if-toggle');
                const toggleValue = radio.val();
                toggleSwitchElements(toggleName, toggleValue);
            }

        }
        else if (target.attr('type') === 'checkbox') {

            let checkValue = '';
            if (target.is(':checked')) {
                checkValue = 'true';
            }
            else {
                checkValue = 'false';
            }

            toggleSwitchElements(name, checkValue);
        }
        else {
            var value = value = target.val();
            toggleSwitchElements(name, value);
        }

    }

    function toggleSwitchElements(name, value) {
        $('[data-display-if-name="' + name + '"]').hide();

        $('[data-display-if-name="' + name + '"][data-display-if-value="' + value + '"]').show();

        if ($('[data-display-if-name="' + name + '"]').attr('data-display-if-not-value') !== undefined) {
            $('[data-display-if-name="' + name + '"][data-display-if-not-value!="' + value + '"]').show();
        }

        if (value !== '' && value !== null && value !== undefined) {
            $('[data-display-if-name="' + name + '"][data-display-if-has-value="true"]').show();
        }

        $('[data-display-if-name="' + name + '"]').each(function (i, e) {
            const element = $(e);
            const functionName = element.attr('data-display-if-client-evaluate');
            if (functionName) {
                const evaluateFunction = window[functionName];
                if (typeof evaluateFunction == 'function') {
                    if (evaluateFunction.apply(null) === true) {
                        element.show();
                    }
                    else {
                        element.hide();
                    }
                }
            }
        });
    }

    $(function () {
        $('select[data-display-if-toggle]').change(valueChange);

        $('input[data-display-if-toggle]').change(valueChange);

    });

    $.fn.rebindDisplayIf = function () {
        $(this)
            .find('select[data-display-if-toggle], input[data-display-if-toggle]')
            .change(valueChange);
        return $(this);
    };

})($);