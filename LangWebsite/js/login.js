// Currently not in use.

function validateLoginPassword() {
    var pass = $('input[id="password-new"]').val();
    if (pass.length >= 8 && // password at least 8 characters
        pass.match(/\d/) && // checks if there is at least 1 number (instead of [0-9])
        pass.match(/[!@#$%^&*]/) && // checks if the password contains at least one of the characters in the group
        pass.match(/[A-Z]/)) { // checks if there is an uppercase letter
        return true;
    } else return false;
}

function validateLoginEmail() {
    var email = $('input[id="email-new"]').val();
    if (email.match(/^.+@.+\..+$/) && // checks if the format is: _@_._ (_ is at least one character)
        !email.match(" ") && // checks if there are no spaces
        email.match(/@/g).length == 1 && // checks if there is exactly one '@'
        email.indexOf("@") + 1 != email.indexOf(".")) { // checks that '.' isn't right after '@' 
        return true;
    } else return false;
}

function validateLoginAll() {
    return validateLoginEmail() && validateLoginPassword();
}