var failedSignInsCount = 0;

function send(event) {
    event.preventDefault();

    if (!$("#form").valid()) {
        return;
    }

    var data = {
        UserName: getData("UserName"),
        Password: getData("Password")
    };

    var ajax = {
        url: "/auth/user",
        data: data,
        type: "post",
        success: function (user) {
            if (user) {
                // if has status code or error,
                // other fields (e.g. firstName) can be empty 
                if (user.statusHint == 404) {
                    redirectToNotFound();
                    return;
                }
                else if (user.error) {
                    setText("message", user.error);
                    showResultMessageAsError();
                }
                else {
                    setText("message", "welcome " + user.userName);
                    showResultMessageAsSuccess();
                }
            }
            else {
                setText("message", "looks like one of the fields is incorrect, please try again.");
                showResultMessageAsError();
            }

            if (failedSignInsCount >= 3) {
                redirectToNotFound();
            }

            toggleInputForm();

            function redirectToNotFound() {
                window.location.href = "Auth/NotFound";
            }
        },
        error: function (e) {
            // TODO: consider inexpected errors
        }
    };
    
    var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
    if (antiForgeryToken) {
        ajax.headers = {};
        ajax.headers["X-XSRF-Token"] = antiForgeryToken;
    };

    $.ajax(ajax);
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

function showResultMessageAsError() {
    setClass("resultMessage", "error");
    removeClass("resultMessage", "success");
    ++failedSignInsCount;
}

function showResultMessageAsSuccess() {
    setClass("resultMessage", "success");
    removeClass("resultMessage", "error");
    failedSignInsCount = 0;
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