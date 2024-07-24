
let btn=document.querySelector('#my-btn');
let todoPlaceholder=document.querySelector('#my-todo');
let todoLists=document.querySelector('#todo-Lists')

function addItem() {

  if(todoPlaceholder.value == '') {
    return;
  }
  
  let div = document.createElement("div");
  let li = document.createElement("li");
  let deleteBtn = document.createElement("button");

  deleteBtn.addEventListener("click", function() {
    deleteBtn.parentElement.remove();
  });

  div.style.display = "flex";
  deleteBtn.innerText = "Remove";
  li.innerText = todoPlaceholder.value;

  div.append(li);
  div.append(deleteBtn);
  todoLists.append(div);
  todoPlaceholder.value = '';
}


btn.addEventListener('click', addItem);

todoPlaceholder.addEventListener('keydown', function(event){
  if(event.key=='Enter'){
    addItem();
  }
})