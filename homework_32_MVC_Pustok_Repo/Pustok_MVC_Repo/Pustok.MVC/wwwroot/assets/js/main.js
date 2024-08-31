
let addBasketBtns = document.querySelectorAll(".add-to-basket");
let openDetails = document.querySelectorAll(".cus-modal-details");

console.log("Salam");

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
                    updateBasketViewComponent();
                } else {
                    alert(data.message);
                }
            })
    })
})


function updateBasketViewComponent() {
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


//modal!!

openDetails.forEach(btn => {
    btn.addEventListener("click", (e) => {
        e.preventDefault();
        let url = btn.getAttribute("href");
        fetch(url)
            .then(response => response.text())
            .then(html => {
                console.log(html)
                document.getElementById('cus-container-for-modal').innerHTML = html;

            })
    })
})


function hideModal() {
    var toastElement = document.getElementById('quickModal');
    toastElement.classList.remove('show');
}








