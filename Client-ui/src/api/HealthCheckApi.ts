export function healthcheck() {
    return fetch(`${API_URL}/healthcheck`, {
        method: 'get',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
    })
}