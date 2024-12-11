// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var userData;
var casesData;
var caseData;

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

    var userMenu = document.getElementById("userMenu");

    if (userMenu) {

        CheckRole(userData);

    }

    var showAdminCases = document.getElementById("showAdminCases");

    if (showAdminCases) {

        DisplayAdminCaseTable(casesData);

    }

    var caseDetails = document.getElementById("caseDetails");

    if (caseDetails) {

        DisplayCaseDetails(caseData,userData);

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
    var techTaskMgmt = document.getElementsByClassName("technicalTasksManagementLink")
    var techsAdmin = document.getElementsByClassName("techniciansAdministrationLink");
    var usersAdmin = document.getElementsByClassName("usersAdministationLink");


    switch (role) {
        case "Lokator":

            removeElements(adminCaseMgmt);
            removeElements(techTaskMgmt);
            removeElements(techsAdmin);
            removeElements(usersAdmin);
            break;

        case "Zarządca osiedla":

            removeElements(adminCase);
            removeElements(techTaskMgmt);
            removeElements(techsAdmin);
            removeElements(usersAdmin);
            break;

        case "Nadzorca techniczny":

            
            removeElements(adminCase);
            removeElements(adminCaseMgmt);
            removeElements(usersAdmin);
            break;

        case "Administrator użytkowników":

            removeElements(adminCase);
            removeElements(adminCaseMgmt);
            removeElements(techTask);
            removeElements(techTaskMgmt);
            break;

        case "Administrator Systemu":

            //removeElements(adminCase);
            removeElements(techTask);
            break;

    }

}

function DisplayAdminCaseTable(casesData) {

    var table = document.getElementById("showAdminCasesTable");
    var tbody = table.createTBody();
    var div = document.getElementById("showAdminCasesDiv");

    if (casesData == 0) {
        table.style.display = "none";
        div.appendChild("<h3>Nie utworzono jeszcze żadnych spraw administracyjnych</h3>");
    }
    else {
        casesData.forEach(caseData => {

            var rawDate = new Date(caseData.CaseCreated);
            var formattedDate = rawDate.toLocaleDateString('pl-PL');

            var row = tbody.insertRow();
            row.className = "caseRow";            

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

        })
    }
}

function DisplayCaseDetails(caseData,userData) {

    var div = document.getElementById("caseDetailsDiv");

    if (caseData.AssignedEmplyee == undefined) {

        caseData.AssignedEmplyee = "Nie przypisano";

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
    caseData.CaseStatus = "W realizacji";

    var htmlText = "<div class='caseDetailsElementBig'><h3>Temat zgłoszenia: " + caseData.CaseSubject + "</h3><hr /></div>" +
        "<div class='caseDetailsElementBig'><label for='caseDetailsParagraph'>Opis sprawy:</label><br /><p id='caseDetailsParagraph'>" + caseData.CaseDetails + "</p></div>" +
        "<div class='caseDetailsElement'><label for='caseRequestorParagraph'>Autor sprawy:</label></div><div class='caseDetailsElement'><label for='caseStatusParagraph'>Status sprawy:</label></div><div class='caseDetailsElement'><label for='caseCreatedParagraph'>Data utworzenia sprawy:</label></div>" +
        "<div class='caseDetailsElement'><p id='caseRequestorParagraph'/>" + caseData.CaseRequestor + "<p></div>" +
        "<div class='caseDetailsElement'><p id='caseStatusParagraph'>" + caseData.CaseStatus + "</p></div>" +
        "<div class='caseDetailsElement'><p id='caseCreatedParagraph'>" + caseData.CaseCreated + "</p></div>" +
        "<div class='caseDetailsElementMedium'><label for='assignedEmployeeParagraph'>Pracownik odpowiedzialny za sprawę:</label></div><div class='caseDetailsElementMedium'><label for='caseClosedParagraph'>Data zamknięcia sprawy:</label></div>" +
        "<div class='caseDetailsElementMedium'><p id='assignedEmployeeParagraph'>" + caseData.AssignedEmplyee + "</p></div>" + 
        "<div class='caseDetailsElementMedium'><p id='caseClosedParagraph'>" + caseData.CaseClosed + "</p></div>" + 
        "<div class='caseDetailsElementBig'><label for='caseResponseTextArea'>Odpowiedź administracji:</label><textarea id='caseResponseTextArea' disabled>" + caseData.CaseResponse + "</textarea></div><br />";

    div.innerHTML = htmlText;

    if (userData.UserRole == "Zarządca osiedla" || userData.UserRole == "Administrator Systemu") {

        //Zmienić na form żeby zrobić to POSTem

        var button = document.createElement("div");
        button.id = "updateCaseButton";
        var button1 = document.createElement("div");
        button1.id = "updateCaseButton";
        var button2 = document.createElement("div");
        button2.id = "updateCaseButton";


        switch (caseData.CaseStatus) {

            case "Nowa":

                button.style.gridColumn = "span 2";
                button.innerHTML = "<p>Do zatwierdzenia</p>";
                button.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${2}`;
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 2";
                button1.innerHTML = "<p>W realizacji</p>";
                button1.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${3}`;
                });
                div.appendChild(button1);
                break;

            case "Do zatwierdzenia":

                var textarea = div.getElementsByTagName("textarea")[0];
                textarea.disabled = false;
                button.style.gridColumn = "span 2";
                button.innerHTML = "<p>W realizacji</p>";
                button.addEventListener("click", function () {
                    window.location.href = `/AdminCases/UpdateCase?caseId=${caseData.CaseId}&caseStatus=${3}`;
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 2";
                button1.innerHTML = "<p>Odrzucona</p>";
                button1.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${5}&caseResponse=${textarea.value}`;
                    }
                });
                div.appendChild(button1);
                break;

            case "W realizacji":

                var textarea = div.getElementsByTagName("textarea")[0];
                textarea.disabled = false;
                button.style.gridColumn = "span 2";
                button.innerHTML = "<p>Rozwiązana</p>";
                button.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${4}&caseResponse=${textarea.value}`;
                    }
                });
                div.appendChild(button);
                button1.style.gridColumn = "span 2";
                button1.innerHTML = "<p>Odrzucona</p>";
                button1.addEventListener("click", function () {
                    if (textarea.value == "") {
                        alert("Odpowiedź nie może być pusta!");
                    } else {
                        window.location.href = `/AdminCases/CloseCase?caseId=${caseData.CaseId}&caseStatus=${5}&caseResponse=${textarea.value}`;
                    }
                });
                div.appendChild(button1);
                break;
                
        }
    }
}