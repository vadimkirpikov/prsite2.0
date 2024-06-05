function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
function makeMark(index) {
    var dropdowns = document.getElementsByClassName("item");
    console.log(dropdowns);
    for (var i = 0; i < dropdowns.length; i++) {
        var el = dropdowns[i];
        console.log("Yes");
        if (el.classList.contains("selected")) {
            el.classList.remove("selected");
        }

    }
    document.getElementById(index).classList.toggle("selected");

}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    console.log("Hello");
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