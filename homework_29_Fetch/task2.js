const myURL = "https://api.tvmaze.com/shows";

let container = document.getElementById("main-container");
let moviesOnEveryPage = 3;
let myMovies = [];


fetch(myURL)
  .then((datas) => datas.json())
  .then((movies) => {
    myMovies = movies;
    let pagesCount = myMovies.length / moviesOnEveryPage;

    document.getElementById("pagination").innerHTML = createPagination(
      pagesCount,
      1,
      myMovies
    );
  });

//pagination starts here

function createPagination(pages, page, myMovies) {
  let str = `<ul id="my-pagination-ul">`;
  let active;
  let pageCutLow = page - 1;
  let pageCutHigh = page + 1;

  creationOfHtml(myMovies, page);

  // Show the Previous button only if you are on a page other than the first
  if (page > 1) {
    str += `<li class="page-item previous no"><a onclick="createPagination(${pages}, ${
      page - 1
    }, myMovies)">Previous</a></li>`;
  }

  // Show all the pagination elements if there are less than 6 pages total
  if (pages < 6) {
    for (let p = 1; p <= pages; p++) {
      active = page == p ? "active" : "no";
      str += `<li class="${active}"><a onclick="createPagination(${pages}, ${p}, myMovies); creationOfHtml(myMovies, ${p});">${p}</a></li>`;
    }
  } else {
    // Use "..." to collapse pages outside of a certain range

    // Show the very first page followed by a "..." at the beginning of the pagination section (after the Previous button)
    if (page > 2) {
      str += `<li class="no page-item"><a onclick="createPagination(${pages}, 1, myMovies)">1</a></li>`;
      if (page > 3) {
        str += `<li class="out-of-range"><a onclick="createPagination(${pages}, ${
          page - 2
        }, myMovies)">...</a></li>`;
      }
    }

    // Determine how many pages to show after the current page index
    if (page === 1) {
      pageCutHigh += 2;
    } else if (page === 2) {
      pageCutHigh += 1;
    }

    // Determine how many pages to show before the current page index
    if (page === pages) {
      pageCutLow -= 2;
    } else if (page === pages - 1) {
      pageCutLow -= 1;
    }

    // Output the indexes for pages that fall inside the range of pageCutLow and pageCutHigh
    for (let p = pageCutLow; p <= pageCutHigh; p++) {
      if (p === 0) {
        p += 1;
      }
      if (p > pages) {
        continue;
      }
      active = page == p ? "active" : "no";
      str += `<li class="page-item ${active}"><a onclick="createPagination(${pages}, ${p}, myMovies); creationOfHtml(myMovies, ${p});">${p}</a></li>`;
    }

    // Show the very last page preceded by a "..." at the end of the pagination section (before the Next button)
    if (page < pages - 1) {
      if (page < pages - 2) {
        str += `<li class="out-of-range"><a onclick="createPagination(${pages}, ${
          page + 2
        }, myMovies)">...</a></li>`;
      }
      str += `<li class="page-item no"><a onclick="createPagination(${pages}, ${pages}, myMovies)">${pages}</a></li>`;
    }
  }

  // Show the Next button only if you are on a page other than the last
  if (page < pages) {
    str += `<li class="page-item next no"><a onclick="createPagination(${pages}, ${
      page + 1
    }, myMovies)">Next</a></li>`;
  }
  str += "</ul>";

  // Update the pagination string in the DOM
  document.getElementById("pagination").innerHTML = str;
  return str;
}

//pagination ends

function creationOfHtml(movies, n) {
  container.innerHTML = "";
  
  for (let i = (n - 1) * moviesOnEveryPage; i < moviesOnEveryPage * n; i++) {
    container.innerHTML += `<div class="card" style="width: 22rem; margin-right: 20px; padding: 0;">
<img src="${movies[i].image.original}" class="card-img-top" style="height: 390px;" alt="...">
<div class="card-body">
  <h5 class="card-title">${movies[i].name}</h5>
  <p class="card-text">Premiere: ${movies[i].premiered}</p>
</div>
<ul class="list-group list-group-flush">
  <li class="list-group-item">IMDB Rating: ${movies[i].rating.average}</li>
  <li class="list-group-item">Genre: ${movies[i].genres}</li>
  <li class="list-group-item">Language: ${movies[i].language}</li>
</ul>
<div class="card-body">
  <a href="${movies[i].officialSite}" class="card-link"><button type="button" class="btn btn-primary">Go to website</button></a>
  <a href="task2_details.html" class="card-link" onclick="localStorage.setItem('idOfCurrentPage', ${i+1})"  ><button type="button" class="btn btn-success"  >Go to detail</button></a>
</div>
</div>`;
  }
}


