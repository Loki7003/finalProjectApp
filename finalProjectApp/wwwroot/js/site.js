// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var userData;
var casesData;
var caseData;
var tasksData;
var taskData;
var techsData;
var techData;
var usersData;
var checkedUserData;
var premisesData;
var premiseData;

document.addEventListener("DOMContentLoaded", function () {

    var userDataJson = document.getElementsByClassName("userData")[0];
    try {
        userData = JSON.parse(userDataJson.dataset.user);
        console.log(userData);
    } catch (e) {
        console.error(e);
    }

    var casesDataJson = document.getElementsByClassName("casesData")[0];
    try {
        casesData = JSON.parse(casesDataJson.dataset.cases);
        console.log(casesData);
    } catch (e) {
        console.error(e);
        casesData = "0";
    }

    var caseDataJson = document.getElementById("caseDetails");
    try {
        caseData = JSON.parse(caseDataJson.dataset.case);
        console.log(caseData);
    } catch (e) {
        console.error(e);
    }

    var tasksDataJson = document.getElementsByClassName("tasksData")[0];
    try {
        tasksData = JSON.parse(tasksDataJson.dataset.tasks);
        console.log(tasksData);
    } catch (e) {
        console.error(e);
    }

    var taskDataJson = document.getElementById("taskDetails");
    try {
        taskData = JSON.parse(taskDataJson.dataset.task);
        console.log(taskData);
    } catch (e) {
        console.error(e);
    }

    var techsDataJson = document.getElementById("manageTechnician");
    try {
        techsData = JSON.parse(techsDataJson.dataset.techs);
        console.log(techsData);
    } catch (e) {
        console.error(e);
    }

    var techDataJson = document.getElementById("technicianDetails");
    try {
        techData = JSON.parse(techDataJson.dataset.tech);
        console.log(techData);
    } catch (e) {
        console.error(e);
    }

    var usersDataJson = document.getElementById("manageUser");
    try {
        usersData = JSON.parse(usersDataJson.dataset.users);
        console.log(usersData);
    } catch (e) {
        console.error(e);
    }

    var checkedUserDataJson = document.getElementById("userDetails");
    try {
        checkedUserData = JSON.parse(checkedUserDataJson.dataset.checked);
        console.log(checkedUserData);
    } catch (e) {
        console.error(e);
    }

    var premisesDataJson = document.getElementsByClassName("premisesData")[0];
    try {
        premisesData = JSON.parse(premisesDataJson.dataset.premises);
        console.log(premisesData);
    } catch (e) {
        console.error(e);
    }

    var premiseDataJson = document.getElementById("premiseDetails");
    try {
        premiseData = JSON.parse(premiseDataJson.dataset.premise);
        console.log(premiseData);
    } catch (e) {
        console.error(e);
    }


    var userMenu = document.getElementById("userMenu");

    if (userMenu) {

        CheckRole(userData);

    }

    var showAdminCases = document.getElementById("showAdminCases");
    var showActiveAdminCases = document.getElementById("showActiveAdminCases");
    var showArchivedAdminCases = document.getElementById("showArchivedAdminCases");

    if (showAdminCases || showActiveAdminCases || showArchivedAdminCases) {

        DisplayAdminCaseTable(casesData);

    }

    var caseDetails = document.getElementById("caseDetails");

    if (caseDetails) {

        DisplayCaseDetails(caseData, userData);

    }

    var showTechTasks = document.getElementById("showTechTasks");
    var showActiveTechTasks = document.getElementById("showActiveTechTasks");
    var showArchivedTechTasks = document.getElementById("showArchivedTechTasks");

    if (showTechTasks || showActiveTechTasks || showArchivedTechTasks) {

        DisplayTechTaskTable(tasksData);

    }

    var taskDetails = document.getElementById("taskDetails");

    if (taskDetails) {

        DisplayTaskDetails(taskData, userData);

    }

    var manageTechnician = document.getElementById("manageTechnician");

    if (manageTechnician) {

        DisplayTechnicianTable(techsData);

    }

    var technicianDetails = document.getElementById("technicianDetails");

    if (technicianDetails) {

        DisplayTechnicianDetails(techData);

    }

    var manageUser = document.getElementById("manageUser");

    if (manageUser) {

        DisplayUsersTable(usersData, userData);

    }

    var userDetails = document.getElementById("userDetails");

    if (userDetails) {

        DisplayUserDetails(checkedUserData);

    }

    var listPremises = document.getElementById("listPremises");

    if (listPremises) {

        DisplayPremisesTable(premisesData);

    }

    var premiseDetails = document.getElementById("premiseDetails");

    if (premiseDetails) {

        DisplayPremiseDetails(premiseData);

    }

    var addUserForm = document.getElementById("addUserForm");

    if (addUserForm) {

        LoadCreateUserForm(userData, addUserForm, premisesData);

    }

});

