async function queryVolumes() {
    const response = await fetch("/api/volumes");
    const data = await response.json();

    document.querySelectorAll(".progress-bar-vertical").forEach(element => {
        try {
            let team = data[element.dataset.team];
            element.style.height = `${team.volume * 100}%`;

            let col = element.closest(".team-col");
            if (team.isActive) col.classList.add("active");
            else col.classList.remove("active");
        } catch (error) {
            console.error(error);
            element.style.height = "0%";
        }
    })
};

document.addEventListener("DOMContentLoaded", setInterval(queryVolumes, 200));
