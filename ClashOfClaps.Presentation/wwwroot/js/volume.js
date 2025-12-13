async function queryVolumes() {
    const response = await fetch('/api/volume');
    const data = await response.json();

    document.querySelectorAll(".progress-bar").forEach(element => {
        try {
            element.style.width = `${data[element.dataset.team].volume}%`;
        } catch (error) {
            console.error(error);
            element.style.width = "0%";
        }
    })
}

document.addEventListener("DOMContentLoaded", setInterval(queryVolumes, 600));