function removeElements(elements) {
        Array.from(elements).forEach((el) => el.remove());
    }

function CheckRole(userData) {

    var role = userData.UserRole;
    var adminCase = document.getElementsByClassName("administrativeCasesLink");
    var adminCaseMgmt = document.getElementsByClassName("administrativeCasesManagementLink");
    var techTask = document.getElementsByClassName("technicalTasksLink");
    var premiseMgmt = document.getElementsByClassName("premiseAdministrationLink");
    var techTaskMgmt = document.getElementsByClassName("technicalTasksManagementLink")
    var techsAdmin = document.getElementsByClassName("techniciansAdministrationLink");
    var usersAdmin = document.getElementsByClassName("usersAdministationLink");


    switch (role) {
        case "Lokator":

            removeElements(adminCaseMgmt);
            removeElements(techTaskMgmt);
            removeElements(premiseMgmt);
            removeElements(techsAdmin);
            removeElements(usersAdmin);
            break;

        case "Zarządca osiedla":

            removeElements(adminCase);
            removeElements(techTask);
            removeElements(techTaskMgmt);
            removeElements(techsAdmin);
            removeElements(usersAdmin);
            break;

        case "Nadzorca techniczny":

            removeElements(adminCase);
            removeElements(adminCaseMgmt);
            removeElements(techTask);
            removeElements(premiseMgmt);
            removeElements(usersAdmin);
            break;

        case "Administrator użytkowników":

            removeElements(adminCase);
            removeElements(adminCaseMgmt);
            removeElements(techTask);
            removeElements(techTaskMgmt);
            removeElements(premiseMgmt);
            break;

        case "Administrator Systemu":

            removeElements(adminCase);
            removeElements(techTask);
            break;

    }

}

function LoadCreateUserForm(userData, addUserForm, premisesData) {

    var roleSelect = addUserForm.getElementsByTagName("select")[0];
    var userPremiseSelect = addUserForm.getElementsByTagName("select")[1];
    

    if (userData.UserRole == "Administrator użytkowników") {

        var administratorOption = roleSelect.getElementsByTagName("option")[1];
        administratorOption.disabled = true;

    }
   
    premisesData.forEach(premiseData => {

        var option = document.createElement("option");
        option.value = premiseData.PremiseId;
        option.text = "Ulica " + premiseData.Street + " " + premiseData.Staircase + "/" + premiseData.Apartment;

        userPremiseSelect.appendChild(option);

    });

    roleSelect.addEventListener("change", function () {

        if (roleSelect.value == "2") {
            userPremiseSelect.disabled = false;
            userPremiseSelect.required = true;
        } else {
            userPremiseSelect.disabled = true;
            userPremiseSelect.required = false;
        }

    });

}

