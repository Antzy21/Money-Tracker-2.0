const path = `https://localhost:${port}`


import {
    get,
    port
} from './ApiBase.js'

function healthcheck() {
    return get(`${path}/healthcheck`)
}

export {
    healthcheck,
}