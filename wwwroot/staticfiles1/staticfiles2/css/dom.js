function showVerticalMenu() {
    document.getElementById("myDropdown").classList.toggle("show");
}
function setClipboardButton(){
    var codeSnippets = document.querySelectorAll('pre');
    console.log(codeSnippets.length)
    var index = 1;
    codeSnippets.forEach(function (codeSnippet)
    {
        var copyButton = document.createElement('button');
        copyButton.classList.toggle("copy-button");
        copyButton.textContent = "копировать";
        copyButton.id = index.toString();
        index+=1;
        codeSnippet.parentNode.insertBefore(copyButton, codeSnippet);
        var cbp = new ClipboardJS(copyButton, {
            target: function(trigger) {
                return trigger.nextElementSibling;
            }
        });
        cbp.on('success', function(e) {
            e.clearSelection();
            var cbs = document.getElementsByClassName('copy-button');
            for (var i = 0; i<cbs.length; i++) {
                var el = cbs[i];
                el.textContent = 'копировать';
            }
            copyButton.textContent = 'скопировано';
        });
    });
}
function makeMark(index) {
    var dropdowns = document.getElementsByClassName("item");
    for (var i = 0; i < dropdowns.length; i++) {
        var el = dropdowns[i];
        if (el.classList.contains("selected")) {
            el.classList.remove("selected");
        }

    }
    document.getElementById(index).classList.toggle("selected");
}
function clearCode() {
    var codes = document.querySelectorAll('code');
    codes.forEach(function (code) {
        code.removeAttribute('data-highlighted');
    });
    codes.forEach(function(code) {
        code.innerHTML = code.textContent;
        code.classList.forEach(function (cls){
            if (cls.includes('hljs')) {
                code.classList.remove(cls);
            }
            else if (!cls.includes('language')) {
                code.classList.remove(cls);
            }
        });
    });
}
function changeTheme(theme) {
    if (theme === 'light') {
        setActiveTheme('dark');
    }
    else {
        setActiveTheme('light');
    }
}
function setActiveTheme(theme) {
    var darkThemeLink = document.getElementById("dark-theme");
    var lightThemeLink = document.getElementById("light-theme");
    darkThemeLink.disabled = (theme === 'light');
    lightThemeLink.disabled = (theme === 'dark');
    localStorage.setItem("theme", theme);
    hljs.highlightAll();
}
function loadTheme() {
    var currentTheme = localStorage.getItem("theme");
    if (!currentTheme) {
        setActiveTheme('dark');
    }
    else {
        setActiveTheme(currentTheme);
    }
}
function e() {
    clearCode();
    clearCode();
    changeTheme(localStorage.getItem("theme"));
}
window.onclick = function (event) {
    if (!event.target.matches('.menu-button')) {
        var dropdowns = document.getElementsByClassName("vertical-menu-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}