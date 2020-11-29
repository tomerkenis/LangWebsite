const hamburger = document.querySelector('.hamburger');
const navLinks = document.querySelector('.nav-links');
const links = document.querySelectorAll('.nav-links li');

hamburger.addEventListener("click", () => {
    navLinks.classList.toggle("open");
    links.forEach(link => {
        link.classList.toggle("fade");
    });
});

const signin = document.querySelector('.sign-in');
const overlay = document.querySelector('.overlay');
const signinoverlay = document.querySelector('.sign-in-overlay');

signin.addEventListener("click", () => {
    overlay.classList.toggle("open");
    signinoverlay.classList.toggle("open");
});

overlay.addEventListener("click", () => {
    overlay.classList.toggle("open");
    signinoverlay.classList.toggle("open");
});

