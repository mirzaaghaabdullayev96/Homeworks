document.addEventListener("DOMContentLoaded", function () {
    const moviesList = document.getElementById("movies-list");
    const loadMoreButton = document.getElementById("load-more");
    const allMoviesModals = document.querySelectorAll(".for-modal-to-open");



    //let moviesPerPage = 10;
    //let currentPage = 1;
    let allMovies = [];

    const movieModal = new bootstrap.Modal(document.getElementById("movieModal"));

    fetchMovies();



    function fetchMovies() {

        fetch("http://localhost:5126/api/MemberMovies")
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                allMovies = data;
                /*displayMovies();*/
            })
            .catch((error) => console.error("Error:", error));
    }

    //function displayMovies() {
    //  const startIndex = (currentPage - 1) * moviesPerPage;
    //  const endIndex = startIndex + moviesPerPage;
    //  const moviesToDisplay = allMovies.slice(startIndex, endIndex);

    //  moviesToDisplay.forEach((movie) => {
    //    const movieCard = document.createElement("div");
    //    movieCard.classList.add("movie-card");

    //    movieCard.innerHTML = `
    //          <img src="${
    //            movie.image ? movie.image.medium : ""
    //          }"  alt="Movie Poster" class="card-img" data-bs-toggle="modal" data-bs-target="#movieModal" data-id="${
    //      movie.id
    //    }">`;
    //    moviesList.appendChild(movieCard);
    //  });

    //  //if (endIndex >= allMovies.length) {
    //  //  loadMoreButton.style.display = "none";
    //  //}
    //}


    //allMoviesModals.forEach.addEventListener("click", (event) => {
    //    const movieId = event.target.dataset.id;
    //    const movie = allMovies.find((m) => m.entities.id == movieId);

    //    document.getElementById("movieModalLabel").textContent = movie.entities.title;
    //    document.getElementById("modalImage").src = movie.image;
    //    document.getElementById("modalSummary").textContent = movie.entities.description;
    //    document.getElementById(
    //        "modalGenres"
    //    ).textContent = `Genres: ${movie.genres.join(", ")}`;
    //    movieModal.show();
    //})



    allMoviesModals.forEach((modalElement) => {
        modalElement.addEventListener("click", (event) => {
            const movieId = event.target.closest('.for-modal-to-open').dataset.id;
            console.log(allMovies)
            const movie = allMovies.entities.find((entity) => entity.id == movieId);

            document.getElementById("movieModalLabel").textContent = movie.title;
            document.getElementById("modalImage").src = `http://localhost:5126/imagesOfMovies/${movie.imageUrl}`
            document.getElementById("modalSummary").textContent = movie.description;
            document.getElementById(
                "modalGenres"
            ).textContent = `Genres: ${movie.genres.join(", ")}`;



            const totalMinutes = movie.duration;
            const hours = Math.floor(totalMinutes / 60);
            const minutes = totalMinutes % 60;

            document.getElementById("modalDuration").textContent = `Duration: ${hours}h ${minutes}m`;

            const buttonBuyTicket = document.getElementById("buy-ticket")
            buttonBuyTicket.setAttribute("href", `http://localhost:7228/ticketreservation/reservation/buy/${movie.id}`);

            document.getElementById("modalRating").textContent = `Rating: ${movie.rating}`;

            const releaseDate = new Date(movie.releaseDate);
            const formattedDate = releaseDate.toISOString().split('T')[0];

            document.getElementById("modalReleaseDate").textContent = `Release date: ${formattedDate}`;
            movieModal.show();
        });
    });



    //document.addEventListener("click", (event) => {
    //    if (event.target && event.target.matches(".card-img")) {
    //        const movieId = event.target.dataset.id;
    //        const movie = allMovies.find((m) => m.id == movieId);

    //        if (movie) {
    //            console.log(movie);
    //            document.getElementById("movieModalLabel").textContent = movie.name;
    //            document.getElementById("modalImage").src = movie.image
    //                ? movie.image.original
    //                : "";
    //            document.getElementById("modalSummary").textContent = movie.summary
    //                ? movie.summary.replace(/<[^>]*>/g, "")
    //                : "No summary available";
    //            document.getElementById(
    //                "modalGenres"
    //            ).textContent = `Genres: ${movie.genres.join(", ")}`;
    //            movieModal.show();
    //        }
    //    }
    //});

    //loadMoreButton.addEventListener("click", () => {
    //  currentPage++;
    //  displayMovies();
    //});


});