function DisplayAdminCaseTable(casesData) {

    var table = document.getElementById("showAdminCasesTable");
    var tbody = table.createTBody();

    casesData.forEach(caseData => {

        var rawDate = new Date(caseData.CaseCreated);
        var formattedDate = rawDate.toLocaleDateString('pl-PL');

        var row = tbody.insertRow();
        row.className = "insertedRow";

        row.addEventListener("click", function () {
            window.location.href = `/AdminCases/CaseDetails?caseId=${caseData.CaseId}`;
        });


        var cellCaseId = row.insertCell(0);
        var cellCaseSubject = row.insertCell(1);
        var cellCreatedDate = row.insertCell(2);
        var cellCaseStatus = row.insertCell(3);

        cellCaseId.textContent = caseData.CaseId;
        cellCaseSubject.textContent = caseData.CaseSubject;
        cellCreatedDate.textContent = formattedDate;
        cellCaseStatus.textContent = caseData.CaseStatus;

        row.style.cursor = "pointer";

    });

    $('#showAdminCasesTable').DataTable({
        language: {
            "decimal": ",",
            "thousands": ".",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ wpisów",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ wpisów",
            "infoEmpty": "Brak dostępnych danych",
            "infoFiltered": "(filtrowano z _MAX_ dostępnych wpisów)",
            "infoPostFix": "",
            "loadingRecords": "Ładowanie...",
            "zeroRecords": "Brak pasujących wpisów",
            "emptyTable": "Brak danych w tabeli",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby sortować rosnąco",
                "sortDescending": ": aktywuj, aby sortować malejąco"
            }
        }
    });
}

function DisplayCaseDetails(caseData,userData) {

    var div = document.getElementById("caseDetailsDiv");

    if (caseData.AssignedEmployee == null) {

        caseData.AssignedEmployee = "Nie przypisano";

    }

    if (caseData.CaseResponse == null) {

        caseData.CaseResponse = "";

    }

    if (caseData.CaseClosed == null) {

        caseData.CaseClosed = "";

    } else {

        var rawClosedDate = new Date(caseData.CaseClosed);
        var formattedClosedDate = rawClosedDate.toLocaleDateString('pl-PL');
        caseData.CaseClosed = formattedClosedDate;

    }

    var rawCreatedDate = new Date(caseData.CaseCreated);
    var formattedCreatedDate = rawCreatedDate.toLocaleDateString('pl-PL');
    caseData.CaseCreated = formattedCreatedDate;

    var htmlText = "<div class='caseDetailsElementBig'><h3>Temat sprawy: " + caseData.CaseSubject + "</h3><hr /></div>" +
        "<div class='caseDetailsElementBig'><label for='caseDetailsParagraph'>Opis sprawy:</label><p id='caseDetailsParagraph'>" + caseData.CaseDetails + "</p></div>" +
        "<div class='caseDetailsElement'><label for='caseRequestorParagraph'>Wnioskodawca:</label></div><div class='caseDetailsElement'><label for='caseStatusParagraph'>Status sprawy:</label></div><div class='caseDetailsElement'><label for='caseCreatedParagraph'>Data utworzenia sprawy:</label></div>" +
        "<div class='caseDetailsElement'><p id='caseRequestorParagraph'/>" + caseData.CaseRequestor + "<p></div>" +
        "<div class='caseDetailsElement'><p id='caseStatusParagraph'>" + caseData.CaseStatus + "</p></div>" +
        "<div class='caseDetailsElement'><p id='caseCreatedParagraph'>" + caseData.CaseCreated + "</p></div>" +
        "<div class='caseDetailsElementMedium'><label for='assignedEmployeeParagraph'>Pracownik odpowiedzialny za sprawę:</label></div><div class='caseDetailsElementMedium'><label for='caseClosedParagraph'>Data zamknięcia sprawy:</label></div>" +
        "<div class='caseDetailsElementMedium'><p id='assignedEmployeeParagraph'>" + caseData.AssignedEmployee + "</p></div>" + 
        "<div class='caseDetailsElementMedium'><p id='caseClosedParagraph'>" + caseData.CaseClosed + "</p></div>" + 
        "<div class='caseDetailsElementBig'><label for='caseAddressParagraph'>Adres wnioskodawcy:</label><p id='caseAddressParagraph'>Ulica " + caseData.RequestorAddress + "</p></div>" +
        "<div class='caseDetailsElementBig'><label for='caseResponseTextArea'>Odpowiedź administracji:</label><textarea id='caseResponseTextArea' disabled>" + caseData.CaseResponse + "</textarea></div>";

    div.innerHTML = htmlText;

    if (userData.UserRole == "Zarządca osiedla") {

        var button = document.createElement("div");
        button.id = "updateCaseButton";
        var button1 = document.createElement("div");
        button1.id = "updateCaseButton";

        switch (caseData.CaseStatus) {

            case "Nowa":

                button.style.gridColumn = "span 3";
                button.innerHTML = "<p>Do zatwierdzenia</p>";
                button.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${2}&userId${userData.userId}`;
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 3";
                button1.innerHTML = "<p>W realizacji</p>";
                button1.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${3}&userId${userData.userId}`;
                });
                div.appendChild(button1);
                break;

            case "Do zatwierdzenia":

                var textarea = div.getElementsByTagName("textarea")[0];
                textarea.disabled = false;
                button.style.gridColumn = "span 3";
                button.innerHTML = "<p>W realizacji</p>";
                button.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${3}&userId${userData.userId}`;
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 3";
                button1.innerHTML = "<p>Odrzucona</p>";
                button1.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${5}&caseResponse=${textarea.value}&userId${userData.userId}`;
                    }
                });
                div.appendChild(button1);
                break;

            case "W realizacji":

                var textarea = div.getElementsByTagName("textarea")[0];
                textarea.disabled = false;
                button.style.gridColumn = "span 3";
                button.innerHTML = "<p>Rozwiązana</p>";
                button.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${4}&caseResponse=${textarea.value}&userId${userData.userId}`;
                    }
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 3";
                button1.innerHTML = "<p>Odrzucona</p>";
                button1.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${5}&caseResponse=${textarea.value}&userId${userData.userId}`;
                    }
                });
                div.appendChild(button1);
                break;
                
        }
    }
}

