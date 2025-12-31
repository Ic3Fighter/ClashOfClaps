async function queryPoints() {
    const response = await fetch("/api/points");
    const data = await response.json();

    document.querySelectorAll(".points").forEach(element => {
        try {
            element.innerText = data[element.dataset.team];
        } catch (error) {
            console.error(error);
        }
    });
}

document.addEventListener("DOMContentLoaded", setInterval(queryPoints, 1000));
