
let addBasketBtns = document.querySelectorAll(".add-to-basket");

addBasketBtns.forEach(btn => {
    btn.addEventListener("click", (e) => {
        e.preventDefault();

        let url = btn.getAttribute("href");

        fetch(url)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    const toastLiveExample = document.getElementById('live-toast-added');
                    toastLiveExample.classList.add("show");
                    updateViewComponent();
                } else {
                    alert(data.message);
                }
            })
    })
})


function updateViewComponent() {
    fetch('/Home/BasketUpdate')
        .then(response => response.text())
        .then(html => {
            document.getElementById('BasketComponent').innerHTML = html;
        })
}


function hideToast() {
    var toastElement = document.getElementById('live-toast-added');
    toastElement.classList.remove('show');
}