function DisplayTechTaskTable(tasksData) {

    var table = document.getElementById("showTechTasksTable");
    var tbody = table.createTBody();

    tasksData.forEach(taskData => {

        var rawDate = new Date(taskData.TaskCreated);
        var formattedDate = rawDate.toLocaleDateString('pl-PL');

        var row = tbody.insertRow();
        row.className = "insertedRow";

        row.addEventListener("click", function () {
            window.location.href = `/TechnicalTasks/TaskDetails?taskId=${taskData.TaskId}`;
        });


        var cellTaskId = row.insertCell(0);
        var cellTaskSubject = row.insertCell(1);
        var cellTaskCategory = row.insertCell(2);
        var cellCreatedDate = row.insertCell(3);
        var cellTaskStatus = row.insertCell(4);

        cellTaskId.textContent = taskData.TaskId;
        cellTaskSubject.textContent = taskData.TaskSubject;
        cellTaskCategory.textContent = taskData.TaskCategory;
        cellCreatedDate.textContent = formattedDate;
        cellTaskStatus.textContent = taskData.TaskStatus;

        row.style.cursor = "pointer";

    })

    $('#showTechTasksTable').DataTable({
        language: {
            "decimal": ",",
            "thousands": ".",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ wpisów",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ wpisów",
            "infoEmpty": "Brak dostępnych danych",
            "infoFiltered": "(filtrowano z _MAX_ dostępnych wpisów)",
            "infoPostFix": "",
            "loadingRecords": "Ładowanie...",
            "zeroRecords": "Brak pasujących wpisów",
            "emptyTable": "Brak danych w tabeli",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby sortować rosnąco",
                "sortDescending": ": aktywuj, aby sortować malejąco"
            }
        }
    });
}

