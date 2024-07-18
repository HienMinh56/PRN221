document.addEventListener('DOMContentLoaded', (event) => {
    const signUpBtn = document.getElementById('sign-up-btn');
    const signInBtn = document.getElementById('sign-in-btn');
    const container = document.querySelector('.container');

    signUpBtn.addEventListener('click', () => {
        container.classList.add("sign-up-mode");
    });

    signInBtn.addEventListener('click', () => {
        container.classList.remove("sign-up-mode");
    });

    const signUpBtn2 = document.getElementById('sign-up-btn2');
    const signInBtn2 = document.getElementById('sign-in-btn2');

    signUpBtn2.addEventListener('click', () => {
        container.classList.add("sign-up-mode");
    });

    signInBtn2.addEventListener('click', () => {
        container.classList.remove("sign-up-mode");
    });
});