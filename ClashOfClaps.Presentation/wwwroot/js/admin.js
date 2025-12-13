async function increasePoints(team, delta) {
    await fetch(`/api/points/${team}/increase?points=${delta}`, {
        method: "PUT"
    });
}

async function decreasePoints(team, delta) {
    await fetch(`/api/points/${team}/decrease?points=${delta}`, {
        method: "PUT"
    });
}

async function resetPoints(team) {
    await fetch(`/api/points/${team}`, {
        method: "PUT"
    });
}