function DisplayTaskDetails(taskData) {

    var div = document.getElementById("taskDetailsDiv");

    if (taskData.TaskTechnician == null) {

        taskData.TaskTechnician = "Nie przypisano";

    }

    if (taskData.TaskClosed == null) {

        taskData.TaskClosed = "";

    } else {

        var rawClosedDate = new Date(taskData.TaskClosed);
        var formattedClosedDate = rawClosedDate.toLocaleDateString('pl-PL');
        taskData.TaskClosed = formattedClosedDate;

    }

    var rawCreatedDate = new Date(taskData.TaskCreated);
    var formattedCreatedDate = rawCreatedDate.toLocaleDateString('pl-PL');
    taskData.TaskCreated = formattedCreatedDate;

    var htmlText = "<div class='taskDetailsElementBig'><h3>Temat zgłoszenia: " + taskData.TaskSubject + "</h3><hr /></div>" +
        "<div class='taskDetailsElementBig'><label for='taskDetailsParagraph'>Opis zgłoszenia:</label><p id='taskDetailsParagraph'>" + taskData.TaskDetails + "</p></div>" +
        "<div class='taskDetailsElement'><label for='taskRequestorParagraph'>Autor zgłoszenia:</label></div><div class='taskDetailsElement'><label for='taskStatusParagraph'>Status zgłoszenia:</label></div><div class='taskDetailsElement'><label for='taskCreatedParagraph'>Data utworzenia zgłoszenia:</label></div>" +
        "<div class='taskDetailsElement'><p id='taskRequestorParagraph'/>" + taskData.TaskRequestor + "<p></div>" +
        "<div class='taskDetailsElement'><p id='taskStatusParagraph'>" + taskData.TaskStatus + "</p></div>" +
        "<div class='taskDetailsElement'><p id='taskCreatedParagraph'>" + taskData.TaskCreated + "</p></div>" +
        "<div class='taskDetailsElement'><label for='taskCategoryParagraph'>Kategoria zgłoszenia:</label></div><div class='taskDetailsElement'><label for='taskTechnicianParagraph'>Technik odpowiedzialny za zgłoszenie:</label></div><div class='taskDetailsElement'><label for='taskClosedParagraph'>Data zamknięcia zgłoszenia:</label></div>" +
        "<div class='taskDetailsElement'><p id='taskCategoryParagraph'>" + taskData.TaskCategory + "</p></div>" +
        "<div class='taskDetailsElement'><p id='taskTechnicianParagraph'>" + taskData.TaskTechnician + "</p></div>" +
        "<div class='taskDetailsElement'><p id='taskClosedParagraph'>" + taskData.TaskClosed + "</p></div>" + 
        "<div class='taskDetailsElementBig'><label for='taskAddressParagraph'>Adres zgłoszenia:</label><p id='taskAddressParagraph'>Ulica " + taskData.RequestorAddress + "</p></div>";

    div.innerHTML = htmlText;

}

function DisplayTechnicianTable(techsData) {

    var table = document.getElementById("manageTechnicianTable");
    var tbody = table.createTBody();

    
    techsData.forEach(techData => {

        var row = tbody.insertRow();
        row.className = "insertedRow";

        row.addEventListener("click", function () {
            window.location.href = `/UsersManagement/TechnicianDetails?techId=${techData.TechId}`;
        });


        var cellTechId = row.insertCell(0);
        var cellTechLogin = row.insertCell(1);
        var cellTechFirstName = row.insertCell(2);
        var cellTechLastName = row.insertCell(3);

        cellTechId.textContent = techData.TechId;
        cellTechLogin.textContent = techData.TechLogin;
        cellTechFirstName.textContent = techData.TechFirstName;
        cellTechLastName.textContent = techData.TechLastName;

        row.style.cursor = "pointer";

    });

    $('#manageTechnicianTable').DataTable({
        language: {
            "decimal": ",",
            "thousands": ".",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ wpisów",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ wpisów",
            "infoEmpty": "Brak dostępnych danych",
            "infoFiltered": "(filtrowano z _MAX_ dostępnych wpisów)",
            "infoPostFix": "",
            "loadingRecords": "Ładowanie...",
            "zeroRecords": "Brak pasujących wpisów",
            "emptyTable": "Brak danych w tabeli",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby sortować rosnąco",
                "sortDescending": ": aktywuj, aby sortować malejąco"
            }
        }
    });
}

