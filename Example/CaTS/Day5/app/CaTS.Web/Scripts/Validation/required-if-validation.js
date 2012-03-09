/// <reference path="jquery-1.4.4-vsdoc.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

$.validator.addMethod('requiredif',
    function (value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];

        // get the target value (as a string, 
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];

        targetvalue =
          (targetvalue == null ? '' : targetvalue).toString();

        // get the actual value of the target control
        // note - this probably needs to cater for more 
        // control types, e.g. radios
        var control = $(id);
        var controltype = control.attr('type');
        var actualvalue =
            controltype === 'checkbox' ?
            control.attr('checked').toString() :
            control.val();

        // if the condition is true, reuse the existing 
        // required field validator functionality
        if (targetvalue === actualvalue)
            return $.validator.methods.required.call(
              this, value, element, parameters);

        return true;
    }
);

$.validator.unobtrusive.adapters.add(
    'requiredif', ['dependentproperty', 'targetvalue'],
    function (options) {
        options.rules['requiredif'] = {
            dependentproperty: options.params['dependentproperty'], // e.g., "SupportTicket_Status"
            targetvalue: options.params['targetvalue']              // e.g., "3" (for StatusTypes.Resolved)
        };

        options.messages['requiredif'] = options.message;
    }
);