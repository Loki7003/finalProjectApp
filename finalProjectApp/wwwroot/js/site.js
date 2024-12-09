// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var userData;

document.addEventListener("DOMContentLoaded", function () {

    const userDataJson = document.getElementsByClassName("userData")[0];

    try {
        userData = JSON.parse(userDataJson.dataset.user);
        console.log(userData);
    } catch (e) {
        console.error(e);
    }
 
    var viewId = document.getElementById("userIndex");

    if (viewId) {

        CheckRole(userData);

    }

});

function CheckRole(userData) {

    var role = userData.UserRole;
    var adminCase = document.getElementsByClassName("administrativeCases");
    var adminCaseMgmt = document.getElementsByClassName("administrativeCasesManagement");
    var techTask = document.getElementsByClassName("technicalTasks");
    var techTaskMgmt = document.getElementsByClassName("technicalTasksManagement")
    var techsAdmin = document.getElementsByClassName("techniciansAdministration");
    var usersAdmin = document.getElementsByClassName("usersAdministation");


    switch (role) {
        case "Lokator":

            for (let i = 0; i < 2; i++) {
                adminCaseMgmt[i].style.display = "none";
                techTaskMgmt[i].style.display = "none";
                techsAdmin[i].style.display = "none";
                usersAdmin[i].style.display = "none";
            }
            break;

        case "Zarządca osiedla":

            for (let i = 0; i < 2; i++) {
                adminCase[i].style.display = "none";
                techTaskMgmt[i].style.display = "none";
                techsAdmin[i].style.display = "none";
                usersAdmin[i].style.display = "none";
            }
            break;

        case "Nadzorca techniczny":

            for (let i = 0; i < 2; i++) {
                adminCase[i].style.display = "none";
                adminCaseMgmt[i].style.display = "none";
                usersAdmin[i].style.display = "none";
            }
            break;

        case "Administrator użytkowników":

            for (let i = 0; i < 2; i++) {
                adminCase[i].style.display = "none";
                adminCaseMgmt[i].style.display = "none";
                techTask[i].style.display = "none";
                techTaskMgmt[i].style.display = "none";
            }
            break;

        case "Administrator Systemu":

            for (let i = 0; i < 2; i++) {
                adminCase[i].style.display = "none";
                techTask[i].style.display = "none";
            }
            break;

    }

}