document.getElementById("addTechForm").addEventListener("submit", function (event) {

    const checkboxes = document.querySelectorAll('#addTechForm input[name="techSpecs"]');
    const isChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);

    if (!isChecked) {
        event.preventDefault();
        alert('Proszę wybrać co najmniej jedną specjalizację technika!');
    }
});

function DisplayTechnicianDetails(techData) {

    var div = document.getElementById("technicianDetailsDiv");

    var rawPasswordDate = new Date(techData.PasswordChangedOn);
    var formattedPasswordDate = rawPasswordDate.toLocaleDateString('pl-PL');
    techData.PasswordChangedOn = formattedPasswordDate;


    if (techData.Enabled == 1) {

        techData.Enabled = "Aktywne";

    } else {

        techData.Enabled = "Dezaktywowane";

    }

    var htmlText = "<div class='technicianDetailsElement'><label for='technicianFirstnameParagraph'>Imię:</label></div><div class='technicianDetailsElement'><label for='technicianLastnameParagraph'>Nazwisko:</label></div><div class='technicianDetailsElement'><label for='technicianLoginParagraph'>Login:</label></div>" +
        "<div class='technicianDetailsElement'><p id='technicianFirstnameParagraph'/>" + techData.TechFirstName + "<p></div>" +
        "<div class='technicianDetailsElement'><p id='technicianLastnameParagraph'>" + techData.TechLastName + "</p></div>" +
        "<div class='technicianDetailsElement'><p id='technicianLoginParagraph'>" + techData.TechLogin + "</p></div>" +
        "<div class='technicianDetailsElement'><label for='techSpecsParagraph'>Specjalizacje:</label></div><div class='technicianDetailsElement'><label for='technicianEnabledParagraph'>Status konta:</label></div><div class='technicianDetailsElement'><label for='technicianPasswordParagraph'>Ostania zmiana hasła:</label></div>" +
        "<div class='technicianDetailsElement'><p id='techSpecsParagraph'>" + techData.SpecializationsName + "</p></div>" +
        "<div class='technicianDetailsElement'><p id='technicianEnabledParagraph'>" + techData.Enabled + "</p></div>" +
        "<div class='technicianDetailsElement'><p id='technicianPasswordParagraph'>" + techData.PasswordChangedOn + "</p></div>";

    div.innerHTML = htmlText;

    var button = document.createElement("div");
    button.id = "updateCaseButton";
    var button1 = document.createElement("div");
    button1.id = "updateCaseButton";

    switch (techData.Enabled) {

        case "Aktywne":

            button.style.gridColumn = "span 3";
            button.innerHTML = "<p>Dezaktywuj konto</p>";
            button.addEventListener("click", function () {
                window.location.href = `/UsersManagement/UpdateTechnician?techId=${techData.TechId}&status=0`;
            });
            div.appendChild(button);
            button1.style.gridColumn = "span 3";
            button1.innerHTML = "<p>Resetuj hasło</p>";
            button1.addEventListener("click", function () {
                window.location.href = `/UsersManagement/ResetTechnicianPassword?techId=${techData.TechId}`;
            });
            div.appendChild(button1);
            break;

        case "Dezaktywowane":

            button.style.gridColumn = "span 6";
            button.innerHTML = "<p>Aktywuj konto</p>";
            button.addEventListener("click", function () {
                window.location.href = `/UsersManagement/UpdateTechnician?techId=${techData.TechId}&status=1`;
            });
            div.appendChild(button);

            break;
    }
}

