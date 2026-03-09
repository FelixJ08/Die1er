document.addEventListener("DOMContentLoaded", function () {
    const switchInput = document.getElementById("themeSwitch");
    const themeIcon = document.getElementById("themeIcon");
    if (!switchInput || !themeIcon) return;

    const body = document.body;

    // Gespeicherten Modus laden
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark") {
        body.classList.add("dark-mode");
        switchInput.checked = true;
        themeIcon.textContent = "☀️";
    }

    switchInput.addEventListener("change", function () {
        if (this.checked) {
            body.classList.add("dark-mode");
            localStorage.setItem("theme", "dark");
            themeIcon.textContent = "☀️"; // Dark Mode = Sonne
        } else {
            body.classList.remove("dark-mode");
            localStorage.setItem("theme", "light");
            themeIcon.textContent = "🌙"; // Light Mode = Mond
        }
    });
});