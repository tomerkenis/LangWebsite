// init
$('.error-password').hide();
$('.error-email').hide();
$('.error-username').hide();

function validatePassword() {
    var pass = $('input[id="password-new"]').val();
    if (pass.length >= 8 && // password at least 8 characters
        pass.match(/\d/) && // checks if there is at least 1 number (instead of [0-9])
        pass.match(/[!@#$%^&*]/) && // checks if the password contains at least one of the characters in the group
        pass.match(/[A-Z]/)) { // checks if there is an uppercase letter
        $('.error-password').hide();
        return true;
    } else {
        $('.error-password').show();
        return false;
    }
}

function validateEmail() {
    var email = $('input[id="email-new"]').val();
    if (email.match(/^.+@.+\..+$/) && // checks if the format is: _@_._ (_ is at least one character)
        !email.match(" ") && // checks if there are no spaces
        email.match(/@/g).length == 1 && // checks if there is exactly one '@'
        email.charAt(email.indexOf("@") + 1) != ".") { // checks that '.' isn't right after '@' 
        $('.error-email').hide();
        return true;
    } else {
        $('.error-email').show();
        return false;
    }
}



 function validateUsername() {
     var name = $('input[id="username-new"]').val();
     if (name.length >= 3) {
         $('.error-username').hide();
         return true;
     }
     else {
         $('.error-username').show();
         return false;
     }
}

function validateAll() {
    emailValidation = validateEmail();
    usernameValidation = validateUsername();
    passwordValidation = validatePassword();
    return emailValidation && usernameValidation && passwordValidation;
 }