function DisplayUsersTable(usersData, userData) {

    var table = document.getElementById("manageUserTable");
    var tbody = table.createTBody();

    if (userData.UserRole == "Administrator użytkowników") {

        usersData = usersData.filter(user => user.UserRole !== "Administrator Systemu");

    }

    usersData.forEach(user => {

        var row = tbody.insertRow();
        row.className = "insertedRow";

        row.addEventListener("click", function () {
            window.location.href = `/UsersManagement/UserDetails?userId=${user.Id}`;
        });

        var cellUserId = row.insertCell(0);
        var cellUserLogin = row.insertCell(1);
        var cellUserFirstName = row.insertCell(2);
        var celluserLastName = row.insertCell(3);

        cellUserId.textContent = user.Id;
        cellUserLogin.textContent = user.Username;
        cellUserFirstName.textContent = user.Name;
        celluserLastName.textContent = user.Lastname;

        row.style.cursor = "pointer";

    });

    $('#manageUserTable').DataTable({
        language: {
            "decimal": ",",
            "thousands": ".",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ wpisów",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ wpisów",
            "infoEmpty": "Brak dostępnych danych",
            "infoFiltered": "(filtrowano z _MAX_ dostępnych wpisów)",
            "infoPostFix": "",
            "loadingRecords": "Ładowanie...",
            "zeroRecords": "Brak pasujących wpisów",
            "emptyTable": "Brak danych w tabeli",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby sortować rosnąco",
                "sortDescending": ": aktywuj, aby sortować malejąco"
            }
        }
    });
}

function DisplayUserDetails(checkedUserData) {

    var div = document.getElementById("userDetailsDiv");

    var rawPasswordDate = new Date(checkedUserData.PasswordChangedOn);
    var formattedPasswordDate = rawPasswordDate.toLocaleDateString('pl-PL');
    checkedUserData.PasswordChangedOn = formattedPasswordDate;


    if (checkedUserData.Enabled == 1) {

        checkedUserData.Enabled = "Aktywne";

    } else {

        checkedUserData.Enabled = "Dezaktywowane";

    }

    var htmlText = "<div class='userDetailsElement'><label for='userFirstnameParagraph'>Imię:</label></div><div class='userDetailsElement'><label for='userLastnameParagraph'>Nazwisko:</label></div><div class='userDetailsElement'><label for='userLoginParagraph'>Login:</label></div>" +
        "<div class='userDetailsElement'><p id='userFirstnameParagraph'/>" + checkedUserData.Name + "<p></div>" +
        "<div class='userDetailsElement'><p id='userLastnameParagraph'>" + checkedUserData.Lastname + "</p></div>" +
        "<div class='userDetailsElement'><p id='userLoginParagraph'>" + checkedUserData.Username + "</p></div>" +
        "<div class='userDetailsElement'><label for='userRoleParagraph'>Rola użytkownika:</label></div><div class='userDetailsElement'><label for='userEnabledParagraph'>Status konta:</label></div><div class='userDetailsElement'><label for='userPasswordParagraph'>Ostania zmiana hasła:</label></div>" +
        "<div class='userDetailsElement'><p id='userRoleParagraph'>" + checkedUserData.UserRole + "</p></div>" +
        "<div class='userDetailsElement'><p id='userEnabledParagraph'>" + checkedUserData.Enabled + "</p></div>" +
        "<div class='userDetailsElement'><p id='userPasswordParagraph'>" + checkedUserData.PasswordChangedOn + "</p></div>";

    div.innerHTML = htmlText;

    var button = document.createElement("div");
    button.id = "updateCaseButton";
    var button1 = document.createElement("div");
    button1.id = "updateCaseButton";

    switch (checkedUserData.Enabled) {

        case "Aktywne":

            button.style.gridColumn = "span 3";
            button.innerHTML = "<p>Dezaktywuj konto</p>";
            button.addEventListener("click", function () {
                window.location.href = `/UsersManagement/UpdateUser?userId=${checkedUserData.Id}&status=0`;
            });
            div.appendChild(button);
            button1.style.gridColumn = "span 3";
            button1.innerHTML = "<p>Resetuj hasło</p>";
            button1.addEventListener("click", function () {
                window.location.href = `/UsersManagement/ResetUserPassword?userId=${checkedUserData.Id}`;
            });
            div.appendChild(button1);
            break;

        case "Dezaktywowane":

            button.style.gridColumn = "span 6";
            button.innerHTML = "<p>Aktywuj konto</p>";
            button.addEventListener("click", function () {
                window.location.href = `/UsersManagement/UpdateUser?userId=${checkedUserData.Id}&status=1`;
            });
            div.appendChild(button);

            break;
    }
}

