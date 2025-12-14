async function queryVolumes() {
    const response = await fetch("/api/volumes");
    const data = await response.json();

    document.querySelectorAll(".progress-bar").forEach(element => {
        try {
            element.style.width = `${data[element.dataset.team]}%`;
        } catch (error) {
            console.error(error);
            element.style.width = "0%";
        }
    })
};

document.addEventListener("DOMContentLoaded", setInterval(queryVolumes, 600));
