function send(event) {
    event.preventDefault();

    data = {
        UserName: getData("userName"),
        Password: getData("password")
    };

    $.ajax({
        url: "/auth/user",
        data: data,
        type: "post",
        success: function(user) {
            if (user) {
                setText("message", "welcome " + user.userName);
                setClass("resultMessage", "success");
                removeClass("resultMessage", "error");
            }
            else {
                setText("message", "looks like one of the fields is incorrect, please try again.");
                setClass("resultMessage", "error");
                removeClass("resultMessage", "success");
            }
            toggleInputForm();
        },
        error: function(e) {
            // TODO: consider inexpected errors
        }
    });
}

function back(event) {
    event.preventDefault();

    toggleInputForm();
}

function getData(inputName) {
    return document.getElementsByName(inputName)[0].value;
}

function setText(id, text) {
    document.getElementById(id).innerHTML = text;
}

function toggleInputForm() {
    toggleClass("inputForm", "hidden");
    toggleClass("resultMessage", "hidden");
}

function toggleClass(classNameSelector, classNameToToggle) {
    var elements = document.getElementsByClassName(classNameSelector);
    elements[0].classList.toggle(classNameToToggle);
}

function setClass(classNameSelector, classNameToToggle) {
    var elements = document.getElementsByClassName(classNameSelector);
    elements[0].classList.add(classNameToToggle);
}

function removeClass(classNameSelector, classNameToToggle) {
    var elements = document.getElementsByClassName(classNameSelector);
    elements[0].classList.remove(classNameToToggle);
}