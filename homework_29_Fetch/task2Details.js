let detailsContainer = document.getElementById("main-container-details");

let idOfMyPage=localStorage.getItem("idOfCurrentPage");


fetch(`https://api.tvmaze.com/shows/${idOfMyPage}`)
  .then(myMovie=>myMovie.json())
  .then(data=>{
    renderDetailsPage(data)
  })

function renderDetailsPage(movies) {
  detailsContainer.innerHTML = `<div
          style="display: flex; justify-content: center; align-items: center"
        >
          <img
            src="${movies.image.original}"
            style="width: 80%; justify-content: center; align-items: center"
            alt=""
          />
        </div>
        <div style="padding-top: 6rem;">
          <h1>${movies.name}</h1>
          <p style="width: 700px" class="fst-italic">
          ${movies.summary}
          </p>
          <ul>
            <li><span class="fw-bold">IMDB Point: ${movies.rating.average} </span></li>
            <li><span class="fw-bold">Language: ${movies.language} </span></li>
            <li><span class="fw-bold">Genre: ${movies.genres}</span></li>
            <li><span class="fw-bold">Premiere: ${movies.premiered} </span></li>
            <li><span class="fw-bold">Ended: ${movies.ended}</span></li>
          </ul>
          <a href="${movies.officialSite}" class="card-link"
            ><button type="button" class="btn btn-success">
              Go to website
            </button></a
          >
          <a href="task2_movies.html" class="card-link"
            ><button type="button" class="btn btn-primary">Go back</button></a
          >
        </div>`;
}