function DisplayPremisesTable(premisesData) {

    var table = document.getElementById("listPremisesTable");
    var tbody = table.createTBody();

    premisesData.forEach(premiseData => {

        var row = tbody.insertRow();
        row.className = "insertedRow";

        row.addEventListener("click", function () {
            window.location.href = `/Premises/PremiseDetails?premiseId=${premiseData.PremiseId}`;
        });


        var cellPremiseId = row.insertCell(0);
        var cellStreet = row.insertCell(1);
        var cellStaircase = row.insertCell(2);
        var cellApartment = row.insertCell(3);

        cellPremiseId.textContent = premiseData.PremiseId;
        cellStreet.textContent = premiseData.Street;
        cellStaircase.textContent = premiseData.Staircase;
        cellApartment.textContent = premiseData.Apartment;

        row.style.cursor = "pointer";

    });

    $('#listPremisesTable').DataTable({
        language: {
            "decimal": ",",
            "thousands": ".",
            "search": "Szukaj:",
            "lengthMenu": "Pokaż _MENU_ wpisów",
            "info": "Wyświetlanie od _START_ do _END_ z _TOTAL_ wpisów",
            "infoEmpty": "Brak dostępnych danych",
            "infoFiltered": "(filtrowano z _MAX_ dostępnych wpisów)",
            "infoPostFix": "",
            "loadingRecords": "Ładowanie...",
            "zeroRecords": "Brak pasujących wpisów",
            "emptyTable": "Brak danych w tabeli",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
            "aria": {
                "sortAscending": ": aktywuj, aby sortować rosnąco",
                "sortDescending": ": aktywuj, aby sortować malejąco"
            }
        }
    });
}

function DisplayPremiseDetails(premiseData) {

    var div = document.getElementById("premiseDetailsDiv");


    if (premiseData.OwnerMailAddress == "" && premiseData.Owner == " ") {

        premiseData.Owner = "Brak";
        premiseData.OwnerMailAddress = "Brak";

    }

    var htmlText = "<div class='premiseDetailsElement'><label for='streetParagraph'>Ulica:</label></div><div class='premiseDetailsElement'><label for='staircaseParagraph'>Klatka schodowa:</label></div><div class='premiseDetailsElement'><label for='apartmentParagraph'>Numer mieszkania:</label></div>" +
        "<div class='premiseDetailsElement'><p id='streetParagraph'/>" + premiseData.Street + "<p></div>" +
        "<div class='premiseDetailsElement'><p id='staircaseParagraph'>" + premiseData.Staircase + "</p></div>" +
        "<div class='premiseDetailsElement'><p id='apartmentParagraph'>" + premiseData.Apartment + "</p></div>" +
        "<div class='premiseDetailsElementMedium'><label for='ownerParagraph'>Właściciel:</label></div><div class='premiseDetailsElementMedium'><label for='ownerMailAddressParagraph'>Adres E-Mail właściela:</label></div>" +
        "<div class='premiseDetailsElementMedium'><p id='ownerParagraph'>" + premiseData.Owner + "</p></div>" +
        "<div class='premiseDetailsElementMedium'><p id='ownerMailAddressParagraph'>" + premiseData.OwnerMailAddress + "</p></div>";

    div.innerHTML = htmlText;

}