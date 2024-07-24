let number = document.querySelector("#numberId");
let fullname = document.querySelector("#fnameId");
let position = document.querySelector("#positionId");
let table = document.getElementById("tableInfo");
let addBtn = document.querySelector("#add-button");

function addInfo(event) {
  event.preventDefault();
  if (number.value == "" || fullname.value == "" || position.value == "") {
    alert("All fields are required");
    return;
  }

  if (table.rows.length==0) {
    createHeaderRow();
    createBodyRow();
    return;
  }
  if (table.rows.length>0) {
    createBodyRow();
  }
}

addBtn.addEventListener("click", addInfo);

function createHeaderRow() {
  let header = document.createElement("thead");
  let headRow = document.createElement("tr");
  let headerNumber = document.createElement("th");
  let headerFullname = document.createElement("th");
  let headerPosition = document.createElement("th");
  headerNumber.innerText = "No";
  headerFullname.innerText = "Fullname";
  headerPosition.innerText = "Position";

  headRow.append(headerNumber);
  headRow.append(headerFullname);
  headRow.append(headerPosition);
  header.append(headRow);
  table.append(header);
}

function createBodyRow() {
  let body = document.createElement("tbody");
  let bodyRow = document.createElement("tr");
  let bodyNumber = document.createElement("td");
  let bodyFullname = document.createElement("td");
  let bodyPosition = document.createElement("td");
  bodyNumber.innerText=number.value;
  bodyFullname.innerText=fullname.value;
  bodyPosition.innerText=position.value;

  bodyRow.append(bodyNumber);
  bodyRow.append(bodyFullname);
  bodyRow.append(bodyPosition);
  body.append(bodyRow);
  table.append(body);
}
