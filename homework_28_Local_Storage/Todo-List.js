let btn = document.querySelector("#my-btn");
let todoPlaceholder = document.querySelector("#my-todo");
let todoLists = document.querySelector("#todo-Lists");
let todoListArr;

document.addEventListener("DOMContentLoaded", function () {
  let todoListArrStrStorage = localStorage.getItem("todoList");

  todoListArr = JSON.parse(todoListArrStrStorage) || [];
  btn.addEventListener("click", addItem);

  if (!todoListArrStrStorage) {
    return;
  }

  todoListArr.forEach((todo) => {
    let div = document.createElement("div");
    let li = document.createElement("li");
    let deleteBtn = document.createElement("button");
    deleteBtn.addEventListener("click", function () {
      todoListArr.splice(todoListArr.indexOf(todo), 1);
      localStorage.setItem("todoList", JSON.stringify(todoListArr));
      if (todoListArr.length == 0) {
        localStorage.removeItem("todoList");
      }
      deleteBtn.parentElement.remove();
    });

    div.style.display = "flex";
    div.style.marginLeft = "15px";
    div.style.marginBottom = "10px";
    deleteBtn.innerText = "Remove";
    deleteBtn.style.marginLeft = "15px";
    li.innerText = todo;

    div.append(li);
    div.append(deleteBtn);
    todoLists.append(div);
  });
});

function addItem() {
  if (todoPlaceholder.value == "") {
    document.getElementById("spanErr").style.display = "block";
    return;
  }

  document.getElementById("spanErr").style.display = "none";

  let div = document.createElement("div");
  let li = document.createElement("li");
  let deleteBtn = document.createElement("button");

  div.style.display = "flex";
  div.style.marginLeft = "15px";
  div.style.marginBottom = "10px";
  deleteBtn.innerText = "Remove";
  deleteBtn.style.marginLeft = "15px";
  li.innerText = todoPlaceholder.value;
  let myValue = todoPlaceholder.value;

  div.append(li);
  div.append(deleteBtn);
  todoLists.append(div);
  todoListArr.push(todoPlaceholder.value);

  deleteBtn.addEventListener("click", function () {
    todoListArr.splice(todoListArr.indexOf(myValue), 1);
    localStorage.setItem("todoList", JSON.stringify(todoListArr));
    if (todoListArr.length == 0) {
      localStorage.removeItem("todoList");
    }
    deleteBtn.parentElement.remove();
  });
  let todolistArrStr = JSON.stringify(todoListArr);
  localStorage.setItem("todoList", todolistArrStr);
  todoPlaceholder.value = "";
}
