let input = document.getElementById("form1");
let searchButton = document.getElementById("search-button");
let container = document.getElementById("my-container");

const myURL = "https://api.github.com/users/";

searchButton.addEventListener("click", function () {
  fetch(myURL + input.value)
    .then((data) => data.json())
    .then((data) => {
      // container.style.visibility="visible";
      // document.getElementById("prof-picture").setAttribute("src",data.avatar_url);
      container.innerHTML = `
      <img src="${data.avatar_url}" class="card-img-top" alt="..." />
        <div class="card-body">
          <h5 class="card-title">${data.bio}</h5>
          <p class="card-text">
            ${data.name}
          </p>
        </div>
        <ul class="list-group list-group-flush" style="list-style:none;">
          <li class="list-group-item">
              <button
              type="button"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              style="
                padding: 0px;
                background: white;
                border: none;
                text-decoration: underline;
              "
            >
              Followers: ${data.followers}
            </button>
          
            <div
              class="modal fade"
              id="exampleModal"
              tabindex="-1"
              aria-labelledby="exampleModalLabel"
              aria-hidden="true"
            >
              <div
                class="modal-dialog modal-dialog modal-dialog-centered modal-dialog-scrollable"
                id="div-for-modal"
              >
                <div class="modal-content">
                  <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">
                      Followers
                    </h1>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="d-flex modal-body div-modal-body div-modal-body-followers">
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-secondary"
                      data-bs-dismiss="modal"
                    >
                      Close
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </li>


          <li class="list-group-item">
              <button
              type="button"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal2"
              style="
                padding: 0px;
                background: white;
                border: none;
                text-decoration: underline;
              "
            >
              Following: ${data.following}
            </button>
          
            <div
              class="modal fade"
              id="exampleModal2"
              tabindex="-1"
              aria-labelledby="exampleModalLabel"
              aria-hidden="true"
            >
              <div
                class="modal-dialog modal-dialog modal-dialog-centered modal-dialog-scrollable"
                id="div-for-modal"
              >
                <div class="modal-content">
                  <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">
                      Following
                    </h1>
                    <button
                      type="button"
                      class="btn-close"
                      data-bs-dismiss="modal"
                      aria-label="Close"
                    ></button>
                  </div>
                  <div class="d-flex modal-body div-modal-body div-modal-body-following">
                  </div>
                  <div class="modal-footer">
                    <button
                      type="button"
                      class="btn btn-secondary"
                      data-bs-dismiss="modal"
                    >
                      Close
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </li>
          <li class="list-group-item">Public repos: ${data.public_repos}</li>
        </ul>
        <div class="card-body">
          <a href="${data.html_url}" class="card-link">Go to profile link</a>
          <a href="#" class="card-link">Another link</a>
        </div>`;

      let divModalFollowers = document.querySelector(
        ".div-modal-body-followers"
      );
      let divModalFollowing = document.querySelector(
        ".div-modal-body-following"
      );

      fetch(data.followers_url)
        .then((followers) => followers.json())
        .then((followers) => {
          followers.forEach((element) => {
            divModalFollowers.innerHTML += `<div class="div-items">
             <img
             class="img-in-modal"
             src="${element.avatar_url}"
             alt=""
             />
             <span class="text-in-modal">Login: ${element.login}</span></div>`;
          });
        });

      fetch(myURL + input.value + "/following")
        .then((following) => following.json())
        .then((following) => {
          following.forEach((following) => {
            divModalFollowing.innerHTML += `<div class="div-items">
             <img
             class="img-in-modal"
             src="${following.avatar_url}"
             alt=""
             />
             <span class="text-in-modal">Login: ${following.login}</span></div>`;
          });
        });

      document.getElementById("my-container").style.visibility="visible";
    });
});
