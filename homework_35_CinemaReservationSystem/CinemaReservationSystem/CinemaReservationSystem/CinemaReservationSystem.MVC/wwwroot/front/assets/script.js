document.addEventListener("DOMContentLoaded", function () {
  const moviesList = document.getElementById("movies-list");
  const loadMoreButton = document.getElementById("load-more");

  let moviesPerPage = 10;
  let currentPage = 1;
  let allMovies = [];
  const movieModal = new bootstrap.Modal(document.getElementById("movieModal"));

  function fetchMovies() {
    fetch("https://api.tvmaze.com/shows")
      .then((response) => response.json())
      .then((data) => {
        allMovies = data;
        displayMovies();
      })
      .catch((error) => console.error("Error:", error));
  }

  function displayMovies() {
    const startIndex = (currentPage - 1) * moviesPerPage;
    const endIndex = startIndex + moviesPerPage;
    const moviesToDisplay = allMovies.slice(startIndex, endIndex);

    moviesToDisplay.forEach((movie) => {
      const movieCard = document.createElement("div");
      movieCard.classList.add("movie-card");

      movieCard.innerHTML = `
            <img src="${
              movie.image ? movie.image.medium : ""
            }"  alt="Movie Poster" class="card-img" data-bs-toggle="modal" data-bs-target="#movieModal" data-id="${
        movie.id
      }">`;
      moviesList.appendChild(movieCard);
    });

    if (endIndex >= allMovies.length) {
      loadMoreButton.style.display = "none";
    }
  }

  document.addEventListener("click", (event) => {
    if (event.target && event.target.matches(".card-img")) {
      const movieId = event.target.dataset.id;
      const movie = allMovies.find((m) => m.id == movieId);

      if (movie) {
        console.log(movie);
        document.getElementById("movieModalLabel").textContent = movie.name;
        document.getElementById("modalImage").src = movie.image
          ? movie.image.original
          : "";
        document.getElementById("modalSummary").textContent = movie.summary
          ? movie.summary.replace(/<[^>]*>/g, "")
          : "No summary available";
        document.getElementById(
          "modalGenres"
        ).textContent = `Genres: ${movie.genres.join(", ")}`;
        movieModal.show();
      }
    }
  });

  loadMoreButton.addEventListener("click", () => {
    currentPage++;
    displayMovies();
  });

  fetchMovies();
});
